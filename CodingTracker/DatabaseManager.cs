using Microsoft.Data.SqlClient;

internal class DatabaseManager
{
    internal void CreateTable(string connectionString)
    {
        using (var connection = new SqlConnection(connectionString))
        {
             connection.Open();

            using (SqlCommand command = new SqlCommand()
)
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'CodingTracker') 
                        CREATE DATABASE CodingTracker";
                command.Connection = connection;
                command.ExecuteNonQuery();
                Console.WriteLine("DB created or existed");
            }


            using (SqlCommand command = new SqlCommand())
            {
                command.CommandText = @"USE CodingTracker IF NOT EXISTS 
                        (SELECT * FROM sys.tables  WHERE name = 'Coding')
                        CREATE TABLE Coding(Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, 
                        Date VARCHAR(12) NOT NULL, Duration VARCHAR(10) NOT NULL)";
                command.Connection = connection;
                command.ExecuteNonQuery();
                Console.WriteLine("table created or existed");
            }
               
        }
    }
}