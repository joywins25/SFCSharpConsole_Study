﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS02490_Lambda
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new[] { 3, 34, 6, 34, 7, 8, 24, 3, 675, 8, 23 };

            int n = Count(arr, x => x % 2 == 0);
            Console.WriteLine("Lambda 짝수의 갯수 : " + n);

            n = Count(arr, x => x % 2 == 1);
            Console.WriteLine("Lambda 홀수의 갯수 : " + n);
        }

        private static int Count(int[] arr, Func<int, bool> testMethod)
        {
            int cnt = 0;
            foreach (var n in arr)
            {
                if (testMethod(n))
                    cnt++;
            }
            return cnt;
        }
    }
}

/*
 * _CS200_359p.SFCS02490_Lambda from A116_LambdaExpression 
 *         

 */
