using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// ExtendedGraph - это подкласс Graph, который добавляет методы getPath и
    /// GetPathRecursive. getPath - это общедоступная версия, которая вызывается с желаемым
    /// integer, в то время как GetPathRecursive - это частный рекурсивный метод, который возвращает
    /// оптимальный путь в виде списка целых чисел.
    /// 
    /// *Обратите внимание, что класс вершин определен в Graph.cs

    /// Небольшие изменения синтаксиса и изменение возвращаемого списка с помощью getPath.
    /// 
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>
    public class ExtendedGraph<T> : Graph<T>
	{
        /// <краткое содержание>
        /// getPath вызывает GetPathRecursive для получения пути через
        /// лабиринт, который затем возвращается.
        /// </краткое содержание>
        ///<param name="destName">желаемый пункт назначения для получения пути к</param>
        /// <returns>список целых чисел (квадратов), которые образуют этот путь</returns>

        public List<T> GetPath(T destName)
		{
			List<T> path = null;          
			Vertex<T> vertex = vertexMap[destName];    	
    	
			if( vertex == null )
				throw new Exception("Конечная вершина не найдена");    		
			else if( vertex.dist == INFINITY )
				Console.WriteLine(destName + " является недостижимым");
			else
			{
                //Вызовы Get Path Рекурсивны для пути
                path = GetPathRecursive(vertex);
			}

            //Список возвращается в обратном порядке, так что переверните его обратно
            path.Reverse();

    		return path;
		}
        /// <краткое содержание>
        /// /// Get Path Recursive - это рекурсивный метод, который получает список целых чисел
        /// (квадраты), которые составляют путь к целевой вершине, а затем
        /// возвращает окончательный список.
        /// По сути, это начинается с пункта назначения и находит обратный путь к
        /// началу.
        /// </краткое содержание>
        /// <param name="destName">назначение вершины, чтобы получить путь к</param>
        /// <returns>список целых чисел (квадратов), которые образуют путь к заданной вершине</returns>

        private List<T> GetPathRecursive(Vertex<T> dest)
		{
			List<T> path = new List<T>();

			path.Add(dest.name);

            //Вызывает себя до тех пор, пока предыдущее назначение не станет нулевым
            if (dest.prev != null)
			{
				path.AddRange(GetPathRecursive(dest.prev));
			}

			return path;
		}    
	}
}
