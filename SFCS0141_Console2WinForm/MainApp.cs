using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0141_Console2WinForm
{
    class MainApp : System.Windows.Forms.Form //...●0010.이름을 Program에서 MainApp으로 변경합니다.
    {
        static void Main(string[] args)
        {
            MainApp form = new MainApp();

            form.Click += new EventHandler( //...☎0040.SFCS0180_Property.
                (sender, eventArgs) =>
                {
                    Console.WriteLine("closing Window...");
                    System.Windows.Forms.Application.Exit();  //...●0030.Exit()가 하는 일.
                });

            Console.WriteLine("starting Windows Application...");
            System.Windows.Forms.Application.Run(form); //...●0020.MainApp타입의 객체 form을 대입합니다.

            Console.WriteLine("exiting Windows Application...");
        }
    }
}

/*
 * _HB594p 
 * 
 
 */
