using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// LabyrinthGameGridForm содержит весь код, который непосредственно взаимодействует с формой
    /// с тем же именем.
    /// Метод RemoveLine является наблюдателем для обработчика событий SquareWallRemoved в
    /// Класс MazeLogic. Это означает, что событие срабатывает для каждой стены, которую алгоритм
    /// вычисляет должен быть уничтожен, и RemoveLine затем реагирует, стирая эту линию / стену.
    /// FillPathSquare является наблюдателем для обработчика событий PathSegmentDrawn, также в MazeLogic
    /// класс. Для каждого квадрата, который образует путь из лабиринта, событие PathSegmentDrawn является
    /// срабатывает, и метод FillPathSquare реагирует, помещая красный круг в этот квадрат.
    /// 
    /// -конструктор-
    /// LabyrinthMazeGridForm(MazeLogic mazeLogic) - инициализация переменных, графики и формы
    /// 
    /// -методы-
    /// InitializeGraphics - повторно инициализирует сетку лабиринта перед созданием каждого нового лабиринта 
    /// DrawGrid - рисует квадраты сетки
    /// RemoveLine - удаляет стену квадрата в направлении вверх, вниз, влево или вправо
    /// (часы Squarewall, перемещенные в MazeLogic)
    /// DrawLine - рисует линию из столбца, координаты строки в другую, с заданным цветом
    /// FillPathSquare - заполняет квадрат красным кругом (следит за отображением сегментов пути в MazeLogic)
    /// MazePanel_Paint - рисует графику лабиринта на панели лабиринта с той же графикой
    /// объект, используемый для создания панели
    /// SearchButton_Click - при нажатии кнопки поиска метод Search() в MazeLogic
    /// вызывается.
    /// NewButton_Click - При нажатии новой кнопки метод Create() в MazeLogic выполняется
    /// звонил.
    /// ColumnBox_TextChanged - при вводе значения в текстовое поле ColumnBox минимальное значение равно
    /// применяется как 1, а максимальное значение - как 20
    /// RowBox_TextChanged - Как указано выше, но для RowBox.
    /// ColumnBox_KeyPress - вызывает метод AcceptOnlyNumbers.
    /// RowBox_KeyPress - Как указано выше, но для RowBox.
    /// AcceptOnlyNumbers - метод, вызываемый ColumnBox_KeyPress и RowBox_KeyPress для обеспечения
    /// в текстовое поле можно вводить только цифры.
    /// 
    /// @автор: Пономаренко Максим 
    /// @версия: 1
    /// @дата: 2022-10-30

    /// </краткое содержание>
    public partial class LabyrinthMazeGridForm : Form
	{
		private Bitmap mazeImage;
		private Graphics mazeGraphics;
		private MazeLogic mazeLogic;
		private int maxSquares;
		private int squareSize;
		private int startColumn = 0;
		private int startRow = 0;
		private int maxRows;
		private int maxColumns;
		private int maxRowSize;
		private int maxColumnSize;
		private Color backgroundColor = Color.White;
		private Color lineColor = Color.Black;
		private Color pathColor = Color.Red;

        /// <краткое содержание>
        /// /// Конструктор для игры Лабиринт в виде сетки
        /// </краткое содержание>
        /// <param name="mazeLogic">объект логики лабиринта, который будет выполнять логические операции для
        /// эта форма</параметр>
        public LabyrinthMazeGridForm(MazeLogic mazeLogic)
		{
			this.mazeLogic = mazeLogic;

            //установите некоторые переменные по умолчанию в свойствах логики лабиринта
            maxRows = mazeLogic.MaxRows;
			maxColumns = mazeLogic.MaxColumns;
			maxSquares = mazeLogic.MaxSquares;
			squareSize = mazeLogic.SquareSize;

            //вычислите общий размер сетки лабиринта
            maxRowSize = maxRows * squareSize;
			maxColumnSize = maxColumns * squareSize;

            //инициализировать форму
            InitializeComponent();

            //На данный момент изображение лабиринта равно нулю.
            //Это позволит присвоить новые значения, используя размер панели
            mazeImage = new Bitmap(MazePanel.Width, MazePanel.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Установите изображение лабиринта в качестве фонового изображения на панели
            MazePanel.BackgroundImage = mazeImage;

            //создайте графический объект maze Graphics из изображения лабиринта
            mazeGraphics = Graphics.FromImage(mazeImage);
            //Заливает изображения, которые мы только что создали, белым
            mazeGraphics.Clear(backgroundColor);
		}
        /// <краткое содержание>
        /// Инициализация графики запускает новую сетку с удалением начального и конечного сегментов линии.
        /// </краткое содержание>
        private void InitializeGraphics()
		{
            //нарисуйте сетку игры
            DrawGrid();
            //создать вход
            DrawLine(startColumn, startRow, startColumn, squareSize, backgroundColor);
            //создать выход
            DrawLine(maxColumnSize, maxRowSize - squareSize, maxColumnSize, maxRowSize, backgroundColor);
		}
        /// <краткое содержание>
        /// DrawGrid рисует основную сетку игры прямоугольниками.
        /// </краткое содержание>
        private void DrawGrid()
		{
			for (int x = startColumn; x < maxColumnSize; x += squareSize)
			{
				for (int y = startRow; y < maxRowSize; y += squareSize)
				{
					mazeGraphics.DrawRectangle(new Pen(lineColor), new Rectangle(x, y, squareSize, squareSize));
				}
			}

			this.Refresh();
		}
        /// <краткое содержание>
        /// Удалить линию - это наблюдатель события удаления квадратной стены в логике лабиринта.
        /// Он вызывает DrawLine, чтобы удалить правильную линию.
        /// </краткое содержание>
        /// <param name="sender">отправляющий объект (логика лабиринта)</параметр>
        /// <param name="e">LineDrawnEvenArgs, который содержит начальный и конечный столбцы и строки
        /// с желаемым цветом</параметр>
        public void RemoveLine(object sender, LineDrawnEventArgs e)
		{
			DrawLine(e.C1, e.R1, e.C2, e.R2, e.color);
		}
        /// <краткое содержание>
        /// DrawLine рисует линию от c1, r1 до c2, r2 заданным цветом.
        /// </краткое содержание>
        /// <param name="c1">column 1 position</param>
        /// <param name="r1">row 1 position</param>
        /// <param name="c2">column 2 position</param>
        /// <param name="r2">row 2 position</param>
        /// <param name="color">желаемый цвет линии</param>
        public void DrawLine(int c1, int r1, int c2, int r2, Color color)
		{
			mazeGraphics.DrawLine(new Pen(color), new Point(c1,r1), new Point(c2,r2));
			this.Refresh();
		}
        /// <краткое содержание>
        /// FillPathSquare - это наблюдатель события рисования сегмента пути в логике лабиринта.
        /// Он рисует круг, используя цвет контура, в координатах, заданных 
        /// pathSquareFilledArgs Указывает сетку внутри прямоугольного ограничивающего прямоугольника.
        /// </краткое содержание>
        /// <param name="sender"> отправляющий объект (MazeLogic)</param>
        /// <param name="pathSquareFilledArgs">аргументы, содержащие GridPoint</param>

        public void FillPathSquare(object sender, PathSquareFilledEventArgs pathSquareFilledArgs)
		{
			GridPoint gridPoint = pathSquareFilledArgs.GP;
			int columnCoord = gridPoint.Column * squareSize;
			int rowCoord = gridPoint.Row * squareSize;

			mazeGraphics.FillEllipse(new SolidBrush(pathColor), new Rectangle(columnCoord, rowCoord, squareSize, squareSize));

			this.Refresh();
		}
        /// <краткое содержание>
        /// Maze Panel_Paint рисует изображение лабиринта на панели лабиринта, используя ту же графику
        /// объект, используемый для рисования панели лабиринта. Попеременно панель лабиринта.CreateGraphics() может
        /// были использованы, но нет никаких причин не использовать PaintEventArgs.
        /// </краткое содержание>
        /// <param name="sender">отправляющий объект (MazePanel)</param>
        /// <param name="e">аргументы события рисования</param>

        private void MazePanel_Paint(object sender, PaintEventArgs e)
		{
			 e.Graphics.DrawImageUnscaled(mazeImage, new Point(startColumn, startRow));
		}
        /// <краткое содержание>
        /// Поиск Button_Click запускает поиск пути через текущий лабиринт, отключая
        /// сам по себе. После завершения он активирует кнопку "Новый лабиринт" и текстовые поля для пользовательских
        /// столбец и строка.
        /// </краткое содержание>
        /// <param name="sender">отправляющий объект (SearchButton)</param>
        /// <param name="e">аргументы события</param>

        private void SearchButton_Click(object sender, EventArgs e)
		{
			SearchButton.Enabled = false;
			mazeLogic.Search();
			NewButton.Enabled = true;
			ColumnBox.Enabled = true;
			RowBox.Enabled = true;
		}
        /// <краткое содержание>
        /// /// New Button_Click инициализирует графику для нового лабиринта, используя текущие значения для столбца и
        /// строки, отключая себя и текстовые поля. Он вызывает логический объект maze для инициализации и
        /// создания лабиринта. После завершения он активирует кнопку поиска.
        /// </краткое содержание>
        ///  <param name="sender">объект отправки (NewButton)</param>
        /// <param name="e">аргументы события</param>

        private void NewButton_Click(object sender, EventArgs e)
		{
			mazeGraphics.Clear(backgroundColor);
			InitializeGraphics();
			NewButton.Enabled = false;
			ColumnBox.Enabled = false;
			RowBox.Enabled = false;
			mazeLogic.InitializeMaze();
			mazeLogic.Create();
			SearchButton.Enabled = true;
		}
        /// <краткое содержание>
        /// /// Столбец Box_TextChanged изменяет максимальное значение столбца в зависимости
        /// на значение, введенное в текстовое поле.
        /// </краткое содержание>
        /// <param name="sender">объект отправителя (ColumnBox)</param>
        /// <param name="e">аргументы события</param>

        private void ColumnBox_TextChanged(object sender, EventArgs e)
		{
			int col;
			if(int.TryParse(ColumnBox.Text, out col))
			{
                //Если значение больше 20, оно становится 20.
                if (col > 20)
				{
					ColumnBox.Text = "20";
					col = 20;
				}
                //Если значение меньше 1, оно становится 1.
                else if (col < 1)
				{
					ColumnBox.Text = "1";
					col = 1;
				}

				mazeLogic.MaxColumns = col;
				maxColumns = col;
				maxColumnSize = col * squareSize;
			}
		}
        /// <краткое содержание>
        /// RowBox_TextChanged изменяет максимальное значение строки в зависимости
        /// на значение, введенное в текстовое поле.
        /// </краткое содержание>
        /// <param name="sender">объект отправителя (RowBox)</param>
        /// <param name="e">аргументы события</param>

        private void RowBox_TextChanged(object sender, EventArgs e)
		{
			int row;
			if (int.TryParse(RowBox.Text, out row))
			{
                //Если значение больше 20, оно становится 20.
                if (row > 20)
				{
					RowBox.Text = "20";
					row = 20;
				}
                //Если значение меньше 1, оно становится 1.
                else if (row < 1)
				{
					RowBox.Text = "1";
					row = 1;
				}

				mazeLogic.MaxRows = row;
				maxRows = row;
				maxRowSize = row * squareSize;
			}
		}
        /// <краткое содержание>
        /// /// Нажатие кнопки Column Box_Key вызывает метод Accept Only Numbers, чтобы
        /// запретить пользователю вводить что-либо, кроме цифровых символов.
        /// </краткое содержание>
        /// <param name="sender">объект отправителя (ColumnBox)</param>
        /// <param name="e">аргументы события нажатия клавиши</param>

        private void ColumnBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			AcceptOnlyNumbers(e);
		}
        /// <краткое содержание>
        /// /// Нажатие клавиши RowBox_Key вызывает метод Accept Only Numbers, чтобы
        /// запретить пользователю вводить что-либо, кроме цифровых символов.
        /// </краткое содержание>
        /// <param name="sender">объект отправителя (RowBox)</param>
        /// <param name="e">аргументы события нажатия клавиши</param>

        private void RowBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			AcceptOnlyNumbers(e);
		}
        /// <краткое содержание>
        /// /// Принимать только числа запрещает все символы, которые не являются числами.
        /// </краткое содержание>
        /// <param name="e">аргументы события нажатия клавиши</param>

        private void AcceptOnlyNumbers(KeyPressEventArgs e)
		{
            //Только цифры
            if (!Char.IsDigit(e.KeyChar))
			{
				if (!(e.KeyChar == Convert.ToChar(Keys.Back)))
					e.Handled = true;
			}
			base.OnKeyPress(e);
		}

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
