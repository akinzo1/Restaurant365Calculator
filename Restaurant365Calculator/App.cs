using Restaurant365.Services.Services;
using System.Text.RegularExpressions;

namespace Restaurant365Calculator;

public class App
{
    private readonly ICalculatorService _service;
    public App(ICalculatorService service)
    {
        _service = service;
    }

    public void Run(string[] args)
    {
        bool endApp = false;
        Console.WriteLine("Restaurant 365 Challenge Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {

            try
            {
                //Enter initial input
                Console.Write("Enter an Input: ");
                var input = Console.ReadLine() ?? "";

                //Support New Line?
                Console.WriteLine("Support New Line Character? ");
                Console.WriteLine("\ty - Yes");
                Console.WriteLine("\tn - No");
                Console.Write("Your option? ");

                string supportNewLine = Console.ReadLine() ?? "";

                // Validate New Line entry matches the pattern
                if (string.IsNullOrEmpty(supportNewLine) || (!supportNewLine.ToUpper().Equals("Y") && !supportNewLine.ToUpper().Equals("N")))
                {
                    Console.WriteLine("Error: Unrecognized input for New Line Character. Please try again");

                    while (string.IsNullOrEmpty(supportNewLine) || (!supportNewLine.ToUpper().Equals("Y") && !supportNewLine.ToUpper().Equals("N")))
                    {
                        Console.Write("Enter valid response as requested above: ");
                        supportNewLine = Console.ReadLine() ?? "";
                    }
                }

                var isNewLineSupported = !string.IsNullOrEmpty(supportNewLine) && supportNewLine.ToUpper().Equals("Y");

                //Support Negative Numbers
                Console.WriteLine("Allow Negative Characters? ");
                Console.WriteLine("\ty - Yes");
                Console.WriteLine("\tn - No");
                Console.Write("Your option? ");

                string allowNegativeCharacters = Console.ReadLine() ?? "";

                // Validate negative entry matches the pattern
                if (string.IsNullOrEmpty(allowNegativeCharacters) || (!allowNegativeCharacters.ToUpper().Equals("Y") && !allowNegativeCharacters.ToUpper().Equals("N")))
                {
                    Console.WriteLine("Error: Unrecognized input for Negative Characters. Please try again");

                    while (string.IsNullOrEmpty(allowNegativeCharacters) || (!allowNegativeCharacters.ToUpper().Equals("Y") && !allowNegativeCharacters.ToUpper().Equals("N")))
                    {
                        Console.Write("Enter valid response as requested above: ");
                        allowNegativeCharacters = Console.ReadLine() ?? "";
                    }
                }

                var isNeNegativeSupported = !string.IsNullOrEmpty(allowNegativeCharacters) && allowNegativeCharacters.ToUpper().Equals("Y");


                Console.Write("Enter an Upper Bound (Numbers greater than your entry will not be processed): ");
                var largeEntry = Console.ReadLine();

                int largeNumber;
                while (!int.TryParse(largeEntry, out largeNumber))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    largeEntry = Console.ReadLine();
                }

                //Perform Calculation
                var result = _service.Calculate(input, null, isNewLineSupported, isNeNegativeSupported, largeNumber);
                Console.WriteLine($"{result.Formula} = {result.Result}");

                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("To close the app, press 'Ctrl + C' or press 'n' and Enter. Press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); 

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        return;
    }
}
