using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0070_Loop
{
    class Program
    {
        static void Main(string[] args)
        {
            //testWhile();
            //testDoWhile();
            //testFor();
            //testForSumTo100();
            //testForSumOddTo100();
            testForSumInverseTo100();
        }

        private static void testForSumInverseTo100()
        {
            //(3) 1 + 1 / 2 + 1 / 3 + ... + 1 / 100 을 구하는 프로그램
            double sum3 = 0;
            for (int x = 1; x <= 100; x++)
            {
                sum3 += 1.0 / x;
                Console.WriteLine("1부터 {0}까지 역수의 합은 {1}", x, sum3);
            }
            Console.WriteLine("1부터 100까지 역수의 합은 {0}", sum3);
        }

        private static void testForSumOddTo100()
        {
            // (2) 1에서 100까지 홀수의 합을 구하는 프로그램
            int sum2 = 0;
            for (int x = 1; x <= 100; x++)
            {
                if (x % 2 == 1)
                    sum2 += x;
            }
            Console.WriteLine("1부터 100까지 홀수의 합은 {0}", sum2);
        }

        private static void testForSumTo100()
        {
            // (1) 1부터 100까지 더하는 프로그램
            int sum = 0;
            for (int i = 1; i <= 100; i++)
            {
                sum += i;
            }
            Console.WriteLine("1부터 100까지 숫자의 합은 {0}", sum);
        }

        private static void testFor()
        {
            for (int k = 0; k < 10; k++)
                Console.WriteLine("{0}: Hello C#", k);
        }

        private static void testDoWhile()
        {
            int j = 0;
            do
            {
                Console.WriteLine("{0}: Hello C#", j);
                j++;
            } while (j < 10);
        }

        private static void testWhile()
        {
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine("{0}: Hello C#", i);
                i++;
            }
        }
    }
}



/*
 * _CS200_138p from A043_Loop
 *     
    while문은 조건을 먼저 체크하고 증감변수의 값 변화가 있습니다.
    반면에, do~while문은 우선 증감변수의 값에 변화가 먼저 있습니다.

 */
