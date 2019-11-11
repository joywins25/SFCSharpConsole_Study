using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0090_TryCatch
{
    class Program
    {

        static void Main(string[] args)
        {

            //testMakingException();
            //testTryCatch();
            //testThrow();
            //testFinally();
        }


        private static void testFinally()
        {
            try
            {
                Console.Write("0으로 나누어 에러를 발생시켜봅니다.");
                Console.Write("제수를 입력하세요. :");
                String temp = Console.ReadLine();
                int divisor = Convert.ToInt32(temp);

                Console.Write("피제수를 입력하세요. : ");
                temp = Console.ReadLine();
                int dividend = Convert.ToInt32(temp);

                Console.WriteLine("{0}/{1} = {2}",
                    divisor, dividend, Divide(divisor, dividend));
            }
            catch (FormatException e)
            {
                Console.WriteLine("에러1 : " + e.Message);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("에러2 : " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("에러3 : " + e.Message);
            }
            finally
            {
                Console.WriteLine("프로그램을 종료합니다.");
            }
        }

        private static object Divide(int divisor, int dividend)
        {
            try
            {
                Console.WriteLine("Divide() 시작");
                return divisor / dividend;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Divide() 예외 발생");
                throw e;
            }
            finally
            {
                Console.WriteLine("Divide()  끝");
            }
        }

        private static void testThrow()
        {
            try
            {
                DoSomething(1);
                DoSomething(3);
                DoSomething(5);
                DoSomething(9);
                DoSomething(11);
                DoSomething(13);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void DoSomething(int arg)
        {
            if (arg < 10)
                Console.WriteLine("arg : {0}", arg);
            else
                throw new Exception("arg가 10보다 큽니다.");
        }

        private static void testTryCatch()
        {
            int[] arr = { 1, 2, 3 };

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("예외가 발생했습니다 : {0}", e.Message);
            }

            Console.WriteLine("종료");
        }

        private static void testMakingException()
        {
            int[] arr = { 1, 2, 3 };

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(arr[i]);
            }

            Console.WriteLine("종료");
        }
    }
}

