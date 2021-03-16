using DataGridWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CalcLinearProgramming
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Путь до директории приложения
        /// </summary>
        string ExeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

        /// <summary>
        /// Инициализация главного окна
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выход из программы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// При нажатии кнопки "Справка"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void f1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Инициируем нажатие кнопки F1, что вызывает реагирование helpProvider
            SendKeys.Send("{F1}");
        }

        /// <summary>
        /// При загрузке главного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            // Путь до справки. Для helpProvider
            helpProvider.HelpNamespace = new Uri(Path.Combine(ExeDirectory, "data\\help\\mainpagehelp.html")).LocalPath;

            // Для openFileDialog
            openFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "data\\")).LocalPath;

            // Для saveFileDialog
            saveFileDialog.InitialDirectory = new Uri(Path.Combine(ExeDirectory, "data\\")).LocalPath;

            // Создаём ячейки
            int countVariables = Decimal.ToInt32(numericCountVariables.Value);
            int countRestrictions = Decimal.ToInt32(numericCountRestrictions.Value);
            
            DataGridWorker.CreateGrids(_dataGridViewTargetFunction, countVariables, false);
            DataGridWorker.CreateGrids(_dataGridViewRestrictions, countVariables, countRestrictions, false);

            // Настройки по умолчанию
            radioButtonMin.Checked = true;
            radioButtonStepByStepMode.Checked = true;
        }

        /// <summary>
        /// Обработчик событий при изменении пункта "Добавить угловую точку"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxCornerDot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCornerDot.Checked == true)
            {
                if (_dataGridViewCornerDot.Columns.Count == 0)
                {
                    int countVariables = Decimal.ToInt32(numericCountVariables.Value);
                    DataGridWorker.CreateGrids(_dataGridViewCornerDot, countVariables, true);
                }               

                _dataGridViewCornerDot.Visible = true;
            }
            else
            {
                _dataGridViewCornerDot.Visible = false;
            }
        }

        /// <summary>
        /// Обработчик событий при изменении "Количество переменных"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericCountVariables_ValueChanged(object sender, EventArgs e)
        {
            // Создаём ячейки
            int countVariables = Decimal.ToInt32(numericCountVariables.Value);
            int countRestrictions = Decimal.ToInt32(numericCountRestrictions.Value);

            DataGridWorker.CreateGrids(_dataGridViewTargetFunction, countVariables, true);
            DataGridWorker.CreateGrids(_dataGridViewRestrictions, countVariables, countRestrictions, true);
            DataGridWorker.CreateGrids(_dataGridViewCornerDot, countVariables, true);
        }

        /// <summary>
        /// Обработчик событий при изменении "Количество ограничений"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericCountRestrictions_ValueChanged(object sender, EventArgs e)
        {
            // Создаём ячейки
            int countVariables = Decimal.ToInt32(numericCountVariables.Value);
            int countRestrictions = Decimal.ToInt32(numericCountRestrictions.Value);

            DataGridWorker.CreateGrids(_dataGridViewTargetFunction, countVariables, true);
            DataGridWorker.CreateGrids(_dataGridViewRestrictions, countVariables, countRestrictions, true);
            DataGridWorker.CreateGrids(_dataGridViewCornerDot, countVariables, true);
        }

        /// <summary>
        /// Обработчик событий для кнопкип "Открыть из файла"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    OpenTaskFromPath(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка считывания файла.", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        /// <summary>
        /// Анимация DragDrop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// DragDrop для главного окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length != 0)
            {               
                try
                {
                    // Если закинуто несколько, откроется только первый файл
                    OpenTaskFromPath(files[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка считывания файла.", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        /// <summary>
        /// Открыть задачу из файла
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenTaskFromPath(string fileName)
        {
            string fileText = File.ReadAllText(fileName);

            if (fileText == "")
            {
                throw new Exception("Файл пуст.\nПожалуйста, проверьте файл и попробуйте ещё раз.");                
            }

            // Чистим массив от лишних символов
            fileText = fileText.Replace("\r", "");
            fileText = fileText.Replace("= ", "");

            // Чистим от последнего /n, если пользователь вдруг нажал Enter после уравнения
            if (fileText[fileText.Length - 1] == '\n')
            {
                fileText = fileText.Remove(fileText.Length - 1);
            }

            // Чистим от лишних пробелов
            fileText = string.Join(" ", fileText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            // Определяем макс или мин и очищаем от этого строку
            if (fileText.Contains("->max") || fileText.Contains("-> max"))
            {
                radioButtonMax.Checked = true;
                fileText = fileText.Replace(" ->max", "");
                fileText = fileText.Replace(" -> max", "");
            }
            else
            {
                if (fileText.Contains("->min") || fileText.Contains("-> min"))
                {
                    radioButtonMin.Checked = true;
                    fileText = fileText.Replace(" ->min", "");
                    fileText = fileText.Replace(" -> min", "");
                }
                else
                {
                    throw new Exception("В файле отсутствует условие максимизации или минимизации. Пожалуйста, установите это условие вручную.");
                }
            }

            // Делим цельный текст на массив строк с уравнениями
            string[] tmpText = fileText.Split('\n');
            
            // Создаём список всего уравнения
            List<List<string>> AllTask = new List<List<string>>();
            // Заносим данные в список
            foreach (string equation in tmpText)
            {
                AllTask.Add(new List<string>(equation.Split(' ')));
            }

            // Делим на целевую и ограничения
            List<List<string>> targetFunction = new List<List<string>>();
            targetFunction.Add(new List<string>());
            targetFunction[0] = AllTask[0];

            // Удаляем целевую функцию из списка
            AllTask.Remove(AllTask.First());

            List<List<string>> restrictions = new List<List<string>>(AllTask);

            // Заносим целевую функцию в Ячейки
            DataGridWorker.SetParamToGrids(_dataGridViewTargetFunction, targetFunction, true);
            // Создаём угловую точку
            DataGridWorker.CreateGrids(_dataGridViewCornerDot, targetFunction[0].Count, true);
            // Ограничения
            DataGridWorker.SetParamToGrids(_dataGridViewRestrictions, restrictions, true);

        }

        /// <summary>
        /// Сохранение задачи в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFileToolStripMenuButton_Click(object sender, EventArgs e)
        {
            // Считываем коэффициенты с ячеек
            List<List<string>> targetFunction = DataGridWorker.ReadGridsFrom(_dataGridViewTargetFunction);
            List<List<string>> restrictions = DataGridWorker.ReadGridsFrom(_dataGridViewRestrictions);

            // В зависимости от выбранного режима добавляем ->max или ->min
            if (radioButtonMax.Checked)
            {
                targetFunction[0].Add("->max");
            }
            else
            {
                targetFunction[0].Add("->min");
            }

            // Добавляем "=" перед последним коэффициентом в ограничения
            foreach (List<string> equation in restrictions)
            {
                equation.Insert(equation.Count - 1, "=");
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                using (TextWriter tw = new StreamWriter(filePath))
                {
                    string joinString = String.Join(" ", targetFunction[0].ToArray());

                    tw.WriteLine(joinString);

                    foreach (List<string> equation in restrictions)
                    {
                        joinString = String.Join(" ", equation.ToArray());
                        tw.WriteLine(joinString);
                    }

                    tw.Close();
                }

                saveFileDialog.FileName = "";
                MessageBox.Show("Файл успешно сохранён.", "Сохранить задачу в файл.", MessageBoxButtons.OK);
            }
        }
        
        /// <summary>
        /// Кнопка "Решить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            // Задача на минимум?
            bool min = radioButtonMin.Checked;
            // Пошаговый режим?
            bool stepByStepModeOn = radioButtonStepByStepMode.Checked;
            // Задана угловая точка?
            bool cornerDotOn = checkBoxCornerDot.Checked;

            // Создаём объект задачи
            LinearProgrammingProblem problem;
            // Инициализируем
            try
            {
                // Данные задачи
                List<List<string>> targetFunction = DataGridWorker.ReadGridsFrom(_dataGridViewTargetFunction);
                List<List<string>> retrictions = DataGridWorker.ReadGridsFrom(_dataGridViewRestrictions);
                List<List<string>> cornerDot;

                // Если включена угловая точка
                if (cornerDotOn)
                {
                    cornerDot = DataGridWorker.ReadGridsFrom(_dataGridViewCornerDot);
                    
                    problem = new LinearProgrammingProblem(targetFunction, retrictions, cornerDot, min);

                    // Использование LINQ
                    var query = problem.CornerDot[0].Where(fraction => !fraction.Equals(new Fraction(0)));
                    if (problem.Restrictions.Count != query.Count())
                    {
                        throw new Exception("Невозможно выразить базис с данной угловой точкой. " +
                            "Количество ненулевых параметров начальной угловой точки не равно количеству линейно независимых строк");
                    }
                }
                else
                {
                    problem = new LinearProgrammingProblem(targetFunction, retrictions, null, min);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при создании задачи!", MessageBoxButtons.OK);
                return;
            }

            // Меню
            // Проверяем режим
            switch (stepByStepModeOn)
            {
                // Если выбран пошаговый режим 
                case true:

                    StepByStepWindow stepByStepWindow = new StepByStepWindow(problem);
                    stepByStepWindow.ShowDialog();

                    break;


                // Если выбран авто-режим
                case false:

                    AutoStepWindow autoStepWindow = new AutoStepWindow(problem);
                    autoStepWindow.ShowDialog();

                    break;
            }
        }

        /// <summary>
        /// Кнопка "О программе"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.ShowDialog();
        }
    }
}
