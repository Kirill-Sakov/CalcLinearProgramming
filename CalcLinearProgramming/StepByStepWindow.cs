using DataGridWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CalcLinearProgramming
{
    public partial class StepByStepWindow : Form
    {
        /// <summary>
        /// Путь до директории приложения
        /// </summary>
        private string ExeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

        /// <summary>
        /// Текущая задача
        /// </summary>
        private LinearProgrammingProblem Problem;

        /// <summary>
        /// Текущий шаг
        /// </summary>
        private int Step;

        /// <summary>
        /// Буфер для задачи
        /// </summary>
        private List<List<List<Fraction>>> BufferElements = new List<List<List<Fraction>>>();

        /// <summary>
        /// Буфер для задачи
        /// </summary>
        private List<List<string>> BufferForHeaders = new List<List<string>>();

        /// <summary>
        /// Вспомогательная переменная, которая определяет была ли создана симплекс-таблица.
        /// </summary>
        private bool simplexTableWasDraw;

        /// <summary>
        /// Вспомогательная переменная, которая определяет была ли добавлена угловая точка для ответа.
        /// </summary>
        private bool cornerDotAnswerWasAdded;

        public StepByStepWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для окна пошагового режима с задачей
        /// </summary>
        /// <param name="problem">Задача линейного программирования</param>
        public StepByStepWindow(LinearProgrammingProblem problem)
        {
            this.Problem = problem;

            // изначально мы на нулевом шаге
            this.Step = 0;

            // Инициализируем компоненты окна
            InitializeComponent();

            // Добавляем в ячейки данные
            tabControl.TabPages[0].Text = "Матрица коэффициентов системы ограничения равенств.";
            DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.Restrictions, true);

            if (Problem.CornerDot != null)
            {
                Problem.TransformColumnsForCornerDot(_dataGridViewProblem);
            }
        }

        /// <summary>
        /// При загрузке окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StepByStepWindow_Load(object sender, EventArgs e)
        {
            // Путь до справки. Для helpProvider
            helpProvider.HelpNamespace = new Uri(Path.Combine(ExeDirectory, "data\\help\\stepbystephelp.html")).LocalPath;
        }

        /// <summary>
        /// Кнопка "Далее"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNext_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    try
                    {
                        // Буферизируем данные
                        BufferingTableValues(Problem.ToFractionList(DataGridWorker.ReadGridsFrom(_dataGridViewProblem)));
                        // Прямой ход Гаусса
                        Problem.Gauss();
                        // Обновляем параметры
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.Restrictions, false);
                        Step++;
                        tabControl.TabPages[0].Text = "Шаг 1: Прямой ход метода Гаусса.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка на {Step} шаге!", MessageBoxButtons.OK);
                    }
                    break;

                case 1:
                    try
                    {
                        // Буферизируем данные
                        BufferingTableValues(Problem.ToFractionList(DataGridWorker.ReadGridsFrom(_dataGridViewProblem)));
                        // Выражение базисных переменных + обратный ход Гаусса
                        Problem.HoistingMatrix();
                        // Обновляем параметры
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.Restrictions, false);
                        Step++;
                        tabControl.TabPages[0].Text = "Шаг 2: Выражение базисных переменных.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка на {Step} шаге!", MessageBoxButtons.OK);
                    }                    
                    break;

                case 2:

                    try
                    {
                        // Буферизируем данные
                        BufferingTableValues(Problem.ToFractionList(DataGridWorker.ReadGridsFrom(_dataGridViewProblem)));
                        BufferingHeaders(_dataGridViewProblem);
                        if (simplexTableWasDraw == false)
                        {
                            Problem.TransformGridForSimplexTable(_dataGridViewProblem);
                            Problem.SimplexTable = new SimplexTable(_dataGridViewProblem);
                            simplexTableWasDraw = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка создания симплекс таблицы на {Step} шаге!", MessageBoxButtons.OK);
                        return;
                    }

                    // Если симплекс таблица создалась и отрисовалась успешно
                    // Проверяеем её на ответ
                    switch (Problem.SimplexTable.ResponseCheck())
                    {
                        case 0:
                            // Продолжаем искать решение
                            Step++;
                            tabControl.TabPages[0].Text = "Шаг 3: Симплекс-таблица.";
                            break;
                        case 1:
                            // Если ответ готов сразу без выбора опорного элемента
                            tabControl.TabPages[0].Text = "Ответ готов!";
                            Step++;
                            labelAnswer.Visible = true;
                            groupBoxCornerDot.Visible = true;
                            buttonNext.Enabled = false;

                            // Подставляем ответ
                            if (Problem.Min == false)
                            {
                                labelAnswer.Text = "Ответ :" + Problem.SimplexTable.Response();
                            }
                            else
                            {
                                labelAnswer.Text = "Ответ :" + Problem.SimplexTable.Response() * (-1);
                            }

                            // Выводим угловую точку ответа (X*)
                            if (cornerDotAnswerWasAdded == false)
                            {
                                //добавляем точку
                                DataGridWorker.SetParamToGrids(
                                    _dataGridViewCornerDot,
                                    Problem.ResponseCornerDot(_dataGridViewProblem),
                                    true);
                                cornerDotAnswerWasAdded = true;
                            }

                            break;

                        case -1:
                            Step++;
                            MessageBox.Show("Задача не разрешима!");
                            tabControl.TabPages[0].Text = "Задача не разрешима!";
                            buttonNext.Enabled = false;
                            break;
                    }

                    break;

                case 3:
                    try
                    {
                        BufferingTableValues(Problem.SimplexTable.SimplexTableElements);
                        //выбор опорного
                        Problem.SimplexTable.SelectionOfTheSupportElements(_dataGridViewProblem);
                        Step++;
                        tabControl.TabPages[0].Text = $"Шаг {Step}: Выбор опорного элемента";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка при выборе опорных элементов на {Step} шаге.", MessageBoxButtons.OK);
                    }
                    break;

                default:
                    
                    try
                    {
                        // Проверяем, выбран ли опорный элемент
                        Problem.SimplexTable.SupportElementPressedOrNot(_dataGridViewProblem);
                        // Меняем хэдэры колонки и строки местами
                        Problem.SimplexTable.ChangeHeaders(_dataGridViewProblem);
                        // Буферизируем симплекс таблицу
                        BufferingTableValues(Problem.SimplexTable.SimplexTableElements);
                        // Удаляем подсвеченные ячейки
                        UncolorGreenGrids(_dataGridViewProblem);
                        // Вычисление симплекс таблицы по выбранному опорному элементу
                        Problem.SimplexTable.CalculateSimplexTable();
                        // Обновление ячеек
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.SimplexTable.SimplexTableElements, false);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка при выборе опорного элемента на шаге {Step}", MessageBoxButtons.OK);
                        return;
                    }

                    switch (Problem.SimplexTable.ResponseCheck())
                    {
                        case 0:
                            Step++;
                            tabControl.TabPages[0].Text = $"Шаг {Step}: Выбор опорного элемента";
                            //выбор опорного
                            Problem.SimplexTable.SelectionOfTheSupportElements(_dataGridViewProblem);
                            break;
                        case 1:
                            tabControl.TabPages[0].Text = "Ответ готов!";
                            Step++;
                            labelAnswer.Visible = true;
                            groupBoxCornerDot.Visible = true;
                            if (Problem.Min == false)
                            {
                                labelAnswer.Text = "Ответ :" + Problem.SimplexTable.Response();
                            }
                            else
                            {
                                labelAnswer.Text = "Ответ :" + Problem.SimplexTable.Response() * (-1);
                            }

                            if (cornerDotAnswerWasAdded == false)
                            {
                                //добавляем угловую точку решения (X*)
                                DataGridWorker.SetParamToGrids(
                                    _dataGridViewCornerDot,
                                    Problem.ResponseCornerDot(_dataGridViewProblem),
                                    true);
                                cornerDotAnswerWasAdded = true;
                            }

                            buttonNext.Enabled = false;
                            break;
                        case -1:
                            Step++;
                            MessageBox.Show("Задача не разрешима!");
                            tabControl.TabPages[0].Text = "Задача не разрешима!";
                            buttonNext.Enabled = false;
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// Кнопка "Назад"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            switch (Step)
            {
                case 0:
                    // Отменить операцию закрытия окна
                    bool CancelСlosing = false;
                    
                    var result = MessageBox.Show(
                        "Предыдущего шага нет. Возврат приведёт к закрытию текущей задачи. Вы уверены?",
                        "Закрыть задачу?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        // Если пользователь не хочет закрывать окно, отменяем операцию
                        CancelСlosing = true;
                    }

                    if (!CancelСlosing)
                        this.Close();
                    break;

                case 1:
                    // Возвращение с шага 1
                    try
                    {
                        // Достаём данные из буфера и заносим их в ячейки
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, GetOutOfTheBuffer(), false);
                        // Заносим данные из буфера в данные задачи
                        Problem.Restrictions = Problem.ToFractionList(DataGridWorker.ReadGridsFrom(_dataGridViewProblem));
                        // Убавляем шаг
                        Step--;
                        tabControl.TabPages[0].Text = "Матрица коэффициентов системы ограничений равенств.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка при возвращении с шага {Step}.", MessageBoxButtons.OK);
                    }
                    break;

                case 2:
                    try
                    {
                        // Достаём данные из буфера и заносим их в ячейки
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, GetOutOfTheBuffer(), false);
                        // Заносим данные из буфера в данные задачи
                        Problem.Restrictions = Problem.ToFractionList(DataGridWorker.ReadGridsFrom(_dataGridViewProblem));
                        // Убавляем шаг
                        Step--;
                        tabControl.TabPages[0].Text = "Шаг 1: Прямой ход метода Гаусса.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка при возвращении с шага {Step}.", MessageBoxButtons.OK);
                    }                    
                    break;

                case 3:

                    // Достаём данные из буфера и заносим их в ячейки
                    DataGridWorker.SetParamToGrids(_dataGridViewProblem, GetOutOfTheBuffer(), true);
                    // Достаём хэдэры из буфера
                    DataGridWorker.SetHeaders(_dataGridViewProblem, GetOutOfTheBufferHeaders());
                    // Обнуляем данные симплекс таблицы
                    Problem.SimplexTable.SimplexTableElements = new List<List<Fraction>>();
                    simplexTableWasDraw = false;

                    Step--;
                    tabControl.TabPages[0].Text = "Шаг 2: Выражение базисных переменных.";

                    // Если ответ был готов сразу и уже отобразился - скрываем его
                    labelAnswer.Visible = false;
                    groupBoxCornerDot.Visible = false;
                    buttonNext.Enabled = true;

                    break;

                case 4:
                    UncolorGreenGrids(_dataGridViewProblem);
                    GetOutOfTheBuffer();
                    Step--;
                    tabControl.TabPages[0].Text = "Шаг 3: Симплекс-таблица.";
                    break;

                default:
                    
                    UncolorGreenGrids(_dataGridViewProblem);

                    // Достаём из буффера элементы симплекс таблицы
                    Problem.SimplexTable.SimplexTableElements = GetOutOfTheBuffer();

                    // Достаём данные из буфера и заносим их в ячейки
                    DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.SimplexTable.SimplexTableElements, false);
                    // Меняем хэдэры обратно, доставая их из буффера
                    Problem.SimplexTable.ChangeHeadersFromBuffer(_dataGridViewProblem);
                    // Выбор опорного
                    Problem.SimplexTable.SelectionOfTheSupportElements(_dataGridViewProblem);

                    Step--;
                    tabControl.TabPages[0].Text = $"Шаг {Step}: Выбор опорного элемента";
                    labelAnswer.Visible = false;
                    groupBoxCornerDot.Visible = false;
                    buttonNext.Enabled = true;

                    break;
            }
        }

        /// <summary>
        /// Вернуть обычный цвет подсвеченным ячейкам
        /// </summary>
        /// <param name="grid"></param>
        private void UncolorGreenGrids(DataGridView grid)
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
            }
        }

        /// <summary>
        /// Буферизация коэффициентов
        /// </summary>
        /// <param name="ogr"></param>
        private void BufferingTableValues(List<List<Fraction>> restrictions)
        {
            BufferElements.Add(new List<List<Fraction>>());
            for (int i = 0; i < restrictions.Count; i++)
            {
                BufferElements[Step].Add(new List<Fraction>());
                for (int j = 0; j < restrictions[0].Count; j++)
                    BufferElements[Step][i].Add(restrictions[i][j]);
            }
        }

        /// <summary>
        /// Возвращение коэффициентов из буффера
        /// </summary>
        /// <returns>Возвращает string матрицу коэффициентов</returns>
        private List<List<Fraction>> GetOutOfTheBuffer()
        {
            List<List<Fraction>> tmpRestrictions = new List<List<Fraction>>();

            for (int i = 0; i < this.BufferElements[Step - 1].Count; i++)
            {
                tmpRestrictions.Add(new List<Fraction>());
                for (int j = 0; j < this.BufferElements[Step - 1][0].Count; j++)
                    tmpRestrictions[i].Add(this.BufferElements[Step - 1][i][j]);
            }

            this.BufferElements.RemoveAt(Step - 1);

            return tmpRestrictions;
        }

        /// <summary>
        /// Буферизация заголовков таблицы для последующего восстановления
        /// </summary>
        /// <param name="grid"></param>
        private void BufferingHeaders(DataGridView grid)
        {
            BufferForHeaders.Add(new List<string>());
            
            for (int i = 0; i <  grid.ColumnCount; i++)
            {
                BufferForHeaders[Step - 2].Add(grid.Columns[i].HeaderText);
            }
        }

        /// <summary>
        /// Буферизация заголовков таблицы для последующего восстановления
        /// </summary>
        /// <param name="grid"></param>
        private List<string> GetOutOfTheBufferHeaders()
        {
            List<string> outBuf = new List<string>(BufferForHeaders[Step - 3]);

            BufferForHeaders.RemoveAt(Step - 3);

            return outBuf;
        }
    }
}
