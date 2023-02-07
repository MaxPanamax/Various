using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Реализация общей очереди приоритетов, в которой общий тип должен реализовывать
    /// общий IComparable.
    /// Этот код не протестирован, поскольку метод алгоритма Дейкстры в Graph, который использовал бы
    /// на самом деле он не используется в этом проекте.
    /// </краткое содержание>
    /// <typeparam name="T">тип, реализующий IComparable</typeparam>

    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
	{
        private readonly LinkedList<T> _items;
 
        public PriorityQueue()
        {
            _items = new LinkedList<T>();
        }
 
        #region IPriorityQueue<T> Members
 
        public void Enqueue(T item)
        {
            if (IsEmpty)
            {
                _items.AddFirst(item);
                return;
            }
 
            LinkedListNode<T> existingItem = _items.First;
 
            while (existingItem != null && existingItem.Value.CompareTo(item) < 0)
            {
                existingItem = existingItem.Next;
            }
 
            if (existingItem == null)
                _items.AddLast(item);
            else
            {
                _items.AddBefore(existingItem, item);
            }
        }
 
        public T Dequeue()
        {
            T value = _items.First.Value;
            _items.RemoveFirst();
 
            return value;
        }
 
        public T Peek()
        {
            return _items.First.Value;
        }
 
        public bool IsEmpty
        {
            get { return _items.Count == 0; }
        }
 
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
 
        #endregion
	}
}
