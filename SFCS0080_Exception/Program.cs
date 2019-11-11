using System;

namespace SFCS0080_Exception
{
    class Program
    {

        static int[] ar = { 1, 2, 3, 4, 5 };
        static int idx = 8;
        
        static void Main(string[] args)
        {
            testException();

        }

        private static void testException()
        {
            try
            {
                Methodl();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.Source);
                Console.WriteLine(e.TargetSite);
                Console.WriteLine(e.StackTrace);
            }
        }

        static public void Methodl()
        {
            Console.WriteLine(ar[idx]);
        }
    }
}
