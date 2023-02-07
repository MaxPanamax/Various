using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// /// Класс Grid Point представляет точку сетки лабиринта с методами для
    /// /// определение направления и какой другой точки сетки (если таковая имеется) можно найти в
    /// направления ВВЕРХ, ВПРАВО, ВНИЗ и ВЛЕВО.
    /// 
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>

    public class GridPoint
	{
		public enum Direction { UP, RIGHT, DOWN, LEFT, ERROR }

		private int row = 0, col = 0;
        /// <краткое содержание>
        /// /// Конструктор точки сетки.
        /// </краткое содержание>
        /// <param name="row">строка точки на сетке</param>
        ///  <param name="col">столбец точки на сетке </param>

        public GridPoint(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		public int Row { get { return row; } set { row = value; } }
		public int Column { get { return col; } set { col = value; } }
        /// <краткое содержание>
        /// getDirection возвращает направление к другой точке, которая, как предполагается находится на перпендикулярном смещении от этого объекта.
        /// </краткое содержание>
        /// <param name="target">целевая точка сетки для сравнения с</param>
        /// <returns></returns>

        public GridPoint.Direction GetDirection(GridPoint target)
		{
			if (target.row < row) return Direction.UP;
			else if (target.row > row) return Direction.DOWN;
			else if (target.col < col) return Direction.LEFT;
			else if (target.col > col) return Direction.RIGHT;
			else
				return Direction.ERROR;
		}
        /// <краткое содержание>
        /// /// Перемещает точку сетки на один шаг в заданном направлении.
        /// </краткое содержание>
        /// <param name="d">направление, определенное перечислением направлений</param>

        public void Move(Direction d)
		{
			switch (d)
			{
				case Direction.UP: row--; break;
				case Direction.RIGHT: col++; break;
				case Direction.DOWN: row++; break;
				case Direction.LEFT: col--; break;
			}
		}
	}
}
