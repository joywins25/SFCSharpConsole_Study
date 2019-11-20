using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS02472_DelegateEvent
{
    //...① 델리게이트를 클래스 안 또는 바깥에 선언합니다.
    delegate void YesEventHandler(string message);

    class MyNotifier
    {
        //...② 앞서 선언해놓은 델리게이트의 인스턴스를 event 키워드로 수식해서 선언합니다.
        public event YesEventHandler SomethingHappened;


        public void DoSomething(int number)
        {
            int temp = number % 10;

            if (temp != 0 && temp % 3 == 0)
            {
                //...② 이벤트가 발생합니다.
                SomethingHappened(String.Format("{0} : 짝", number));
            }
        }
    }

    class MainApp
    {
        //...③ ①에서 선언한 델리게이트와 일치하는 메소드 형태로 이벤트 핸들러를 작성합니다.
        static public void MyHandler(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            //...④ 클래스의 인스턴스를 생성하고, 이 객체의 이벤트에 ③에서 작성한 이벤트 핸들러를 등록합니다.
            MyNotifier notifier = new MyNotifier();
            notifier.SomethingHappened += new YesEventHandler(MyHandler);

            for (int i = 1; i < 30; i++)
            {
                //...⑤ 이벤트가 발생하면 이벤트 핸들러가 호출됩니다.
                notifier.DoSomething(i);
            }
        }
    }
}

/*
 * _HB401p.SFCS02472_DelegateEvent from EventTest
 *         
    이벤트 : 
        알람시계처럼 특정 시간이 되면 알람이 우는 것 처럼 사용자가 버튼을 클릭했을 때와 같이 
        어떤 일이 생겼을 때 이를 알려주는 객체 입니다.        
        이벤트는 event 키워드로 수식한 델리게이트입니다.

    이벤트와 델리게이트의 차이점 :
        이벤트는 외부에서 직접 사용할 수 없습니다.
        이벤트는 public으로 선언되어 있어도 자신이 선언되어 있는 클래스 외부에서는 호출이 불가능합니다.
        그래서, 이벤트는 객체 자신의 상태 변화나 사건의 발생을 알리는 용도로 사용합니다.

        델리게이트는 public 또는 internal로 선언되면 클래스 외부에서도 호출이 가능합니다.
        델리게이트는 콜백 용도로 사용합니다.
 */

