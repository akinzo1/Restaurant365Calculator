namespace Restaurant365.Services.Services;

public class CalculatorService : ICalculatorService
{

    public (string Formula, int Result) Calculate(string input, int maxConstraint = 2)
    {
        //Convert invalid numbers, empty input or missing numbers to 0
        var inputEntries = input.Split(',').ToList().Select(c =>
        {
            if (int.TryParse(c, out _))
                return Int32.Parse(c);

            if (decimal.TryParse(c, out decimal n))
                return Convert.ToInt32(n);

            if (!c.All(Char.IsDigit) || c.Length == 0)
                c = "0";
           
            return Int32.Parse(c);
        }).ToList();

        //Check if we exceed the maximum constraint.
        //Made this an argument so that we can keep
        //the unit tests beyond this requirement for readability
        //Default currently set at 2. I intend on using null to remove the constraint
        if (inputEntries.Count <= maxConstraint)
        {
            var total = inputEntries.Sum();
            var displayFormula = string.Join(" + ", inputEntries);
            return (displayFormula, total);

        }
        else
        {
            throw new InvalidOperationException($"Max constraint of {maxConstraint} exceeded");
        }

    }
}
