using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0170_As
{
    class CoffeeMachine
    {
        public void order()
        {
            System.Console.WriteLine("CoffeeMachine order...");
        }
    }

    class MaximCoffee : CoffeeMachine
    {
        public void addSugar()
        {
            System.Console.WriteLine("MaximCoffee addSugar..");
        }
    }

    class FrenchCoffee : CoffeeMachine
    {
        public void addSyrup()
        {
            System.Console.WriteLine("FrenchCoffee addSyrup..");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("as연산자...");
            CoffeeMachine cm3 = new MaximCoffee();
            MaximCoffee cm4 = cm3 as MaximCoffee; //...●0010.as 연산자
            if (cm4 != null)
            {
                Console.WriteLine(true);
            }
            else
            {
                Console.WriteLine(false);

            }

            CoffeeMachine cm5 = new FrenchCoffee();
            MaximCoffee cm6 = cm5 as MaximCoffee;
            if (cm6 != null)
            {
                Console.WriteLine(true);
            }
            else
            {
                Console.WriteLine(false);

            }
        }
    }
}
/*
 * _DN231p 
 * 

 */
