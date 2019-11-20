using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0190_Argument_RefOut
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 3;
            Sqr(a);     //...●0010.값을 복사해서 전달.
            Console.WriteLine("Value: {0}", a); // 3이 출력됩니다.

            int b = 3;
            Sqr(ref b);  //...●0020.참조를 전달.
            Console.WriteLine("ref: {0}", b); // 9가 출력됩니다.

            string name;
            int id;
            GetName(out name, out id);  //...●0030.출력.
            Console.WriteLine("out: {0} {1}", name, id);
        }

        static void Sqr(int x)
        {
            x = x * x;
        }
        static void Sqr(ref int x)
        {
            x = x * x;            
        }
        static void GetName(out string name, out int id)
        {
            Console.Write("Enter Name: ");
            name = Console.ReadLine();
            Console.Write("Enter Id: ");
            id = int.Parse(Console.ReadLine());
        }
    }
}


/*
 * _CS200_274p.SFCS0190_Argument_RefOut from A089_MethodArguments 
 *         

 */
