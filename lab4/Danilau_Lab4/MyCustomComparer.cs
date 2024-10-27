using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilau_Lab4
{
    public class MyCustomComparer<T> : IComparer<T>
        where T : Resident
    {
        public int Compare(T? x, T? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentException("Сравниваемые объекты не должны быть null.");
            }

            // Сравниваем по свойству Name (в алфавитном порядке)
            return string.Compare(x.Name, y.Name);
        }
    }
}
