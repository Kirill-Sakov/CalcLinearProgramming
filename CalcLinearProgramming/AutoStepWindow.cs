using DataGridWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcLinearProgramming
{
    public partial class AutoStepWindow : Form
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
        /// Вспомогательная переменная, которая определяет была ли добавлена угловая точка для ответа.
        /// </summary>
        private bool cornerDotAnswerWasAdded;

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public AutoStepWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для авто-режима с задачей
        /// </summary>
        /// <param name="problem"></param>
        public AutoStepWindow(LinearProgrammingProblem problem)
        {
            this.Problem = problem;

            // Инициализируем компоненты окна
            InitializeComponent();

            // Заносим параметры в ячейки
            DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.Restrictions, true);

            if (Problem.CornerDot != null)
            {
                Problem.TransformColumnsForCornerDot(_dataGridViewProblem);
            }

            //Процесс выполнения.
            Implementation();
        }

        /// <summary>
        /// Основные шаги
        /// </summary>
        public void Implementation()
        {
            try
            {
                //Прямой ход метода Гаусса для приведения к треугольному виду.
                Problem.Gauss();
                //Выражение базисных переменных и приведение к диагональному виду.
                Problem.HoistingMatrix();
                // Обновляем параметры в ячейках
                DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.Restrictions, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Ошибка при решении системы методом Гаусса.", MessageBoxButtons.OK);
                return;
            }
            
            try
            {
                // Создаём симплекс таблицу
                Problem.TransformGridForSimplexTable(_dataGridViewProblem);
                Problem.SimplexTable = new SimplexTable(_dataGridViewProblem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Ошибка создания симплекс таблицы!", MessageBoxButtons.OK);
                return;
            }

            int responce;
            int step = 1;

            while (true)
            {
                // Проверяем симплекс таблицу на решённость
                if ((responce = Problem.SimplexTable.ResponseCheck()) == 0)
                {
                    try
                    {
                        //выбор любого опорного
                        Problem.SimplexTable.SelectFirstSupportElement();
                        //меняем местами переменные 
                        Problem.SimplexTable.ChangeHeaders(_dataGridViewProblem);
                        // высчитывание по опорному
                        Problem.SimplexTable.CalculateSimplexTable();
                        // Обновление ячеек
                        DataGridWorker.SetParamToGrids(_dataGridViewProblem, Problem.SimplexTable.SimplexTableElements, false);

                        step++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"Ошибка при выборе опорных элементов на {step} шаге.", MessageBoxButtons.OK);
                        return;
                    }
                    
                }
                else if (responce == 1)
                {
                    tabControl.TabPages[0].Text = "Ответ готов!";

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

                    break;
                }

                else if (responce == -1)
                {
                    MessageBox.Show("Задача не разрешима!");
                    tabControl.TabPages[0].Text = "Задача не разрешима!";
                    break;
                }
            }
        }

        /// <summary>
        /// При загрузке главного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoStepWindow_Load(object sender, EventArgs e)
        {
            // Путь до справки. Для helpProvider
            helpProvider.HelpNamespace = new Uri(Path.Combine(ExeDirectory, "data\\help\\automodehelp.html")).LocalPath;
        }
    }
}
