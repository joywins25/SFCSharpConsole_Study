using System;

namespace SFCS0100_Checked
{
    class Program
    {
        static void Main(string[] args)
        {
            //testOverFlow();
            testUnchecked(); 
        }



        private static void testUnchecked()
        {
            int i = 123456;
            short s;
            unchecked
            {
                s = (short)i;
                Console.WriteLine(s);
            }
        }

        private static void testOverFlow()
        {
            byte b;
            b = 255;
            b++;
            Console.WriteLine(b);

            b = 0;
            b--;
            Console.WriteLine(b);
        }
    }
}


