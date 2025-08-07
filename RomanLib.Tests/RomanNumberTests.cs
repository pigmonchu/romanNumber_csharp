namespace RomanLib.Tests;

public class RomanNumberTests
{
    [Fact]
    public void Uno_DeberiaSer_I()
    {
        Assert.Equal("I", new RomanNumber(1).representation);
    }

    [Theory]
    [InlineData(1, "I")]
    [InlineData(2, "II")]
    [InlineData(3, "III")]
    [InlineData(4, "IV")]
    [InlineData(5, "V")]
    [InlineData(6, "VI")]
    [InlineData(9, "IX")]

    [InlineData(10, "X")]
    [InlineData(14, "XIV")]
    [InlineData(19, "XIX")]
    [InlineData(20, "XX")]
    [InlineData(39, "XXXIX")]

    [InlineData(40, "XL")]
    [InlineData(44, "XLIV")]
    [InlineData(49, "XLIX")]
    [InlineData(50, "L")]
    [InlineData(59, "LIX")]

    [InlineData(90, "XC")]
    [InlineData(99, "XCIX")]
    [InlineData(100, "C")]
    [InlineData(101, "CI")]
    [InlineData(149, "CXLIX")]

    [InlineData(400, "CD")]
    [InlineData(444, "CDXLIV")]
    [InlineData(500, "D")]
    [InlineData(666, "DCLXVI")]
    [InlineData(900, "CM")]

    [InlineData(944, "CMXLIV")]
    [InlineData(1000, "M")]
    [InlineData(1111, "MCXI")]
    [InlineData(1492, "MCDXCII")]
    [InlineData(1987, "MCMLXXXVII")]

    [InlineData(2024, "MMXXIV")]
    [InlineData(2421, "MMCDXXI")]
    [InlineData(2999, "MMCMXCIX")]
    [InlineData(3000, "MMM")]
    [InlineData(3999, "MMMCMXCIX")]

    public void Arabigo_a_romano_menor_10(int value, string representation)
    {
        var result = new RomanNumber(value);
        Assert.Equal(representation, result.representation);
    }

    [Theory]
    [InlineData("I", 1)]
    [InlineData("II", 2)]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("VI", 6)]
    [InlineData("IX", 9)]

    [InlineData("X", 10)]
    [InlineData("XIV", 14)]
    [InlineData("XIX", 19)]
    [InlineData("XX", 20)]
    [InlineData("XXXIX", 39)]

    [InlineData("XL", 40)]
    [InlineData("XLIV", 44)]
    [InlineData("XLIX", 49)]
    [InlineData("L", 50)]
    [InlineData("LIX", 59)]

    [InlineData("XC", 90)]
    [InlineData("XCIX", 99)]
    [InlineData("C", 100)]
    [InlineData("CI", 101)]
    [InlineData("CXLIX", 149)]

    [InlineData("CD", 400)]
    [InlineData("CDXLIV", 444)]
    [InlineData("D", 500)]
    [InlineData("DCLXVI", 666)]
    [InlineData("CM", 900)]

    [InlineData("CMXLIV", 944)]
    [InlineData("M", 1000)]
    [InlineData("MCXI", 1111)]
    [InlineData("MCDXCII", 1492)]
    [InlineData("MCMLXXXVII", 1987)]

    [InlineData("MMXXIV", 2024)]
    [InlineData("MMCDXXI", 2421)]
    [InlineData("MMCMXCIX", 2999)]
    [InlineData("MMM", 3000)]
    [InlineData("MMMCMXCIX", 3999)]

    public void Romano_a_arabigo_menor_4000(string representation, int value)
    {
        var result = new RomanNumber(representation);
        Assert.Equal(value, result.value);
    }
    
    
}