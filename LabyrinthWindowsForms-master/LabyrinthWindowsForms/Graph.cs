using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Класс графа: оценка кратчайших путей.
    ///
    /// КОНСТРУКЦИЯ: без параметров.
    ///
    /// ****************** ОБЩЕСТВЕННЫЕ ОПЕРАЦИИ**********************
    /// void addEdge( int v, int w, double cvw )
    /// --> Добавить дополнительное ребро
    /// void printPath( int w ) --> Путь печати после запуска alg
    /// void невзвешенный( int s ) --> Невзвешенный из одного источника
    /// void дейкстра( int s ) --> Взвешенный с одним источником
    /// ****************** ОШИБКИ*********************************
    /// Выполняется некоторая проверка ошибок, чтобы убедиться, что график в порядке,
    /// и убедиться, что график удовлетворяет свойствам, необходимым каждому
    /// алгоритм.  Исключения генерируются при обнаружении ошибок.
    /// 
    // Преобразование / C#:
    /// Поскольку очереди приоритетов не являются стандартными структурами в c# на момент написания этого
    /// программа, была реализована версия от x.
    /// Также потребовались незначительные изменения синтаксиса с Java на C#.
    /// 
    /// Классы Edge, Vertex и SearchPath можно найти после класса Graph
    /// в этом файле.
    /// 
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>
    public class Graph<T>
	{
		public const double INFINITY = double.MaxValue;
		protected Dictionary<T, Vertex<T>> vertexMap = new Dictionary<T, Vertex<T>>();
        /// <краткое содержание>
        /// addEdge добавляет ребро к графику. Имена вершин являются целыми числами.
        /// </краткое содержание>
        /// <param name="sourceName">имя исходной вершины</param>
        /// <param name="destName">имя целевой вершины</param>
        /// <param name="cost">стоимость перемещения из источника в пункт назначения</param>

        public void AddEdge(T sourceName, T destName, double cost)
		{
			Vertex<T> v = GetVertex(sourceName);
			Vertex<T> w = GetVertex(destName);
			v.adj.AddLast(new Edge<T>(w, cost));
		}
        /// <краткое содержание>
        /// GetVertex получает вершину из словарной карты всех вершин или создает
        /// новый, если он не существует в карте вершин словаря.
        /// </краткое содержание>
        /// <param name="vertexName">целочисленное имя вершины, которую нужно получить</param>
        /// <returns>вершину (имя вершины из карты вершин или новую вершину)</returns>

        private Vertex<T> GetVertex(T vertexName)
		{
			Vertex<T> v = null;

			if (vertexMap.ContainsKey(vertexName))
				v = vertexMap[vertexName];

			if(v == null)
			{
				v = new Vertex<T>(vertexName);
				vertexMap.Add(vertexName, v);
			}

			return v;
		}
        /// <краткое содержание>
        /// /// Процедура для обработки недоступности и вывода общей стоимости.
        /// Он вызывает рекурсивную процедуру для печати кратчайшего
        /// путь к destNode после выполнения кратчайшего пути alogirthm.
        /// </краткое содержание>
        /// <param name="destName"></param>

        public void PrintPath(T destName)
		{
			if (!vertexMap.ContainsKey(destName))
				throw new Exception("Пункт назначения не найден!");

			Vertex<T> w = vertexMap[destName];

			if( w == null )
				throw new Exception("Конечная вершина не найдена");
			else if( w.dist == INFINITY )
				Console.WriteLine(destName + " является недостижимым");
			else
			{
				Console.Write("(Cost is: " + w.dist + ") ");
				PrintPathRecursive(w);
				Console.WriteLine();
			}			
		}
        /// <краткое содержание>
        /// Рекурсивная процедура для вывода кратчайшего пути к dest после запуска shortest
        /// алгоритм пути. Известно, что этот путь существует.
        /// </краткое содержание>
        /// <param name="dest">конечная вершина для вывода пути к</param>

        private void PrintPathRecursive(Vertex<T> dest)
		{
			if( dest.prev != null )
			{
				PrintPathRecursive(dest.prev);
				Console.Write(" to ");
			}
			Console.Write(dest.name);
		}
        /// <краткое содержание>
        /// Инициализирует информацию о выходе вершины перед запуском любого кратчайшего пути
        /// алгоритм.
        /// </краткое содержание>
        private void ClearAll()
		{
			foreach(Vertex<T> v in vertexMap.Values)
				v.Reset( );
		}
        /// <краткое содержание>
        /// Невзвешенный алгоритм кратчайшего пути с одним источником.
        /// Это алгоритм, используемый лабиринтом для вычисления кратчайшего пути с помощью
        /// структура очереди.
        /// </краткое содержание>
        /// <param name="startName">целочисленное имя начальной вершины</param>

        public void Unweighted(T startName)
		{
			ClearAll( );

			if (!vertexMap.ContainsKey(startName))
				throw new Exception("Начало не найдено!");

			Vertex<T> start = vertexMap[startName];
			if( start == null )
				throw new Exception("Начальная вершина не найдена");

			Queue<Vertex<T>> q = new Queue<Vertex<T>>( );
			q.Enqueue(start);
			start.dist = 0;

			while(q.Count!=0)
			{
				Vertex<T> v = q.Dequeue();

				foreach(Edge<T> e in v.adj)
				{
					Vertex<T> w = e.dest;
					if(w.dist == INFINITY)
					{
						w.dist = v.dist + 1;
						w.prev = v;
						q.Enqueue(w);
					}
				}
			}
		}
        /// <краткое содержание>
        /// Взвешенный алгоритм кратчайшего пути с одним источником.
        /// Алгоритм Дейкстры не используется Labyrinth, но был включен в
        /// предыдущий код и был обновлен для использования пользовательской очереди приоритетов.
        /// 
        /// Это преобразование кода не проверено.
        /// </краткое содержание>
        /// <param name="startName">целочисленное имя начальной вершины</param>

        public void Dijkstra(T startName)
		{
			PriorityQueue<SearchPath<T>> pq = new PriorityQueue<SearchPath<T>>( );

			if (!vertexMap.ContainsKey(startName))
				throw new Exception("Начало не найдено!");

			Vertex<T> start = vertexMap[startName];

			if( start == null )
				throw new Exception("Начальная вершина не найдена");

			ClearAll( );
			pq.Enqueue(new SearchPath<T>(start, 0));
			start.dist = 0;
        
			int nodesSeen = 0;
			while(!pq.IsEmpty && nodesSeen < vertexMap.Count)
			{
				SearchPath<T> vrec = pq.Dequeue();
				Vertex<T> v = vrec.dest;
				if( v.scratch != 0)  // уже обработанный v
                    continue;
                
				v.scratch = 1;
				nodesSeen++;

				foreach(Edge<T> e in v.adj)
				{
					Vertex<T> w = e.dest;
					double cvw = e.cost;
                
					if( cvw < 0 )
						throw new Exception("График имеет отрицательные ребра");
                    
					if( w.dist > v.dist + cvw )
					{
						w.dist = v.dist +cvw;
						w.prev = v;
						pq.Enqueue(new SearchPath<T>( w, w.dist));
					}
				}
			}
		}


	}
    /// <краткое содержание>
    /// Класс Edge представляет ребро на графике - пункт назначения и стоимость
    /// связанный с его достижением.
    /// </краткое содержание>

    public class Edge<T>
	{
		public Vertex<T> dest;
		public double cost;
        /// <краткое содержание>
        /// Конструктор для Edge.
        /// </краткое содержание>
        ///  <param name="dest">конечная вершина</param>
        /// <param name="cost">значение затрат, связанное с этим пунктом назначения</param>

        public Edge(Vertex<T> dest, double cost)
		{
			this.dest = dest;
			this.cost = cost;
		}
	}
    /// <краткое содержание>
    /// /// Класс Vertex представляет вершину с именем (в виде int), связанный список связанных
    /// ребра, значение расстояния, предыдущее значение вершины и значение нуля.
    /// </краткое содержание>
    public class Vertex<T>
	{
		public T name;
		public LinkedList<Edge<T>> adj;
		public double dist;
		public Vertex<T> prev;
		public int scratch;
        /// <краткое содержание>
        /// Конструктор для вершины задает целочисленное имя для этой вершины, создает
        /// пустой LinkedList для добавления связанных ребер и сброса вершины.
        /// </краткое содержание>
        /// <param name="nm">целочисленное имя вершины</param>

        public Vertex(T nm)
		{
			name = nm;
			adj = new LinkedList<Edge<T>>();
			Reset();
		}
        /// <краткое содержание>
        /// Сброс устанавливает значение расстояния по умолчанию в качестве максимального значения для двойного (определенного в
        /// класс графа в начале), устанавливает предыдущую вершину в значение null и 
        /// значение нуля равно 0.
        /// </краткое содержание>
        public void Reset()
		{
			dist = Graph<T>.INFINITY;
			prev = null;
			scratch = 0;
		}
	}

    /// <краткое содержание>
    /// /// Класс пути поиска представляет запись в приоритетной очереди для алгоритма Дейкстры
    ///.  Он реализует интерфейс IComparable для самого себя.
    /// </краткое содержание>
    public class SearchPath<T> : IComparable<SearchPath<T>>
	{
		public Vertex<T> dest;
		public double cost;
        /// <краткое содержание>
        /// Конструктор для SearchPath.
        /// </краткое содержание>
        /// <param name="d">Вершина в качестве пункта назначения</param>
        ///  <param name="c">удвоение в качестве значения затрат</param>

        public SearchPath(Vertex<T> d, double c)
		{
			dest = d;
			cost = c;
		}
        /// <краткое содержание>
        /// compareTo требуется для реализации IComparable.
        /// /// Это позволяет сравнивать один путь поиска с другим на основе стоимости.
        /// </краткое содержание>
        /// <param name="rhs">Путь поиска с правой стороны</param>
        ///<returns> -1 при меньшей стоимости, 0 при той же или 1 при большей стоимости</returns>

        public int CompareTo(SearchPath<T> rhs)
		{
			double otherCost = rhs.cost;
			return cost < otherCost ? -1 : cost > otherCost ? 1 : 0;
		}
	}
}
