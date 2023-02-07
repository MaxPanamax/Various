using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Класс LineDrawnEventArgs наследуется от EventArgs и используется с
    /// Событие SquareWallRemoved в MazeLogic и получено методом RemoveLine
    /// в форме лабиринтной сетки.
    /// Он содержит четыре целых числа для 2 пар столбцов и строк и цвет
    /// которые могут быть переданы в качестве данных методу observer. Линия предназначена для того, чтобы быть
    /// нарисовано от координат C1,R1 до координат C2,R2 указанным цветом.
    /// </краткое содержание>
    public class LineDrawnEventArgs : EventArgs
	{
		public int C1 { get; set; }
		public int R1 { get; set; }
		public int C2 { get; set; }
		public int R2 { get; set; }
		public System.Drawing.Color color { get; set; }
	}
}
