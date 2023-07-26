using Microsoft.Data.SqlClient;
using System.Configuration;

string connectionString = ConfigurationManager.ConnectionStrings["CodingTracker"].ConnectionString;

DatabaseManager databaseManager = new();
GetUserInput getUserInput = new();
databaseManager.CreateTable(connectionString);

getUserInput.MainMenu();
