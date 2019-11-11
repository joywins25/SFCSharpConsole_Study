using System;

namespace SFCS0010_Enum
{
    class Program
    {
        enum DialogResult { YES, NO, CANCEL, CONFIRM, OK }
        enum DialogResult2 { YES = 10, NO, CANCEL, CONFIRM = 50, OK }

        static void Main(string[] args)
        {
            examEnum();
            examEnum2();
            examEnum3();
        }

        private static void examEnum3()
        {
            Console.WriteLine((int)DialogResult2.YES);
            Console.WriteLine((int)DialogResult2.NO);
            Console.WriteLine((int)DialogResult2.CANCEL);
            Console.WriteLine((int)DialogResult2.CONFIRM);
            Console.WriteLine((int)DialogResult2.OK);
        }

        private static void examEnum2()
        {
            DialogResult result = DialogResult.YES;

            Console.WriteLine(result == DialogResult.YES);
            Console.WriteLine(result == DialogResult.NO);
            Console.WriteLine(result == DialogResult.CANCEL);
            Console.WriteLine(result == DialogResult.CONFIRM);
            Console.WriteLine(result == DialogResult.OK);
        }

        private static void examEnum()
        {
            Console.WriteLine((int)DialogResult.YES);
            Console.WriteLine((int)DialogResult.NO);
            Console.WriteLine((int)DialogResult.CANCEL);
            Console.WriteLine((int)DialogResult.CONFIRM);
            Console.WriteLine((int)DialogResult.OK);
        }
    }
}


