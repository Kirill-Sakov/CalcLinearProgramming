using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataGridWork
{
    public class DataGridWorker
    {
        /// <summary>
        /// Создание ячеек для ограничений
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="countVariables">Количество переменных</param>
        /// <param name="countRestrictions">Количество ограничений</param>
        /// <param name="clear">Очищать перед созданием</param>
        public static void CreateGrids(DataGridView dataGridView, int countVariables, int countRestrictions, bool clear)
        {
            if (clear)
                dataGridView.Columns.Clear();

            for (int i = 0; i < countVariables + 1; i++)
            {
                dataGridView.Columns.Add($"ogr_x{i + 1}", $"x{i + 1}");

                if (i == countVariables) // именуем последний эл
                {
                    dataGridView.Columns[i].HeaderText = "d";
                }
            } // Создаём столбцы

            for (int i = 1; i < countRestrictions + 1; i++)
            {
                dataGridView.Rows.Insert(0, "0");
            } // Создаём строки


            for (int i = 0; i < countVariables + 1; i++)
            {
                for (int j = 0; j < countRestrictions; j++)
                    dataGridView.Rows[j].Cells[i].Value = "0";
            }

            // Делаем колонки несортируемыми
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Создание ячеек для целевой функции и угловой точки
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="countVariables">Количество переменных</param>
        /// <param name="clear">Очищать перед созданием</param>
        public static void CreateGrids(DataGridView dataGridView, int countVariables, bool clear)
        {
            countVariables--;

            if (clear)
                dataGridView.Columns.Clear();

            // Создаём столбцы
            for (int i = 0; i < countVariables + 1; i++)
            {
                dataGridView.Columns.Add($"ogr_x{i + 1}", $"x{i + 1}");
            }

            // Добавляем одну строку
            dataGridView.Rows.Insert(0, "0");

            // Заполняем ячейки нулями
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Rows[0].Cells[i].Value = "0";
            }

            // Делаем колонки несортируемыми
            foreach (DataGridViewColumn dgvc in dataGridView.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Добавление коэффициентов уравнений в ячейки dataGridView
        /// </summary>
        /// <param name="grid">dataGridView в который необходимо добавить</param>
        /// <param name="equation">матрица, которую необходимо добавить</param>
        /// <param name="clear">очищать dataGrid перед добавлением</param>
        public static void SetParamToGrids(DataGridView grid, List<List<string>> equations, bool clear)
        {
            if (clear)
                grid.Columns.Clear();

            // Создаём колонки
            while (equations[0].Count > grid.ColumnCount)
            {
                grid.Columns.Add($"x{grid.ColumnCount + 1}", $"x{grid.ColumnCount + 1}");
            }
            // Ставим Хэдер "d" в последнюю колонку
            if (equations[0].Count == grid.ColumnCount)
            {
                grid.Columns[grid.ColumnCount - 1].HeaderText = "d";
            }
            // Добавляем уравнения
            foreach(List<string> eq in equations)
            {
                grid.Rows.Add(eq.ToArray());
            }            

            // Делаем колонки несортируемыми
            foreach (DataGridViewColumn dgvc in grid.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Добавление коэффициентов уравнений в ячейки dataGridView
        /// </summary>
        /// <param name="grid">dataGridView в который необходимо добавить</param>
        /// <param name="equations">матрица, которую необходимо добавить</param>
        /// <param name="clear">очищать dataGrid перед добавлением</param>
        public static void SetParamToGrids(DataGridView grid, List<List<Fraction>> equations, bool clear)
        {
            if (clear)
                grid.Columns.Clear();

            // Создаём колонки
            while (equations[0].Count > grid.ColumnCount)
            {
                grid.Columns.Add($"x{grid.ColumnCount + 1}", $"x{grid.ColumnCount + 1}");
            }

            // Ставим Хэдер "d" в последнюю колонку
            if (equations[0].Count == grid.ColumnCount)
            {
                grid.Columns[grid.ColumnCount - 1].HeaderText = "d";
            }

            // Сокращаем все возможные дроби
            for (int i = 0; i < equations.Count; i++)
            {
                for (int j = 0; j < equations[i].Count; j++)
                {
                    equations[i][j] = equations[i][j].Reduce();
                }
            }

            if (clear)
            {
                // Добавляем уравнения
                foreach (List<Fraction> eq in equations)
                {
                    grid.Rows.Add(eq.ToArray());
                }
            }
            else
            {
                // Если мы не очищали перед добавлением, изменяем элементы по ячейке
                for (int i = 0; i < equations.Count; i++)
                {
                    for (int j = 0; j < equations[i].Count; j++)
                    {
                        grid.Rows[i].Cells[j].Value = equations[i][j];
                    }
                }
            }

            // Делаем колонки несортируемыми
            foreach (DataGridViewColumn dgvc in grid.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// Считать данные с ячеек dataGridView
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static List<List<string>> ReadGridsFrom(DataGridView grid)
        {
            List<List<string>> readedGrids = new List<List<string>>();

            for (int i = 0; i < grid.Rows.Count; i++)
            {
                readedGrids.Add(new List<string>());
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    if (grid.Rows[i].Cells[j].Value == null)
                        throw new Exception("Один или несколько элементов не заполнены. Пожалуйста, попробуйте ещё раз.");

                    readedGrids[i].Add(grid.Rows[i].Cells[j].Value.ToString().Trim());
                }
            }

            return readedGrids;
        }


        public static void SetHeaders(DataGridView grid, List<string> headers)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].HeaderText = headers[i];
            }
        }
    }
}
