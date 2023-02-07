using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ponomarenko_Labyrinth_WF
{
    /// <краткое содержание>
    /// Pair представляет собой общую пару значений. Вероятно, это можно было бы сделать
    /// с классом C# Tuple, но Pair был частью исходного кода Java.
    /// 
    /// @автор: Пономаренко Максим
    /// @версия: 1
    /// @дата: 2022-10-30
    /// </краткое содержание>
    /// <typeparam name="A">type A</typeparam>
    /// <typeparam name="B">type B</typeparam>

    public class Pair<A, B>
	{
		public Pair(A first, B second)
		{
			this.first = first;
			this.second = second;
		}

		public A first;
		public B second;
	}
}
