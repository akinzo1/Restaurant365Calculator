using Restaurant365.Services.Services;

namespace Restaurant365.Tests.Services;

public class CalculatorServiceTests
{
    [Fact]
    public void Calculate_NotNull()
    {
        var input = "2,3";
        CalculatorService service = new();
        var message = service.Calculate(input);
        Assert.True(!string.IsNullOrEmpty(message.Result.ToString()));

    }

    [Fact]
    public void Calculate_MaxConstraintExceeded_ShouldThrowException()
    {
        var input = "2,3,4";
        var maxConstraint = 2;
        CalculatorService service = new();

        var exceptionType = typeof(InvalidOperationException);
        
        Assert.Throws(exceptionType, () => {
            service.Calculate(input, maxConstraint);
        });

    }

    [Fact]
    public void Calculate_MissingNumbers()
    {
        var input = ",";
        var maxConstraint = 2;
        var expected = 0;
        CalculatorService service = new();
        var message = service.Calculate(input, maxConstraint);
        Assert.Equal(expected, message.Result);
    }

    [Fact]
    public void Calculate_NegativeNumbers()
    {
        var input = "-1,";
        var maxConstraint = 2;
        var expected = -1;
        CalculatorService service = new();
        var message = service.Calculate(input, maxConstraint);
        Assert.Equal(expected, message.Result);
    }

    [Fact]
    public void Calculate_MeetsMaxConstraint()
    {
        CalculatorService service = new();
        var input = "2,3";
        var maxConstraint = 2;
        var expected = 5;
        var message = service.Calculate(input, maxConstraint);
        Assert.Equal(expected, message.Result);

    }
    [Fact]
    public void Calculate_MeetsMaxConstraint_DecimalIncluded()
    {
        CalculatorService service = new();
        var input = "22.7,3";
        var maxConstraint = 2;
        var expected = 26;
        var message = service.Calculate(input, maxConstraint);
        Assert.Equal(expected, message.Result);

    }
}
