using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0122_SqlCommand
{
    class Program
    {
        static void Main(string[] args)
        {

            
            ReadOrderData();
        }

        private static void ReadOrderData()
        {
            // Build connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(); //...0010.SqlConnectionStringBuilder :  https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder?view=netframework-4.8 
            builder.DataSource = "localhost";   // update me
            builder.UserID = "sa";              // update me
            builder.Password = "mssql";      // update me
            builder.InitialCatalog = "master";

            // READ demo
            Console.WriteLine("Reading data from table, press any key to continue...");
            Console.ReadKey(true);

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();
                StringBuilder sb = new StringBuilder();   
                sb.Append("USE MySchool; ");
                sb.Append("SELECT CourseID, Title FROM Course;");
                String sql = sb.ToString();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {

                    using (SqlDataReader reader = command.ExecuteReader()) //...0010.ExecuteReader()
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                        }
                    }
                }

            }
        }
    }
}


/*
 * ...☎ SFCS0122_SqlCommand
 * 
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8

    ...0010.ExecuteReader()
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand.executereader?view=netframework-4.8#System_Data_SqlClient_SqlCommand_ExecuteReader
 */
