using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0080_TryCatch
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



/*
 * _HB361p testMakingException()
 * 
    길이가 3인 배열의 길이보다 큰 배열 요소에 접근할때 예외가 발생하고 프로그램이 강제 종료합니다.
    이때 콘솔창에서 보여주는 메시지는 처리되지 않은 예외: System.IndexOutOfRangeException: 인덱스가 배열 범위를 벗어났습니다.
    라고 나옵니다.
    길이가 3인 배열의 길이보다 큰 배열 요소에 접근할때 발생한 System.IndexOutOfRangeException 예외를 Main()메소드에 던지고,
    Main() 안에서 이 예외를 처리할 로직이 없기 때문에, 이 예외를 다시 CLR에 넘긴 결과, "처리되지 않은 예외"로 처리한다는
    메시지를 출력하고 프로그램을 강제 종료한겁니다.

 */

/*
 * _HB364p testTryCatch()
 * 
    길이가 3인 배열의 길이보다 큰 배열 요소에 접근할때 발생한 System.IndexOutOfRangeException 예외를 Main()메소드에 던지면
    try ~ catch문이 받아서 이 예외를 처리하기 때문에, 이 예외를 Main()메소드에서 CLR에 넘기지 않고, 개발자가 원하는 대로
    예외처리를 할 수 있습니다.

    즉, 모든 예외 클래스들은 System.Exception 형식으로 받을 수 있기 때문에, System.Exception 형식의 예외를 받는 catch절 하나면
    모든 예외를 다 받을 수 있습니다.
    하지만, 예외는 여러가지의 상황이 있고 System.Exception 클래스 하나로 받는 catch는 적절하지 않고, 여러 경우의 catch로
    예외처리하는 것이 필요한 이유입니다.

 */

/*
 * _HB367p testThrow()
 * 
    throw를 통해 던져진 예외 객체는 catch문이 받습니다.
    throw로 던졌지만 DoSomething() 메소드안에는 이 예외를 처리할 수 있는 코드가 없으므로, 이 예외는 DoSomething() 메소드를
    호출한 곳에 있는 try ~ catch문에서 받아서 처리합니다.
    즉, 예외 발생시 자신을 포함하고 있는 가장 가까운 try블록을 찾아 이 블록에 대응되는 catch를 실행합니다.

 */

/*
 * _HB370p testFinally()
 * 
    DB의 연결해제와 같이 예외처리와 상관없이 finally로 반드시 처리해야 할 뒷마무리를 할 수 있습니다.
    예외가 일어나든 일어나지 않든간에 반드시 fianlly절의 코드는 실행이 됩니다.
    try절이 실행된다면 finally절은 어떤 경우에라도 실행됩니다.
    심지어 try절 안에서 return문이나 throw문이 사용되더라도 finally절은 반드시 실행됩니다.
    

 */
