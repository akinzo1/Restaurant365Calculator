using Restaurant365.Services.Services;

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
        Console.Write("Enter Input: ");
        var input = Console.ReadLine() ?? "";
        var result =_service.Calculate(input, null, true, false, 1000);
        Console.WriteLine($"{result.Formula} = {result.Result}");
    }
}
