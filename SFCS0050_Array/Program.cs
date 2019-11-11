using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0050_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            testArray();
            testArray2_Initialize();
            testArray3_Initialize();
        }

        private static void testArray3_Initialize() //...☎0010.173p.A057_ArrayBasic.
        {
            int[] a = { 1, 2, 3 };      // 배열의 초기화
            Console.Write("a[]: ");
            foreach (var value in a)
                Console.Write(value + " ");

            int[] b = new int[] { 1, 2, 3 }; // 배열의 초기화
            Console.Write("\nb[]: ");
            for (int i = 0; i < 3; i++)
                Console.Write(b[i] + " ");

            int[] c = new int[3] { 1, 2, 3 }; // 배열의 초기화
            Console.Write("\nc[]: ");
            for (int i = 0; i < b.Length; i++)  //...☎0010.174p.
                Console.Write(b[i] + " ");

            int[] d = new int[3];
            d[0] = 1;
            d[1] = d[0] + 1;
            d[2] = d[1] + 1;
            Console.Write("\nd[]: ");
            foreach (int value in d)
                Console.Write(value + " ");
            Console.WriteLine();
        }

        private static void testArray2_Initialize()
        {
            string[] array1 = new string[3] { "안녕", "Hello", "Halo" }; //...방법1: new, 배열크기, 배열요소의 값.

            Console.WriteLine("array1...");
            foreach (string greeting in array1)
                Console.WriteLine(" {0}", greeting);

            string[] array2 = new string[] { "안녕", "Hello", "Halo" }; //...방법2: new, 배열요소의 값.

            Console.WriteLine("\narray2...");
            foreach (string greeting in array2)
                Console.WriteLine(" {0}", greeting);

            string[] array3 = { "안녕", "Hello", "Halo" }; //...방법3: 배열요소의 값.

            Console.WriteLine("\narray3...");
            foreach (string greeting in array3)
                Console.WriteLine(" {0}", greeting);
        }

        private static void testArray()
        {
            int[] scores = new int[5];
            scores[0] = 80;
            scores[1] = 74;
            scores[2] = 81;
            scores[3] = 90;
            scores[4] = 34;

            foreach (int score in scores)
                Console.WriteLine(score);

            int sum = 0;
            foreach (int score in scores)
                sum += score;

            int average = sum / scores.Length;  //...배열의 길이.

            Console.WriteLine("Average Score : {0}", average);
        }
    }
}

