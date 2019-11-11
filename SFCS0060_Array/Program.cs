using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0060_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            testArray();
        }

        private static void testArray()
        {
            int[] array = new int[] { 10, 30, 20, 7, 1 };
            Console.WriteLine("Type Of array : {0}", array.GetType()); //...☎0010.HB105p.    
            Console.WriteLine("Base type Of array : {0}", array.GetType().BaseType);
        }
    }
}

