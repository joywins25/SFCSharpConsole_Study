using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0150_Inheritance
{
    class CoffeeMachine
    {
        protected string coffeeName;

        public CoffeeMachine(string Name)
        {
            this.coffeeName = Name;
            Console.WriteLine("CoffeeMachine 생성자 {0}", this.coffeeName);
            Console.WriteLine("\n");
        }

        ~CoffeeMachine()  //...●0010.소멸자 기호
        {
            Console.WriteLine("CoffeeMachine 소멸자 {0}", this.coffeeName);
            Console.WriteLine("\n");
        }

        public void makeCoffee()
        {
            Console.WriteLine("CoffeeMachine makeCoffee {0}", coffeeName);
            Console.WriteLine("\n");
        }
    }

    class NesCafeCoffeeMachine : CoffeeMachine  //...●0020.파생클래스 정의 형식
    {
        public NesCafeCoffeeMachine(string Name) : base(Name)
        {
            Console.WriteLine("NesCafeCoffeeMachine 생성자 {0}", this.coffeeName);
            Console.WriteLine("\n");
        }

        ~NesCafeCoffeeMachine()
        {
            Console.WriteLine("NesCafeCoffeeMachine 소멸자 {0}", this.coffeeName);
            Console.WriteLine("\n");
        }

        public void makeNescafeCoffee()
        {
            Console.WriteLine("makeNescafeCoffee {0}", coffeeName);
            Console.WriteLine("\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //CoffeeMachine cm = new CoffeeMachine("Americano");
            //cm.makeCoffee();

            NesCafeCoffeeMachine nc = new NesCafeCoffeeMachine("Supreme"); //...●0030.자식클래스의 생성자
            nc.makeCoffee();
            nc.makeNescafeCoffee();
        }
    }
}

/*
 * _HB223p 
 * 

 */
