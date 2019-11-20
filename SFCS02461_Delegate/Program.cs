using System;

namespace SFCS02461_Delegate
{
    delegate int MyDelegate(int a, int b);

    class MainApp
    {
        static void Main(string[] args)
        {
            MyDelegate Callback;

            Callback = new MyDelegate(Plus);
            Console.WriteLine(Callback(3, 4));

            Callback = new MyDelegate(Minus);
            Console.WriteLine(Callback(7, 5));
        }


        public static int Plus(int a, int b)
        {
            return a + b;
        }

        public static int Minus(int a, int b)
        {
            return a - b;
        }
    }
}

/*
 * _HB383p.SFCS02461_Delegate from Delegate 
 *         
 
 */

