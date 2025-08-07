namespace RomanLib.Tests;

public class RomanNumberTests
{
    [Fact]
    public void Uno_DeberiaSer_I()
    {
        Assert.Equal("I", new RomanNumber(1)._representation);
    }

    [Theory]
    [InlineData(1, "I")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(9, "IX")]
    public void Arabigo_a_romano_menor_10(int value, string representation)
    {
        var result = new RomanNumber(value);
        Assert.Equal(representation, result._representation);
    }
}