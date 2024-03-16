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

    [Fact]
    public void Calculate_NoMaxConstraint()
    {
        CalculatorService service = new();
        var input = "1,2,3,4,5,6,7,8,9,10,11,12";
        var expected = 78;
        var message = service.Calculate(input);
        Assert.Equal(expected, message.Result);

    }

    [Fact]
    public void Calculate_WithDelimiter()
    {
        CalculatorService service = new();
        var input = @"1\n2,3";
        var expected = 6;
        var message = service.Calculate(input, null, @"\n");
        Assert.Equal(expected, message.Result);

    }

    [Fact]
    public void Calculate_NegativeNumbersNotAllowed_ThrowException_ConfirmMessage()
    {
        var input = "-5, -4,3 ";
        var expectedMessage = "-5,-4";
        CalculatorService service = new();

        var exceptionType = typeof(InvalidOperationException);

        var ex = Record.Exception(() => {
            service.Calculate(input, null, string.Empty, false);
        });

        Assert.NotNull(ex);

        Assert.IsType(exceptionType, ex);

        Assert.Contains(expectedMessage, ex.Message);

    }

    [Fact]
    public void Calculate_NegativeNumbersNotAllowed_Pass()
    {
        var input = "5, 4,3 ";
        CalculatorService service = new();

        var exceptionType = typeof(InvalidOperationException);

        var ex = Record.Exception(() => {
            service.Calculate(input, null, string.Empty, false);
        });

        Assert.Null(ex);

    }

    [Fact]
    public void Calculate_WithUpperBounds()
    {
        var input = "2,1001,6";
        var expected = 8;
        CalculatorService service = new();

        var message = service.Calculate(input, null, string.Empty, true, 1000);
        Assert.Equal(expected, message.Result);

    }
}
