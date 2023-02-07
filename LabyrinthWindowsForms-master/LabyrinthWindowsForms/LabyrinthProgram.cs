using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Программа Labyrinth запускает приложение WindowsForms, в котором пользователь может ввести строку и
    /// столбец, и создается случайный лабиринт, который затем может быть проинструктирован программой для решения
    /// для кратчайшего пути.
    /// 
    /// Это адаптация школьного проекта на C#, написанного на Java. Версии Java для
    /// MazeLogic и ExtendedGraph были написаны Робином Осборном и Эммой Дирнбергер.
    /// Непересекающиеся наборы, график, точка сетки и пара были частью примера Java-кода, предоставленного
    /// Марком Алленом Вайсом и Уно Холмером.
    /// 
    /// IPriorityQueue и PriorityQueue являются непроверенными реализациями примера кода C# с помощью
    /// Леон ван Бокхорст в
    /// http://www.remondo.net/generic-priority-queue-example-csharp / который был выбран для
    /// сходство с предыдущей реализацией связанного списка.
    /// 
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>
    static class LabyrinthProgram
	{
        /// <краткое содержание>
        /// Основная точка входа для приложения.
        /// </краткое содержание>
        [STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			MazeLogic mazeLogic = new MazeLogic();
			LabyrinthMazeGridForm labyrinthGameGridForm = new LabyrinthMazeGridForm(mazeLogic);
			mazeLogic.SquareWallRemoved += labyrinthGameGridForm.RemoveLine;
			mazeLogic.PathSegmentDrawn += labyrinthGameGridForm.FillPathSquare;

			Application.Run(labyrinthGameGridForm);
		}
	}
}
