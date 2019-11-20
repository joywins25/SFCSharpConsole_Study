using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFCS0120_SQLServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Connect to SQL Server and demo Create, Read, Update and Delete operations.");

                //...0010.Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";   // update me
                builder.UserID = "sa";              // update me
                builder.Password = "mssql";      // update me
                builder.InitialCatalog = "master";

                //...0020.Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");

                    //...0030.Create a sample database
                    Console.Write("Dropping and creating database 'SampleDB' ... ");
                    String sql = "DROP DATABASE IF EXISTS [SampleDB]; CREATE DATABASE [SampleDB]";
                    using (SqlCommand command = new SqlCommand(sql, connection)) //...0031. SqlCommand 클래스
                    {
                        command.ExecuteNonQuery(); //...0032. SqlCommand.ExecuteNonQuery 메서드
                        Console.WriteLine("Done.");
                    }

                    //...0040.Create a Table and insert some sample data
                    Console.Write("Creating sample table with data, press any key to continue...");
                    Console.ReadKey(true);
                    StringBuilder sb = new StringBuilder();   //...0041.StringBuilder
                    sb.Append("USE SampleDB; ");
                    sb.Append("CREATE TABLE Employees ( ");
                    sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
                    sb.Append(" Name NVARCHAR(50), ");
                    sb.Append(" Location NVARCHAR(50) ");
                    sb.Append("); ");
                    sb.Append("INSERT INTO Employees (Name, Location) VALUES ");
                    sb.Append("(N'Jared', N'Australia'), ");   //...0042.접두사N : nchar, nvarchar.
                    sb.Append("(N'Nikita', N'India'), ");
                    sb.Append("(N'Tom', N'Germany'); ");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();  //...0043.SqlCommand.ExecuteNonQuery 메서드
                        Console.WriteLine("Done.");
                    }

                    //...0050.INSERT demo
                    Console.Write("Inserting a new row into table, press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("INSERT Employees (Name, Location) ");
                    sb.Append("VALUES (@name, @location);");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", "Jake"); //...0051.SqlCommand.Parameters 속성
                        command.Parameters.AddWithValue("@location", "United States");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) inserted");
                    }

                    //...0060.UPDATE demo
                    String userToUpdate = "Nikita";
                    Console.Write("Updating 'Location' for user '" + userToUpdate + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("UPDATE Employees SET Location = N'United States' WHERE Name = @name");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) updated");
                    }

                    //...0070.DELETE demo
                    String userToDelete = "Jared";
                    Console.Write("Deleting user '" + userToDelete + "', press any key to continue...");
                    Console.ReadKey(true);
                    sb.Clear();
                    sb.Append("DELETE FROM Employees WHERE Name = @name;");
                    sql = sb.ToString();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " row(s) deleted");
                    }

                    //...0080.SELECT demo
                    Console.WriteLine("Reading data from table, press any key to continue...");
                    Console.ReadKey(true);
                    sql = "SELECT Id, Name, Location FROM Employees;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = command.ExecuteReader())  //...0081.SqlDataReader 클래스
                        {
                            while (reader.Read())  //...0082.SqlDataReader.Read 메서드
                            {
                                Console.WriteLine("{0} {1} {2}", 
                                                    reader.GetInt32(0), //...0083.SqlDataReader.GetInt32(Int32) 메서드
                                                    reader.GetString(1), reader.GetString(2)); //...0084.SqlDataReader.GetString(Int32) 메서드
                            }
                        }
                    }
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
 * ...0031. SqlCommand 클래스
 * 
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand?view=netframework-4.8
    
    ...☎ SFCS0122_SqlCommand

    ...0032. SqlCommand.ExecuteNonQuery 메서드
    ...0043.SqlCommand.ExecuteNonQuery 메서드
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8#System_Data_SqlClient_SqlCommand_ExecuteNonQuery
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand.executenonquery?view=netframework-4.8
    연결에 대한 Transact-SQL 문을 실행하고 영향을 받는 행의 수를 반환합니다.
    데이터베이스의 구조를 쿼리하거나 테이블 등의 데이터베이스 개체를 만드는 경우) UPDATE, INSERT 또는 DELETE 문을 실행하여
    DataSet를 사용 하지 않고 데이터베이스의 데이터를 변경할 수 있습니다.

 */


/*
 * ...0041.StringBuilder
 * 
    String 및 StringBuilder 형식 :
    https://docs.microsoft.com/ko-kr/dotnet/api/system.text.stringbuilder?view=netframework-4.8#StringAndSB
    String 값이 변경될 때 마다 매번 새로운 메모리의 객체를 생성하여 참조하게 되어 메모리를 차지합니다.

    ...☎ SFCS0121_StringBuilder.


   ...0042.접두사N : nchar, nvarchar.
   
    INSERT (Transact-SQL) - SQL Server | Microsoft Docs
    https://docs.microsoft.com/en-us/sql/t-sql/statements/insert-transact-sql?redirectedfrom=MSDN&view=sql-server-ver15
    nchar, nvarchar : 유니코드용.

 */

/*
 * ...0051.SqlCommand.Parameters 속성
 * 
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqlcommand.parameters?view=netframework-4.8#System_Data_SqlClient_SqlCommand_Parameters

 */


/*
 * ...0081.SqlDataReader 클래스
 * 
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqldatareader?view=netframework-4.8

    ...0082.SqlDataReader.Read 메서드
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqldatareader.read?view=netframework-4.8#System_Data_SqlClient_SqlDataReader_Read

    ...0083.SqlDataReader.GetInt32(Int32) 메서드
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqldatareader.getint32?view=netframework-4.8#System_Data_SqlClient_SqlDataReader_GetInt32_System_Int32_

    ...0084.SqlDataReader.GetString(Int32) 메서드
    https://docs.microsoft.com/ko-kr/dotnet/api/system.data.sqlclient.sqldatareader.getstring?view=netframework-4.8

 */
