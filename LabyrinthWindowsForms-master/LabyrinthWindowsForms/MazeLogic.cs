using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// MazeLogic рисует лабиринт с помощью метода Create, который использует KnockDownWall для
    /// уведомления наблюдателя о событии SquareWallRemoved, полученном методом RemoveLine в
    /// LabyrinthGameGridForm для обработки графических элементов. Аналогично, метод поиска использует
    /// событие PathSegmentDrawn для уведомления метода FillPathSquare в LabyrinthGameGridForm
    /// для рисования пути решения к лабиринту.
    ///
    /// DisjointSet используется для логики поиска/объединения, random - генератор случайных чисел
    /// для создания пути лабиринта, а ExtendedGraph - это график, используемый для поиска
    /// способ.
    ///
    /// -методы-
    /// InitializeMaze - инициализирует новый лабиринт. 
    /// Create - использует логику поиска/объединения непересекающихся множеств для создания лабиринта при построении
    /// графика (невзвешенного). Он вызывает KnockDownWall для обработки уведомления пользовательского интерфейса об изменениях.
    /// KnockDownWall(int squareId, точка сетки.Direction dir) - вызывает другой метод KnockDownWall
    /// Нокдаунволл(int row, int col, GridPoint.Direction dir) - определяет координаты для
    /// строка для удаления и отправляет событие SquareWallRemoved
    /// Search - использует алгоритм невзвешенного графа, извлекает путь с помощью getPath (из
    /// ExtendedGraph class), а затем отправляет событие PathSegmentDrawn для каждого квадрата пути.
    /// GetSquareId - получает целочисленный идентификатор квадрата сетки из строки и столбца точки сетки. 
    /// GetRow - возвращает номер строки квадрата сетки из идентификатора квадрата.
    /// GetColumn - возвращает номер столбца квадрата сетки из идентификатора квадрата.
    /// isValid - возвращает, если точка сетки находится в пределах сетки. 
    /// HasValidExit - определяет, имеет ли точка на сетке допустимый выход, используя
    /// isValid - используется для определения того, когда выходы больше невозможны (достигнут конец).
    /// ValidDirection - определяет, является ли случайно выбранное направление допустимым, используя isValid
    /// и возможные методы объединения.
    /// UnionPossible - определяет, возможно ли объединение между двумя точками (с непересекающимися наборами)
    /// CalculateUntion - выполняет фактическое объединение для двух точек и добавляет ребра к ExtendedGraph
    /// для последующего использования методом поиска.
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>
    public class MazeLogic
	{
		private DisjointSets disjointSet;
		private Random random;
		private ExtendedGraph<int> extendedGraph;
		private int maxRows;
		private int maxColumns;
		private int maxSquares;
		private int adjustedSquareSize = 20;
        // private int gridSize = 400;
        // private int rowOffset / частный набор строк = (400-20*20)/2 + 2;
        // частные столбцы int = (400-20*20)/2 + 2;
        public EventHandler<LineDrawnEventArgs> SquareWallRemoved = null;
		public EventHandler<PathSquareFilledEventArgs> PathSegmentDrawn = null;
	    private Color backgroundColor = Color.White;
        
		/// <краткое содержание>
        /// Конструктор для логики лабиринта.
        /// </краткое содержание>
        public MazeLogic()
		{
			maxRows = 20;
			maxColumns = 20;
			maxSquares = maxRows * maxColumns;

            //int adj Строка = размер сетки/maxRows;
            //int adj Col = размер сетки/максимальное количество столбцов;
            //скорректированный размер квадрата = (adj Коричневый < adj Холодный ? adj Коричневый : катушка adj);
            //смещение строки = (размер сетки-максимальные строки*скорректированный размер квадрата)/2 + 2; 
            //смещение столбца = (размер сетки -максимальное количество столбцов * скорректированный размер квадрата)/2 + 2; 

            //инициализировать непересекающийся набор
            disjointSet = new DisjointSets(maxSquares);
            //инициализировать генератор случайных чисел
            random = new Random();
            //инициализировать график
            extendedGraph = new ExtendedGraph<int>();
		}

        /// <краткое содержание>
        /// InitializeMaze инициализирует новый лабиринт, создавая новый разрозненный набор, случайный
        /// генератор и расширенный график.
        /// </краткое содержание>
        public void InitializeMaze()
		{
			maxSquares = maxRows * maxColumns;

            ////инициализировать непересекающийся набор
            disjointSet = new DisjointSets(maxSquares);
            //инициализировать генератор случайных чисел
            random = new Random();
            //инициализировать график
            extendedGraph = new ExtendedGraph<int>();
		}

        /// <краткое содержание>
        /// Свойство maxRows.
        /// </краткое содержание>
        public int MaxRows
		{
			get { return maxRows; }
			set { maxRows = value; }
		}
        /// <краткое содержание>
        /// Свойство Max Columns.
        /// </краткое содержание>
        public int MaxColumns
		{
			get { return maxColumns; }
			set { maxColumns = value; }
		}

        /// <краткое содержание>
        /// Свойство Max Squares.
        /// </краткое содержание>
        public int MaxSquares
		{
			get { return maxSquares; }
			set { maxSquares = value; }
		}

        /// <краткое содержание>
        /// Свойство размера квадрата.
        /// </краткое содержание>
        public int SquareSize
		{
			get { return adjustedSquareSize; }
			set { adjustedSquareSize = value; }
		}
        /// <краткое содержание>
        /// KnockDown Wall получает идентификатор квадрата и информацию о направлении и
        /// вызывает другую версию Knockdown Wall с информацией о строке и столбце.
        /// </краткое содержание>
        /// <param name="squareId">идентификатор квадрата</param>
        /// <param name="dir">направление стены, которую нужно сбить</param>
		
        private void KnockDownWall(int squareId, GridPoint.Direction dir)
		{
			KnockDownWall(GetRow(squareId), GetColumn(squareId), dir);
		}
        /// <краткое содержание>
        /// Стена Нокдауна получает информацию о строке, столбце и направлении и
        ///вычисляет координаты перед отправкой удаленной квадратной стены
        /// событие с нарисованными линиями EventArgs.
        /// </краткое содержание>
        /// <param name="row">строка квадрата</param>
        /// <param name="col">столбец квадрата</param>
        /// <param name="dir">направление стены, которую нужно сбить</param>

        private void KnockDownWall(int row, int col, GridPoint.Direction dir)
		{
            // Вычислить координаты отрезка линии для удаления
            int c1 = 0, r1 = 0, c2 = 0, r2 = 0;
			if (dir == GridPoint.Direction.UP)
			{
				c1 = col * adjustedSquareSize + 1;
				c2 = (col + 1) * adjustedSquareSize - 1;
				r1 = r2 = row * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.RIGHT)
			{
				r1 = row * adjustedSquareSize + 1;
				r2 = (row + 1) * adjustedSquareSize - 1;
				c1 = c2 = (col + 1) * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.DOWN)
			{
				c1 = col * adjustedSquareSize + 1;
				c2 = (col + 1) * adjustedSquareSize - 1;
				r1 = r2 = (row + 1) * adjustedSquareSize;
			}
			else if (dir == GridPoint.Direction.LEFT)
			{
				r1 = row * adjustedSquareSize + 1;
				r2 = (row + 1) * adjustedSquareSize - 1;
				c1 = c2 = col * adjustedSquareSize;
			}
            //Уведомить наблюдателя, чтобы он стер линию
            LineDrawnEventArgs lineArgs = new LineDrawnEventArgs();
			lineArgs.C1 = c1;
			lineArgs.C2 = c2;
			lineArgs.R1 = r1;
			lineArgs.R2 = r2;
			lineArgs.color = backgroundColor;
			SquareWallRemoved(this, lineArgs);
		}
        /// <краткое содержание>
        ///  Get Square Id получает целочисленный идентификатор квадрата сетки из строки и
        ///  столбца точки сетки.
        /// </краткое содержание>
        /// <param name="p">точечный объект сетки</param>
        /// <returns>целочисленный идентификатор, представляющий квадратную позицию</returns>

        private int GetSquareId(GridPoint p)
		{
			return p.Row * maxColumns + p.Column;
		}
        /// <краткое содержание>
        /// GetRow получает номер строки квадрата сетки из идентификатора квадрата.
        /// </краткое содержание>
        /// <param name="squareId">идентификатор квадрата</param>
        /// <returns>строку, в которой можно найти этот идентификатор квадрата</returns>

        private int GetRow(int squareId)
		{
			return squareId / maxColumns;
		}
        /// <краткое содержание>
        /// GetColumn возвращает номер столбца квадрата сетки из идентификатора квадрата.
        /// </краткое содержание>
        /// <param name="squareId">идентификатор квадрата</param>
        /// <returns>столбец, в котором можно найти этот квадратный идентификатор</returns>

        private int GetColumn(int squareId)
		{
			return squareId % maxColumns;
		}
        /// <краткое содержание>
        /// Допустимо возвращает, если точка сетки находится в пределах сетки. 
        /// </краткое содержание>
        /// <param name="p">точка сетки</param>
        /// <returns>true, если в пределах сетки, false, если нет</returns>

       
        private bool IsValid(GridPoint p)
		{
			return p.Row >= 0 && p.Row < maxRows && p.Column >= 0 && p.Column < maxColumns;
		}
        /// <краткое содержание>
        /// Create создает лабиринт, циклически проходя по всем точкам сетки
        /// и определение случайного действительного выхода до тех пор, пока больше выходов не будет найдено
        /// (который является последним квадратом).
        ///  Каждая точка сетки соединена с другой с помощью объединения с непересекающимся множеством.
        /// Это гарантирует непрерывное соединение отдельных секций, позволяя
        /// отсутствие повторяющихся подключений и возможность подключения по крайней мере к каждой точке сетки
        ///  в одну другую точку сетки.
        /// </краткое содержание>
        public void Create()
		{
            //Цикл по всем точкам в сетке
            for (int r = 0; r < maxRows; r++)
			{
				for (int c = 0; c < maxColumns; c++)
				{
                    //переменные
                    GridPoint gridPoint = new GridPoint(r, c);
					Pair<int, GridPoint.Direction> pair = null;

                    //Установите начальное случайное значение и проверьте правильность
                    int randomNumber = random.Next(4);
					GridPoint newPoint = ValidDirection(randomNumber, gridPoint);

                    //Найдите случайное направление, которое является допустимым (если таковое существует)
                    while (newPoint == null && HasValidExit(gridPoint))
					{
						randomNumber = random.Next(4);
						newPoint = ValidDirection(randomNumber, gridPoint);
					}

                    //Последняя точка сетки в сетке не будет существовать, так что это
                    //пропускает этот конкретный случай
                    if (newPoint != null)
					{
                        //Выполните объединение задействованных точек и вычислите
                        //разрушение стены
                        pair = CalculateUnion(gridPoint, newPoint);
						KnockDownWall(GetRow(pair.first), GetColumn(pair.first), pair.second);
					}
				}
			}
		}

        /// <краткое содержание>
        /// Поиск находит кратчайший путь через лабиринт, используя невзвешенный график 
        ///  алгоритм на графике и извлекает путь.  Событие рисования сегмента пути затем
        /// отправлено с PathSquareFilledEventArgs для графической интерпретации.
        /// </краткое содержание>
        public void Search()
		{
			if (maxRows == 1 && maxColumns == 1)
			{
				GridPoint gridPoint = new GridPoint(0, 0);
				PathSquareFilledEventArgs pathArgs = new PathSquareFilledEventArgs();
				pathArgs.GP = gridPoint;
				PathSegmentDrawn(this, pathArgs);
			}
			else
			{
                //Запускает невзвешенный алгоритм на графике с начальной точкой сетки ячейки 0
                extendedGraph.Unweighted(0);
				extendedGraph.PrintPath(maxSquares - 1);
                //Получает наилучший путь к местоположению конечного квадрата
                List<int> path = extendedGraph.GetPath(maxSquares - 1);

                //Уведомляет наблюдателя о наилучшем пути через лабиринт
                if (path != null)
				{
					foreach (int i in path)
					{
						GridPoint gridPoint = new GridPoint(GetRow(i), GetColumn(i));
						PathSquareFilledEventArgs pathArgs = new PathSquareFilledEventArgs();
						pathArgs.GP = gridPoint;
						PathSegmentDrawn(this, pathArgs);
					}
				}
			}
		}
        /// <краткое содержание>
        ///  Имеет допустимый выход определяет, имеет ли точка сетки на сетке допустимый выход, и
        /// возвращает true, если это так.
        /// Когда будет установлен последний квадрат, этот метод должен вернуть значение false.
        /// </краткое содержание>
        /// <param name="gridPoint">Точка сетки для проверки выходов из</param>
        /// <returns>true, если существует допустимый выход, в противном случае false</returns>

        private bool HasValidExit(GridPoint gridPoint)
		{
            //Тест вверх
            GridPoint newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.UP);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

            //Тест вниз    
            newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.DOWN);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			//Тест влево      
			newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.LEFT);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			//Тест вправо       
			newPoint = new GridPoint(gridPoint.Row, gridPoint.Column);
			newPoint.Move(GridPoint.Direction.RIGHT);

			if ((IsValid(newPoint)) && (UnionPossible(gridPoint, newPoint)))
			{
				return true;
			}

			return false;
		}
        /// <краткое содержание>
        /// Допустимое направление определяет, является ли случайно сгенерированное направление от начальной точки сетки ////// допустимым, и может ли найденная там новая точка сетки
        /// быть соединенным (union) с начальной точкой сетки.
        /// </краткое содержание>
        /// <param name="randomNumber">генератор случайных чисел для выбора направления</param>
        /// <param name="gridPoint">Точка сетки для выбора направления</param>
        /// <returns>новый объект GridPoint, если он допустим, в противном случае null</returns>

        private GridPoint ValidDirection(int randomNumber, GridPoint gridPoint)
		{
            //Инициализирует новую точку значением точки сетки
            GridPoint newPoint = new GridPoint(gridPoint.Row,gridPoint.Column);

            //Перемещает новую точку сетки в направлении, в зависимости от того, какое случайное
            //указано направление
            if (randomNumber == 0) {
				newPoint.Move(GridPoint.Direction.UP); 
			}
			else if(randomNumber == 1) {
				newPoint.Move(GridPoint.Direction.RIGHT); 
			}
			else if(randomNumber == 2) {
				newPoint.Move(GridPoint.Direction.DOWN); 
			}
			else if(randomNumber == 3) {
				newPoint.Move(GridPoint.Direction.LEFT); 
			}          
			else {
				Console.WriteLine("Error in random number");
				return null;
			}

            //Если новая точка сетки действительна и возможно объединение с первой точкой сетки
            //возвращает новую точку сетки
            if ( (IsValid(newPoint) ) && (UnionPossible(gridPoint, newPoint)) ) {
				return newPoint;
			}
        
			return null;
		}
        /// <краткое содержание>
        ///  Объединение возможно проверяет, возможно ли соединить корни двух точек
        /// с разрозненным множеством.  Если это возможно, метод возвращает true,
        /// в противном случае false.
        /// </краткое содержание>
        /// <param name="randomNumber">первая точка сетки</param>
        ///<param name="gridPoint">вторая точка сетки</param>
        /// <returns>значение true, если объединение было возможно, в противном случае значение false</returns>

        private bool UnionPossible(GridPoint gridPoint, GridPoint newPoint)
		{
			int here = GetSquareId(gridPoint);
			int next = GetSquareId(newPoint);
			int hereRoot = 0;
			int nextRoot = 0;

            //найдите корень здесь   
            try
            {
				hereRoot = disjointSet.Find(here);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

            //найдите корень следующего      
            try
            {
				nextRoot = disjointSet.Find(next);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

            //Если они оба являются верхними корнями, верните true
            if (hereRoot == 0 && nextRoot == 0)
			{
				return true;
			}

            //если возвращается значение 0, то это верхний корень.  Используйте его квадрат
            //значение вместо этого.      
            if (hereRoot == 0)
			{
				hereRoot = here;
			}
			if (nextRoot == 0)
			{
				nextRoot = next;
			}

            //Если они не имеют одного и того же корня, верните true
            if (hereRoot != nextRoot)
			{
				return true;
			}

			return false;
		}
        /// <краткое содержание>
        ///  Вычислить объединение определяет задействованные корни и выполняет объединение между
        /// значения двух квадратов (представляющие две точки в лабиринте).
        /// В случае успеха он возвращает объект Pair с информацией, необходимой для последующего
        ///  передайте наблюдателям (точку сетки и направление).  При сбое (который не должен
        /// happen) он возвращает значение null.
        /// </краткое содержание>
        /// <param name="randomNumber">очка сетки для выхода из</param>
        /// <param name="gridPoint">новая точка сетки, к которой ведет выход</param>
        /// <returns>объект класса Pair с целым числом и точкой сетки.Значения направления,
        /// или null при сбое</returns>

        private Pair<int, GridPoint.Direction> CalculateUnion(GridPoint gridPoint, GridPoint newPoint)
		{
            //Переменные
            int here = GetSquareId(gridPoint); //значение ячейки точки сетки 
            int next = GetSquareId(newPoint);  //значение ячейки новой точки 
            int hereRoot = 0;
			int nextRoot = 0;

            //найдите корень здесь
            try
            {
				hereRoot = disjointSet.Find(here);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

            //найдите корень следующего
            try
            {
				nextRoot = disjointSet.Find(next);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

            //если возвращается значение 0, то это верхний корень.  Используйте его квадрат
            //значение вместо этого.
            if (hereRoot == 0) {
				hereRoot = here; 
			}
			else if(nextRoot == 0) {
				nextRoot = next;
			}

            //попытайтесь объединить и верните объект Pair в случае успеха
            try
            {
				disjointSet.Union(nextRoot, hereRoot);
                //Добавляет ребра к расширенному графику
                extendedGraph.AddEdge(here, next, 1);
				extendedGraph.AddEdge(next, here, 1);            
            
				return(new Pair<int,GridPoint.Direction>(here,gridPoint.GetDirection(newPoint)));                  
			}
			catch (Exception e)
			{  
				MessageBox.Show("Ошибка объединения: " + here+" "+next+" ; "+e.Message);
			}
        
			return null;
		}
	}
}
