using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SFCS02510_MessageFilter
{
    class MessageFilter : IMessageFilter //...●0010.참조에서 System.Windows.Forms 추가합니다.
    {
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x0F || m.Msg == 0xA0 ||
                m.Msg == 0x200 || m.Msg == 0x113)
                return false;

            Console.WriteLine("{0} : {1}", m.ToString(),
                m.Msg);

            if (m.Msg == 0x201)
                Application.Exit();

            return true;
        }
    }

    class MainApp : Form
    {
        static void Main(string[] args)
        {
            Application.AddMessageFilter(new MessageFilter());
            Application.Run(new MainApp());
        }
    }
}

/*
 * _HB599p.SFCS02510_MessageFilter from MessageFilter 
 *         

 */

