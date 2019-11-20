using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0160_Inheritance
{
    class CoffeeMachine
    {
        protected string coffeeName;  //...●0010.protected 접근 한정자
        protected int cup;
        private string beanOrigin;

        public CoffeeMachine(string _name, int _cup)
        {
            coffeeName = _name;
            cup = _cup;
            beanOrigin = "한국";
        }

        public virtual void order()  //...●0020.오버라이드 조건
        {
            System.Console.WriteLine("커피머신에서 주문한 커피이름은 {0}이고, {1} 컵을 주문하셨습니다", coffeeName, cup);
        }

        public virtual void like(string sth) //...●0020.오버라이드 조건
        {
            System.Console.WriteLine("커피머신은 {0}를 좋아합니다.", sth);
        }

        public void thankyou() //...●0030.메소드 숨기기
        {
            System.Console.WriteLine("커피머신을 이용해주셔서 감사합니다!");
        }

        public string getCountry()
        {
            string msg = "커피원두의 원산지는 " + this.beanOrigin + " 입니다.";
            return msg;
        }

        public virtual void giveStick()  //...●0040.메소드 봉인하기
        {
            System.Console.WriteLine("커피머신이 스틱을 제공합니다.");
        }
    }

    sealed class Starbucks  //...●0050.클래스 봉인하기
    {
        protected string coffeeName;
        public Starbucks(string _name)
        {
            this.coffeeName = _name;
        }
    }

    class NesCafeCoffeeMachine : CoffeeMachine //...●0020.상속받은 클래스
    {
        private int sugar;

        public NesCafeCoffeeMachine(string _name, int _cup, int _sugar) : base(_name, _cup)
        {
            sugar = _sugar;
        }

        public override void order() //...●0020.오버라이드 조건
        {
            base.order(); //...●0030.부모클래스 메소드 호출
            System.Console.WriteLine("설탕은 {0} 스푼 입니다.", sugar);
        }


        public new void thankyou() //...●0030.메소드 숨기기
        //public void thankyou()
        {
            System.Console.WriteLine("네스카페 커피머신을 이용해주셔서 감사합니다!");
        }

        public void useMugcup()
        {
            System.Console.WriteLine("네스카페 커피머신의 머그컵을 이용하겠습니다.");
        }

        public sealed override void giveStick()  //...●0040.메소드 봉인하기
        {
            System.Console.WriteLine("네스카페 커피머신이 스틱을 제공합니다.");
        }

    }

    class NesCafe : NesCafeCoffeeMachine
    {
        public NesCafe(string _name, int _cup, int _sugar) : base(_name, _cup, _sugar)
        {
            System.Console.WriteLine("NesCafe 생성자");
        }

        //public override void giveStick()  //...●0040.메소드 봉인하기
        //{
        //    System.Console.WriteLine("NesCafe 봉인된 메소드 사용불가.");
        //}

    }

    class TeaMachine
    {
        protected string teaName;

        public TeaMachine(string _name)
        {
            teaName = _name;
        }
    }

    class MaximCoffeeMachine : CoffeeMachine 
    {
        private int sugar;

        public MaximCoffeeMachine(string _name, int _cup, int _sugar) : base(_name, _cup)
        {
            sugar = _sugar;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            NesCafeCoffeeMachine nccm;
            nccm = new NesCafeCoffeeMachine("Supreme", 2, 1);
            nccm.order();           //...자식꺼.
            nccm.thankyou();        //...자식꺼.
            nccm.like("겨울");      //...부모꺼.
            nccm.useMugcup();       //...자식꺼.
            System.Console.WriteLine(nccm.getCountry());    //...부모꺼.

            CoffeeMachine cm = new NesCafeCoffeeMachine("아메리카노", 3, 2);
            cm.thankyou(); //...●0030.메소드 숨기기

            CoffeeMachine cm2 = new CoffeeMachine("아메리카노", 3);
            cm2.thankyou(); //...●0030.메소드 숨기기

            TeaMachine tm = new TeaMachine("우엉차");

            Console.WriteLine("is연산자...");
            Console.WriteLine(tm is TeaMachine ? true : false); //...●0060.is 연산자
            Console.WriteLine(nccm is TeaMachine ? true : false);
            Console.WriteLine(nccm is NesCafeCoffeeMachine ? true : false);
            Console.WriteLine(cm is NesCafeCoffeeMachine ? true : false);
            Console.WriteLine(cm2 is NesCafeCoffeeMachine ? true : false);
            Console.WriteLine(cm2 is CoffeeMachine ? true : false);

        }
    }
}

/*
 * _DN244p 
 * 

 */

