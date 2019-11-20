using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0200_Params
{
  class Program
    {
        public static void PrintIntParams(params int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        public static void PrintObjectParams(params object[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            PrintIntParams(1, 2, 3, 4);
            PrintIntParams(10, 20, 30, 40, 50);
            PrintObjectParams(1, 1.234, 'a', "test");
            PrintObjectParams();

            int[] myIntArray = { 5, 6, 7, 8, 9 };
            PrintIntParams(myIntArray);

            object[] myObjArray = { 2, 2.345, 'b', "test", "again" };
            PrintObjectParams(myObjArray);

            PrintObjectParams(myIntArray);
        }
    }
}


/*
 * _CS200_278p.SFCS0200_Params from A090_params 
 *         

 */
