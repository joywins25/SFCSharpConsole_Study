using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0140_Console2WinForm
{
    class Program:System.Windows.Forms.Form //...●0010.참조에서 System.Windows.Forms 추가합니다.
    {
        static void Main(string[] args)
        {
            System.Windows.Forms.Application.Run(new Program()); //...●0020.System.Windows.Forms.Application 클래스가 하는 일
            //System.Windows.Forms.Application.Exit();
        }
    }
}

/*
 * _HB592p 
 * 

 */
