using DataGridWork;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalcLinearProgramming
{
    /// <summary>
    /// Представляет симплекс таблицу
    /// </summary>
    public class SimplexTable
    {
        /// <summary>
        /// Список координат всех опорных элементов
        /// </summary>
        private List<List<int>> SupportElementsCoordinates;

        /// <summary>
        /// Коэффициенты симплекс таблицы
        /// </summary>
        public List<List<Fraction>> SimplexTableElements;

        /// <summary>
        /// Разрешающая строка.
        /// </summary>
        int RowOfTheSupportElement;

        /// <summary>
        /// Разрешающий столбец.
        /// </summary>
        int ColumnOfTheSupportElement;

        /// <summary>
        /// Текущий опорный элемент
        /// </summary>
        Fraction SupportingElement;

        /// <summary>
        /// Буффер для хэдэров колонок и строк
        /// </summary>
        private List<List<int>> bufferVarV = new List<List<int>>();

        public SimplexTable()
        {
            SimplexTableElements = new List<List<Fraction>>();
        }

        /// <summary>
        /// Создаёт симплекс таблицу по текущим ячейкам
        /// </summary>
        /// <param name="grid"></param>
        public SimplexTable(DataGridView grid)
        {
            SimplexTableElements = new List<List<Fraction>>();

            // Заполнение внутреннего объекта матрицы симплекс таблицы
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                SimplexTableElements.Add(new List<Fraction>());
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    //добавляем в массив число
                    SimplexTableElements[i].Add((Fraction)grid.Rows[i].Cells[j].Value);
                }
            }
        }

        /// <summary>
        /// Проверка симплекс таблицы на решение
        /// </summary>
        /// <returns>Возвращает код состояния таблицы:</returns>
        /// <returns>-1: Задача неразрешима</returns>
        /// <returns>1: Ответ готов</returns>
        /// <returns>0: Продолжение поиска решения</returns>
        public int ResponseCheck()
        {
            //неразрешимо?
            bool insoluble = false;
            for (int j = 0; j < SimplexTableElements[0].Count - 1; j++)
            {
                if (SimplexTableElements[SimplexTableElements.Count - 1][j] < new Fraction(0)) // Проходимся по последней строке. Если нашли отрицательный, то...
                {
                    // Предполагаем, что она неразрешима, и смотрим дальше...
                    insoluble = true;
                    for (int i = 0; i < SimplexTableElements.Count - 1; i++) // Проходимся по всем элеметам, кроме последней(f) строки и (d)столбца
                    {
                        if (SimplexTableElements[i][j] > 0)
                        {
                            // Если найден хоть один положительный элемент, считаем симплекс таблицу разрешимой
                            insoluble = false;
                            break;
                        }
                        // Если все элементы отрицательные - оставляем её неразрешимой
                    }
                }
                //неразрешима
                if (insoluble)
                    return -1;
            }

            //предполагаем, что нет отрицательных элементов в последней строке
            insoluble = true;
            //проверяем
            for (int j = 0; j < SimplexTableElements[0].Count - 1; j++) // Проходимся по последней (f) строке
                if (SimplexTableElements[SimplexTableElements.Count - 1][j] < 0)
                {
                    //если есть отрицательный элемент, то ищем решение дальше
                    insoluble = false;
                    break;
                }
            //есть ответ
            if (insoluble)
                return 1;

            //продолжение поиска решения
            return 0;
        }

        /// <summary>
        /// Возвращает ответ симплекс таблицы
        /// </summary>
        /// <returns>Возвращает ответ симплекс таблицы</returns>
        public Fraction Response()
        {
            return SimplexTableElements[SimplexTableElements.Count - 1][SimplexTableElements[0].Count - 1];
        }

        /// <summary>
        /// Поиск всех опорных элементов и выделение их ячейки цветом на DataGridView
        /// </summary>
        /// <param name="grid"></param>
        public void SelectionOfTheSupportElements(DataGridView grid)
        {
            //координаты минимального элемента в столбце
            int[] minimum = new int[2];
            SupportElementsCoordinates = new List<List<int>>();
            int index = 0; //для счёта координат

            //ищем отрицательный элемент в последней строке
            for (int j = 0; j < SimplexTableElements[0].Count - 1; j++)
            {
                if (SimplexTableElements[SimplexTableElements.Count - 1][j] < 0)
                {

                    minimum[0] = -1;
                    minimum[1] = -1;
                    //ищем подходящий не отрицательный элемент в столбце
                    for (int i = 0; i < SimplexTableElements.Count - 1; i++)
                    {
                        if (SimplexTableElements[i][j] > 0)
                        {
                            // Если минимального не встречалось - назначем
                            if ((minimum[0] == -1) && (minimum[1] == -1))
                            {
                                minimum[0] = i;
                                minimum[1] = j;
                            }
                            // Если минимальный уже есть - сравниваем
                            else if ((SimplexTableElements[minimum[0]][SimplexTableElements[0].Count - 1] / SimplexTableElements[minimum[0]][minimum[1]]) > (SimplexTableElements[i][SimplexTableElements[0].Count - 1] / SimplexTableElements[i][j]))
                            {
                                minimum[0] = i;
                                minimum[1] = j;
                            }
                        }
                    }

                    //если есть минимальный, то делаем его подсвеченным
                    if ((minimum[0] != -1) && (minimum[1] != -1))
                    {
                        grid.Rows[minimum[0]].Cells[minimum[1]].Style.BackColor = System.Drawing.Color.GreenYellow;

                        //координаты возможного опорного элемента
                        SupportElementsCoordinates.Add(new List<int>());
                        SupportElementsCoordinates[index].Add(minimum[0]);
                        SupportElementsCoordinates[index].Add(minimum[1]);
                        index++;
                    }
                }
            }
        }

        /// <summary>
        /// Выбор первого попавшегося опорного элемента
        /// </summary>
        public void SelectFirstSupportElement()
        {
            //координаты минимума в столбце
            int[] minimum = new int[2];

            //ищем отрицательный элемент в последней строке
            for (int j = 0; j < SimplexTableElements[0].Count - 1; j++)
            {
                if (SimplexTableElements[/*simplextablegrid.RowDefinitions.Count - 2*/SimplexTableElements.Count - 1][j] < 0)
                {
                    minimum[0] = -1;
                    minimum[1] = -1;
                    //ищем подходящий не отрицательный элемент в столбце
                    for (int i = 0; i < /*simplextablegrid.RowDefinitions.Count - 1*/SimplexTableElements.Count - 1; i++)
                    {
                        if (SimplexTableElements[i][j] > 0)
                        {
                            if ((minimum[0] == -1) && (minimum[1] == -1))
                            {
                                minimum[0] = i;
                                minimum[1] = j;
                            }
                            else if ((SimplexTableElements[minimum[0]][SimplexTableElements[0].Count - 1] 
                                / SimplexTableElements[minimum[0]][minimum[1]]) 
                                > (SimplexTableElements[i][SimplexTableElements[0].Count - 1] 
                                / SimplexTableElements[i][j]))
                            {
                                minimum[0] = i;
                                minimum[1] = j;
                            }
                        }
                    }
                    //если есть минимальный элемент, то делаем его опорным
                    if ((minimum[0] != -1) && (minimum[1] != -1))
                    {
                        RowOfTheSupportElement = minimum[0];
                        ColumnOfTheSupportElement = minimum[1];
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Проверяем, выбран ли опорный элемент
        /// </summary>
        public void SupportElementPressedOrNot(DataGridView grid)
        {
            for (int i = 0; i < SupportElementsCoordinates.Count; i++)
            {
                if (grid.Rows[SupportElementsCoordinates[i][0]].Cells[SupportElementsCoordinates[i][1]] == grid.CurrentCell)
                {
                    RowOfTheSupportElement = SupportElementsCoordinates[i][0];
                    ColumnOfTheSupportElement = SupportElementsCoordinates[i][1];
                    return;
                }
            }
            throw new Exception("Не выбран опорный элемент");
        }

        /// <summary>
        /// По текущему опорному элементу меняем Хэдэры и буферизируем их, чтобы можно было вернуть обратно
        /// </summary>
        /// <param name="grid"></param>
        public void ChangeHeaders(DataGridView grid)
        {
            string tmp_x;

            // Буферизация координат опорного элемента до смены переменных местами - по этим координатам мы сможем поменять элементы обратно, если это потребуется
            bufferVarV.Add(new List<int>());
            bufferVarV[bufferVarV.Count - 1].Add(RowOfTheSupportElement);
            bufferVarV[bufferVarV.Count - 1].Add(ColumnOfTheSupportElement);

            // Меняем названия переменных местами
            tmp_x = (string)grid.Rows[RowOfTheSupportElement].HeaderCell.Value;
            grid.Rows[RowOfTheSupportElement].HeaderCell.Value = grid.Columns[ColumnOfTheSupportElement].HeaderText;
            grid.Columns[ColumnOfTheSupportElement].HeaderText = tmp_x;
        }

        /// <summary>
        /// меняем Хэдэры доставая данные из буффера
        /// </summary>
        /// <param name="grid"></param>
        public void ChangeHeadersFromBuffer(DataGridView grid)
        {
            string tmpX;
            int[] tmpCoordination = new int[2];

            // Достаём из буфера последние координаты опорного элемента
            tmpCoordination[0] = bufferVarV[bufferVarV.Count - 1][0];
            tmpCoordination[1] = bufferVarV[bufferVarV.Count - 1][1];

            // Меняем названия переменных местами
            tmpX = (string)grid.Rows[tmpCoordination[0]].HeaderCell.Value;
            grid.Rows[tmpCoordination[0]].HeaderCell.Value = grid.Columns[tmpCoordination[1]].HeaderText;
            grid.Columns[tmpCoordination[1]].HeaderText = tmpX;

            bufferVarV.RemoveAt(bufferVarV.Count - 1);
        }

        /// <summary>
        /// Вычисление симплекс таблицы согласно текущему опорному элементу
        /// </summary>
        public void CalculateSimplexTable()
        {
            //записываем значение опорного элемента
            SupportingElement = SimplexTableElements[RowOfTheSupportElement][ColumnOfTheSupportElement];

            //вычисление остальных строк сиплекс-таблицы

            for (int i = 0; i < SimplexTableElements.Count; i++)
            {
                if (i != RowOfTheSupportElement) // Исключаем строку с опорным элементом
                {
                    for (int j = 0; j < SimplexTableElements[0].Count; j++)
                    {
                        if (j != ColumnOfTheSupportElement) // Исключаем колонку с опорным элементом
                        {
                            // вычисляем i-тый, j-тый элемент по формуле.
                            SimplexTableElements[i][j] = (((SimplexTableElements[i][j] 
                                * SimplexTableElements[RowOfTheSupportElement][ColumnOfTheSupportElement]) 
                                - (SimplexTableElements[RowOfTheSupportElement][j] * SimplexTableElements[i][ColumnOfTheSupportElement])) 
                                / SupportingElement).Reduce();
                        }
                    }
                }
            }

            //вычисление на месте опорного
            SimplexTableElements[RowOfTheSupportElement][ColumnOfTheSupportElement] = 1 / SupportingElement; // на место опорного элемента подставляем 1 поделить на опорный

            // Стоит после вычисления всех строк, потому что домнажать по формуле мы должны на старые значения, а не новые.
            //вычисление разрешающей строки
            for (int j = 0; j < SimplexTableElements[0].Count; j++) // Проходимся столько раз, сколько ширина симплекс таблицы (слева направо)
            {
                if (j != ColumnOfTheSupportElement) // Если текущая колонка не с опорным элементом, то делаем...
                {
                    SimplexTableElements[RowOfTheSupportElement][j] /= SupportingElement; // в строке с опорным элементом делим ячейку на опорный

                }
            }

            //вычисление разрешающего столбца
            for (int i = 0; i < SimplexTableElements.Count; i++) // Проходимся столько раз, сколько высота симплекс таблицы (сверху вниз)
            {
                if (i != RowOfTheSupportElement) // Если текущий элемент не в строке опорного, то
                {
                    SimplexTableElements[i][ColumnOfTheSupportElement] /= SupportingElement * (-1); // в колонке опорного делим ячейку на опорный домноженную на -1
                }
            }
        }


    }
}
