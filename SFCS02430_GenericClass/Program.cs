using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS02430_GenericClass
{
    class MyClass<T>
    {
        private T[] arr;
        private int count = 0;

        public MyClass(int length)
        {
            arr = new T[length];
            count = length;
        }

        public void Insert(params T[] args)
        {
            for (int i = 0; i < args.Length; i++)
                arr[i] = args[i];
        }

        public void Print()
        {
            foreach (T i in arr)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        public T AddAll()
        {
            T sum = default(T);  //...☎0010.SFCS0150_Inheritance.
            foreach (T item in arr)
                sum = sum + (dynamic)item; //...☎0020.SFCS02450_GenericMethodsUsingDynamic.
            return sum;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyClass<int> a = new MyClass<int>(10);
            MyClass<string> s = new MyClass<string>(5);

            a.Insert(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            s.Insert("Tiger", "Lion", "Zebra", "Monkey", "Cow");

            a.Print();
            s.Print();

            Console.WriteLine("a.AddAll() : " + a.AddAll());
            Console.WriteLine("s.AddAll() : " + s.AddAll());
        }
    }
}


/*
 * _CS200_290p.SFCS02430_GenericClass from A094_GenericClass 
 *         

 */

