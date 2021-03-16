using DataGridWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalcLinearProgramming
{
    /// <summary>
    /// Представляет задачу линейного программирования
    /// </summary>
    public class LinearProgrammingProblem
    {
        /// <summary>
        /// Матрица целевой функции
        /// </summary>
        public List<List<Fraction>> TargetFunction;

        /// <summary>
        /// Матрица ограничений
        /// </summary>
        public List<List<Fraction>> Restrictions;

        /// <summary>
        /// Матрица угловой точки
        /// </summary>
        public List<List<Fraction>> CornerDot;

        /// <summary>
        /// Задача на минимум?
        /// </summary>
        public bool Min;

        /// <summary>
        /// Задана угловая точка?
        /// </summary>
        bool CornerDotIsOn;

        /// <summary>
        /// Ранг матрицы
        /// </summary>
        public int Rang { get; private set; }
        
        /// <summary>
        /// Количество базисных перменных
        /// </summary>
        protected int NumberOfBasixPermutations;

        /// <summary>
        /// Количество свободных переменных
        /// </summary>
        protected int NumberOfFreeVariables;

        /// <summary>
        /// Симплекс таблица
        /// </summary>
        public SimplexTable SimplexTable;

        /// <summary>
        /// Конструктор задачи
        /// </summary>
        /// <param name="targetFunction">Целевая функция</param>
        /// <param name="retrictions">Ограничения</param>
        /// <param name="cornerDot">Угловая точка</param>
        /// <param name="min">bool: задача на минимум?</param>
        public LinearProgrammingProblem(List<List<string>> targetFunction, List<List<string>> retrictions, List<List<string>> cornerDot, bool min)
        {
            this.TargetFunction = ToFractionList(targetFunction);
            this.Restrictions = ToFractionList(retrictions);

            if (cornerDot != null)
            {
                this.CornerDot = ToFractionList(cornerDot);
                this.CornerDotIsOn = true;
            }
            else
                this.CornerDotIsOn = false;

            this.Min = min;

            this.Rang = RangOfMatix();

            // Количество базисных переменных.
            NumberOfBasixPermutations = Rang;
            
            // Вычисляем количество свободных переменных
            NumberOfFreeVariables = TargetFunction[0].Count - NumberOfBasixPermutations;
        }

        /// <summary>
        /// Функция для подсчёта ранга матрицы
        /// </summary>
        /// <returns>Возвращает ранг матрицы</returns>
        private int RangOfMatix()
        {
            //вспомогательный массив для подсчёта ранга для обыкновенных дробей
            List<List<Fraction>> copy_elements = new List<List<Fraction>>();

            // Копируем элементы в вспомогательный массив
            for (int i = 0; i < Restrictions.Count; i++)
            {
                copy_elements.Add(new List<Fraction>());
                for (int j = 0; j < Restrictions[0].Count; j++)
                {
                    copy_elements[i].Add(Restrictions[i][j]);
                }
            }

            Fraction first_elem = new Fraction(0);

            for (int i = 0; i < copy_elements.Count; i++)
            {
                int j = 0;
                first_elem = copy_elements[i][j];
                //находим ненулевой элемент в строке. если такого нет, то удаляем строку из нулей
                if (first_elem == 0)
                {
                    j = 1;
                    while (first_elem == 0)
                    {
                        first_elem = copy_elements[i][j];
                        j++;
                        //если не нашли не нулевого, то удаляем строку из нулей
                        if (j == copy_elements[0].Count)
                        {
                            copy_elements.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                    j--;
                }

                //удалось найти не нулевой
                if (first_elem != 0)
                {
                    for (int p = 0; p < copy_elements[0].Count; p++)
                        copy_elements[i][p] /= first_elem;
                    for (int k = i + 1; k < copy_elements.Count; k++)
                    {
                        if (copy_elements[k][j] != 0)
                        {
                            Fraction first_elem_1 = copy_elements[k][j];
                            for (int m = 0; m < copy_elements[0].Count; m++)
                            {
                                copy_elements[k][m] = copy_elements[k][m] - copy_elements[i][m] * first_elem_1;
                            }
                        }
                    }
                }
            }

            return copy_elements.Count;
        }

        /// <summary>
        /// Меняет колонки местами для смены базиса
        /// </summary>
        /// <param name="dataGridViewProblem"></param>
        internal void TransformColumnsForCornerDot(DataGridView grid)
        {
            int column_index = 0;
            string tmpVar;

            for (int j = 0; j < CornerDot[0].Count; j++)
            {
                // Еесли встретили ненулевой элемент корневой точки
                if (CornerDot[0][j] != 0)
                {
                    Fraction[] tmp_fractions = new Fraction[Restrictions.Count];

                    // Меняем местами столбцы
                    for (int i = 0; i < Restrictions.Count; i++)
                    {
                        // Копируем первый столбец
                        tmp_fractions[i] = Restrictions[i][column_index];
                    }

                    for (int i = 0; i < Restrictions.Count; i++)
                    {
                        // заносим на место первого j-тый - тот, на котором на встретился ненулевой элемент корневой точки
                        Restrictions[i][column_index] = Restrictions[i][j];
                        grid.Rows[i].Cells[column_index].Value = Restrictions[i][j];
                    }

                    for (int i = 0; i < Restrictions.Count; i++)
                    {
                        // Из копии достаём и ставим
                        Restrictions[i][j] = tmp_fractions[i];
                        grid.Rows[i].Cells[j].Value = tmp_fractions[i];
                    }

                    // Меняем местами хэдэры в dataGrid
                    tmpVar  = grid.Columns[column_index].HeaderText;
                    grid.Columns[column_index].HeaderText = grid.Columns[j].HeaderText;
                    grid.Columns[j].HeaderText = tmpVar;

                    column_index++;
                }
            }
        }

        /// <summary>
        /// Преобразовать string матрицу задачи в Fraction матрицу
        /// </summary>
        /// <param name="stringList"></param>
        /// <returns></returns>
        public List<List<Fraction>> ToFractionList(List<List<string>> stringList)
        {
            List<List<Fraction>> fractionList = new List<List<Fraction>>();
        
            for (int i = 0; i < stringList.Count; i++)
            {
                fractionList.Add(new List<Fraction>());

                foreach (string coef in stringList[i])
                {
                    string[] tmp_fraction = new string[2];
                    
                    tmp_fraction = (coef.Split('/'));

                    if (tmp_fraction.Length == 1)
                        tmp_fraction = new string[] { tmp_fraction[0], "1" };

                    fractionList[i].Add(new Fraction(Int32.Parse(tmp_fraction[0]), Int32.Parse(tmp_fraction[1])));
                }
            }

            return fractionList;
        }

        /// <summary>
        /// Метод Гаусса
        /// </summary>
        /// <param name="сornerDotOn">Задана угловая точка?</param>
        public void Gauss()
        {
            for (int global = 0; global < this.Restrictions.Count; global++)
            {
                for (int i = global; i < this.Restrictions.Count; i++)
                {
                    if (i == global)
                    {
                        //проверяем возможность выражения переменной
                        Fraction first_elem = this.Restrictions[i][global];
                        bool responce = true; //можно ли вообще выразить
                        if (first_elem == 0)
                        {
                            responce = false;
                            for (int k = i + 1; k < this.Restrictions.Count; k++)
                                if (this.Restrictions[k][global] != 0)
                                {
                                    responce = true;
                                    first_elem = this.Restrictions[k][global];
                                    Fraction temp;
                                    //смена строк
                                    for (int j = 0; j < this.Restrictions[0].Count; j++)
                                    {
                                        temp = this.Restrictions[i][j];
                                        this.Restrictions[i][j] = this.Restrictions[k][j];
                                        this.Restrictions[k][j] = temp;
                                    }
                                    break;
                                }
                        }

                        //если не получилось выразить переменную и была задана начальная угловая точка
                        if ((responce == false) && (this.CornerDotIsOn == true))
                            throw new Exception("Невозможно выразить одну или несколько базисных переменных. Возможно неверно введены коэффициенты или угловая точка.");
                        //если не получилось выразить переменную и НЕ была задана начальная угловая точка
                        else if ((responce == false) && (this.CornerDotIsOn == false))
                        {
                            //то ищем в других столбцах
                            bool check = false;
                            for (int column = global + 1; column < this.Restrictions[0].Count; column++)
                            {
                                for (int row = i; row < this.Restrictions.Count; row++)
                                {
                                    if (this.Restrictions[row][column] != 0)
                                    {
                                        check = true;
                                        first_elem = this.Restrictions[row][column];
                                        Fraction temp; //вспомогательная переменная

                                        //смена строк
                                        for (int j = 0; j < this.Restrictions[0].Count; j++)
                                        {
                                            //для элементов матрицы
                                            temp = this.Restrictions[i][j];
                                            this.Restrictions[i][j] = this.Restrictions[row][j];
                                            this.Restrictions[row][j] = temp;
                                        }

                                        //смена столбцов
                                        for (int k = 0; k < this.Restrictions.Count; k++)
                                        {
                                            //для элементов матрицы
                                            temp = this.Restrictions[k][global];
                                            this.Restrictions[k][global] = this.Restrictions[k][column];
                                            this.Restrictions[k][column] = temp;
                                        }
                                    }

                                    if (check)
                                        break;
                                }
                                if (check)
                                    break;
                            }

                            //Такого случая возможно не может быть. Поэтому это излишне.
                            if (check == false)
                                throw new Exception("Невозможно выразить переменные. Возможно неверно введены коэффициенты.");
                        }



                        for (int j = 0; j < this.Restrictions[0].Count; j++)
                            this.Restrictions[i][j] /= first_elem;
                    }
                    else
                    {
                        Fraction first_elem = this.Restrictions[i][global];
                        for (int j = 0; j < this.Restrictions[0].Count; j++)
                        {
                            this.Restrictions[i][j] -= this.Restrictions[global][j] * first_elem;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Выражение базисных переменных + обратный ход Гаусса
        /// </summary>
        public void HoistingMatrix()
        {
            for (int global = 1; global < NumberOfBasixPermutations; global++)
            {
                for (int i = global - 1; i >= 0; i--)
                {
                    Fraction first_elem = Restrictions[i][global];
                    for (int j = global; j < Restrictions[0].Count; j++)
                    {
                        Restrictions[i][j] -= Restrictions[global][j] * first_elem;
                    }
                }
            }
        }
        
        /// <summary>
        /// Трансформирует текущий DataGridView в форму симплекс таблицы
        /// </summary>
        /// <param name="grid"></param>
        public void TransformGridForSimplexTable(DataGridView grid)
        {

            Fraction a;
            //счёт столбца
            int column_index = 1;

            // Отображаем таблицу в её естественном виде // убираем базис и переносим его в левую колонку
            string now_basix_name = "";

            // Алгоритм определяющий единственные единицы в столбцах
            for (int j = 0; j < grid.Columns.Count - 1; j++) // Проходимся по всем колонкам
            {
                a = new Fraction(0);
                column_index = 0;
                bool only_one_in_column = false; // Единственная единица в столбце

                for (int i = 0; i < grid.Rows.Count; i++) // Проходимся по всем элементам в колонке, кроме последнего(т.е. кроме целевой функции)
                {
                    a += (Fraction)grid.Rows[i].Cells[j].Value;

                    // Если нам встречается единица, считаем, что колонка базисная
                    if ((Fraction)grid.Rows[i].Cells[j].Value == new Fraction(1))
                    {
                        now_basix_name = grid.Columns[j].HeaderText;
                        column_index = i;
                        only_one_in_column = true;
                    }
                    // Если встретили отличное от единицы и это не ноль, смотрим, встречалась ли нам единица раньше. Если встречалась - значит считаем колонку НЕ базисной
                    else if (only_one_in_column == true && !((Fraction)grid.Rows[i].Cells[j].Value == new Fraction(0)))
                    {
                        only_one_in_column = false;
                        break;
                    }

                    // Если дошли до последнего элемента в столбце и сумма всех не равна единице, то считаем НЕ базисной
                    if ((i == grid.Rows.Count - 1) && (a != 1))
                    {
                        only_one_in_column = false;
                        break;
                    }
                }
                if (only_one_in_column)
                {
                    grid.Rows[column_index].HeaderCell.Value = now_basix_name;
                    grid.Columns.Remove(grid.Columns[j]);
                    j--;
                }
            }

            column_index = 0;
            // считаем коэффициенты последней строки
            grid.Rows.Add();
            grid.Rows[NumberOfBasixPermutations].HeaderCell.Value = "F";
            for (int j = NumberOfBasixPermutations; j < Restrictions[0].Count - 1; j++)
            {
                //логика
                a = new Fraction(0);
                for (int i = 0; i < Restrictions.Count; i++)
                {
                    a += Restrictions[i][j] * TargetFunction[0][Int32.Parse(grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                }
                a -= TargetFunction[0][Int32.Parse(grid.Columns[column_index].HeaderCell.Value.ToString().Replace("d", column_index.ToString()).Trim('x')) - 1];

                ////отображение

                grid.Rows[NumberOfBasixPermutations].Cells[column_index].Value = a;
                column_index++;
            }

            //коэффициент в нижнем правом углу симплекс таблицы
            a = new Fraction(0);
            for (int i = 0; i < Restrictions.Count; i++)
                a += Restrictions[i][Restrictions[0].Count - 1] * TargetFunction[0][Int32.Parse(grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];

            grid.Rows[grid.Rows.Count - 1].Cells[grid.Columns.Count - 1].Value = a;
        }

        /// <summary>
        /// Возвращает текущую угловую точку
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public List<List<string>> ResponseCornerDot(DataGridView grid)
        {
            //угловая точка соответствующая решению 
            string[] finish_corner_dot = new string[TargetFunction[0].Count];

            // по умолчанию элементы - 0
            for (int i = 0; i < finish_corner_dot.Length; i++)
                finish_corner_dot[i] = "0";

            //вспомогательная переменная
            int temp;

            //заполняем коэффициентами
            for (int i = 0; i < grid.Rows.Count - 1; i++)
            {
                temp = Int32.Parse(grid.Rows[i].HeaderCell.Value.ToString().Trim('x'));
                finish_corner_dot[temp - 1] = (SimplexTable.SimplexTableElements[i][SimplexTable.SimplexTableElements[0].Count - 1].ToString());
            }

            // Преобразование string[] в List<List<string>>
            List<string> tmpList = finish_corner_dot.ToList();
            var tmpListList = new List<List<string>>();
            tmpListList.Add(tmpList);

            return tmpListList;
        }

        
    }
}
