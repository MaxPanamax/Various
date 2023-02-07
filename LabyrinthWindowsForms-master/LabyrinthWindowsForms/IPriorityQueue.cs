using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// /// Интерфейс для общей очереди приоритетов, который реализует общую версию
    /// IEnumerable и требует, чтобы T принадлежал к типу, который реализует общий IComparable.
    /// 
    /// /// Приоритетная очередь взята из примера кода C# Леона ван Бокхорста в
    /// http://www.remondo.net/generic-priority-queue-example-csharp / и был выбран для
    /// сходство с предыдущей реализацией связанного списка.
    /// 
    /// Этот код не протестирован, поскольку метод алгоритма Дейкстры в Graph, который использовал бы
    /// на самом деле он не используется в этом проекте.
    /// </краткое содержание>
    /// <typeparam name="T"> тип, реализующий IComparable(T)"/></typeparam>

    interface IPriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
	{
        bool IsEmpty { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
	}
}
