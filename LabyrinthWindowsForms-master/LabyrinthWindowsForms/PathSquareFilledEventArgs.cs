using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Класс PathSquareFilledEventArgs наследуется от EventArgs и используется
    /// с событием PathSegmentDrawn в MazeLogic и полученным FillPathSquare
    /// метод в форме LabyrinthGameGridForm.
    /// Он содержит точку сетки, которая может быть передана в качестве данных методу observer.

    public class PathSquareFilledEventArgs : EventArgs
	{
		public GridPoint GP { get; set; }
	}
}
