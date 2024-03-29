﻿using Restaurant365.Utility;

namespace Restaurant365.Services.Services;

public class CalculatorService : ICalculatorService
{

    public (string Formula, int Result) Calculate(string input, int? maxConstraint = null, bool supportNewLineDelimiter = true, bool allowNegativeNumbers = true, int upperBounds = int.MaxValue)
    {
        var delimiterList = input.GetDelimiterList(supportNewLineDelimiter);

        //Remove delimiter portion of the string. Account for length of newline character
        input = input.StartsWith("//") ? input.Substring(input.IndexOf(@"\n") + 2)  : input;

        //Convert invalid numbers, empty input or missing numbers to 0
        var inputEntries = input.Split(delimiterList.ToArray(), StringSplitOptions.None).ToList().Select(c =>
        {
            if (int.TryParse(c, out int d))
                return d <= upperBounds ? d : 0;

            if (decimal.TryParse(c, out decimal n))
                return Convert.ToInt32(n);

            if (!c.All(Char.IsDigit) || c.Length == 0)
                c = "0";

            return Int32.Parse(c);
        }).ToList();

        if (!allowNegativeNumbers)
        {
            var negativeNumbers = inputEntries.Where(o => o < 0).ToList();
            //throw error only if there are actually negative numbers
            if (negativeNumbers.Any())
            {
                throw new InvalidOperationException($"{string.Join(",", negativeNumbers)} not allowed");
            }
        }


        //Check if we exceed the maximum constraint.
        if (!maxConstraint.HasValue || inputEntries.Count <= maxConstraint)
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
