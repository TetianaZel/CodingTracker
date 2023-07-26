using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

internal class GetUserInput
{
    CodingController codingController = new();
    internal void MainMenu()
    {
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to View records");
            Console.WriteLine("Type 2 to Add records");
            Console.WriteLine("Type 3 to Delete records");
            Console.WriteLine("Type 4 to Update records\n\n");

            string commandInput = Console.ReadLine();

            while (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\n Invalid Command. Please type a number from 0 to 4. \n");
                commandInput = Console.ReadLine();
            }

            switch (commandInput)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    codingController.Get();
                    break;
                case "2":
                    ProcessAdd();
                    break;
                case "3":
                    ProcessDelete();
                    break;
                case "4":
                    ProcessUpdate();
                    break;
                //case "5":
                //    ProcessReport();
                //    break;

                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    break;
            }
        }
    }

    private void ProcessUpdate()
    {
        codingController.Get();
        Console.WriteLine("Please type id of entry you wish to update or 0 to exit to Main Menu");
        string idInput = Console.ReadLine();
        while (!Int32.TryParse(idInput, out _) || string.IsNullOrEmpty(idInput) || Int32.Parse(idInput) < 0)
        {
            Console.WriteLine("Incorrect input. Id should be positive integer. Press 0 to return to Main Menu");
            idInput = Console.ReadLine();
        }

        int id = Int32.Parse(idInput);

        if (id == 0) MainMenu();

        Coding codingResult = codingController.GetById(id);

        while (codingResult.Id == 0)
        {
            Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
            ProcessUpdate();
        }

        var updateInput = "";

        bool updating = true;
        while (updating == true)
        {
            Console.WriteLine($"\nType 'd' for Date \n");
            Console.WriteLine($"\nType 't' for Duration \n");
            Console.WriteLine($"\nType 's' to save update \n");
            Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");

            updateInput = Console.ReadLine();

            switch (updateInput)
            {
                case "d":
                    codingResult.Date = GetDateInput();
                    break;

                case "t":
                    codingResult.Duration = GetDurationInput();
                    break;

                case "0":
                    MainMenu();
                    updating = false;
                    break;

                case "s":
                    updating = false;
                    break;

                default:
                    Console.WriteLine($"\nType '0' to Go Back to Main Menu \n");
                    break;
            }
        }
        codingController.Update(codingResult);
        MainMenu();
    }

    private void ProcessDelete()
    {
        codingController.Get();
        Console.WriteLine("Please type id of entry you wish to delete or 0 to exit to Main Menu");
        string idInput = Console.ReadLine();
        while (!Int32.TryParse(idInput, out _) || string.IsNullOrEmpty(idInput) || Int32.Parse(idInput) < 0)
        {
            Console.WriteLine("Incorrect input. Id should be positive integer. Press 0 to return to Main Menu");
            idInput = Console.ReadLine();
        }

        int id = Int32.Parse(idInput);

        if (id == 0) MainMenu();

        Coding codingResult = codingController.GetById(id);

        while (codingResult.Id  == 0)
        {
            Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
            ProcessDelete();
        }
        codingController.Delete(id);
        MainMenu();
    }

    private void ProcessAdd()
    {
        var date = GetDateInput();
        var duration = GetDurationInput();

        Coding coding = new(){ Date = date, Duration = duration};

        codingController.Post(coding);

        
    }

    private string GetDurationInput()
    {
        Console.WriteLine("\n\nPlease insert the duration: (Format: hh:mm). Type 0 to return to main menu.\n\n");

        string durationInput = Console.ReadLine();

        if (durationInput == "0") MainMenu();

        while (!TimeSpan.TryParseExact(durationInput, "h':'mm", CultureInfo.InvariantCulture, out _))
        {
            Console.WriteLine("\n\nDuration invalid. Please insert the duration: (Format: hh:mm) or type 0 to return to main menu.\n\n");
            durationInput = Console.ReadLine();
            if (durationInput == "0") MainMenu();
        }
        return durationInput;
    }

    internal string GetDateInput()
    {
        Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.\n\n");
        string dateInput = Console.ReadLine();

        if (dateInput == "0") MainMenu();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: dd-mm-yy.\n\n");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }
}