using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0110_SQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(); //...0010.SqlConnectionStringBuilder :  https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder?view=netframework-4.8 
                builder.DataSource = "localhost";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "mssql";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString)) //...0020.using : https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/using-statement
                {
                    connection.Open(); //...0030.SqlConnection : https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlconnection?view=netframework-4.8
                    Console.WriteLine("Done.");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
}


/*
 * _SFCS0110_SQLServer
 * 
    Step 2.1 Create a C# app that connects to SQL Server and executes queries
    https://www.microsoft.com/en-us/sql-server/developer-get-started/csharp/win/step/2.html

 */



/*
 * ...0010.using : https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/using-statement
 * 
    수명이 다한 경우, 자동으로 소멸되게끔 함.

 */
