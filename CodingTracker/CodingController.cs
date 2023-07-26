using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Text;

internal class CodingController
{
    string connectionString = ConfigurationManager.ConnectionStrings["CodingTracker"].ConnectionString;

    internal void Post(Coding coding)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $"use CodingTracker INSERT INTO Coding VALUES ('{coding.Date}', '{coding.Duration}')";
                sqlCommand.ExecuteNonQuery();
            }
        }
    }

    internal void Get()
    {
        List<Coding> list = new List<Coding>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $"use CodingTracker SELECT * FROM Coding";

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            list.Add(new Coding
                            {
                                Id = sqlDataReader.GetInt32(0),
                                Date = sqlDataReader.GetString(1),
                                Duration = sqlDataReader.GetString(2),
                            });
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\nNo data found");
                    }
                }
            }
            TableVisualisation.ShowTable(list);
        }
    }

    internal Coding GetById(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $"use CodingTracker SELECT * FROM Coding WHERE Id = {id}";
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Coding coding = new();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        coding.Id = reader.GetInt32(0);
                        coding.Date = reader.GetString(1);
                        coding.Duration = reader.GetString(2);
                    }

                    Console.WriteLine("\n\n");

                    return coding;
                }
            }
        }
    }

    internal void Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $"use CodingTracker DELETE FROM Coding WHERE Id = {id}";
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"\nEntry with id {id} was successfully deleted. \n\n");

            }
        }
    }

    internal void Update(Coding codingResult)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                connection.Open();
                sqlCommand.CommandText = $@"use CodingTracker UPDATE Coding SET 
                            Date = '{codingResult.Date}',
                            Duration = '{codingResult.Duration}'
                            WHERE Id = {codingResult.Id}";
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine($"\nEntry with id {codingResult.Id} was successfully updated. \n\n");

            }
        }
    }
}