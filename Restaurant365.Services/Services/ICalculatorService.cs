namespace Restaurant365.Services.Services;

public interface ICalculatorService
{
    /// <summary>
    /// Operates addition on an input string. This function works for integer and decimal numbers. Decimals are converted to Integer whole numbers
    /// </summary>
    /// <param name="input">String to be operated on</param>
    /// <param name="maxConstraint">Maximum number of entries after delimiter split. Set to Null to remove constraint</param>
    /// <param name="supportNewLineDelimiter">Support New line delimiter</param>
    /// <param name="allowNegativeNumbers">Allow negative numbers to be processed in input string</param>
    /// <param name="upperBounds">Do not process numbers greater</param>
    /// <returns>A formula as well as an integer result of the Operation</returns>
    public (string Formula, int Result) Calculate(string input, int? maxConstraint = null, bool supportNewLineDelimiter = false, bool allowNegativeNumbers = true, int upperBounds = int.MaxValue);
}
