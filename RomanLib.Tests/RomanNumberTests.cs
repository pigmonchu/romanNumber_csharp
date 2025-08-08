namespace RomanLib.Tests;
using System.Numerics;


public class RomanNumberTests
{
    [Fact]
    public void Uno_DeberiaSer_I()
    {
        Assert.Equal("I", new RomanNumber(new BigInteger(1)).Representation);
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
        var result = new RomanNumber(new BigInteger(value));
        Assert.Equal(representation, result.Representation);
    }

    [Theory]
    [InlineData("", 0)]

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
        Assert.Equal(new BigInteger(value), result.AbsValue);
    }

    [Theory]
    [InlineData("IIII", typeof(FormatException))]
    [InlineData("VV", typeof(FormatException))]
    [InlineData("IL", typeof(FormatException))]
    [InlineData("IC", typeof(FormatException))]
    [InlineData("XD", typeof(FormatException))]
    [InlineData("VX", typeof(FormatException))]
    [InlineData("LC", typeof(FormatException))]
    [InlineData("MMMM", typeof(FormatException))]
    [InlineData("IIV", typeof(FormatException))]
    [InlineData("IM", typeof(FormatException))]
    [InlineData("ABC", typeof(FormatException))]
    [InlineData(null, typeof(ArgumentNullException))]
    public void RomanNumber_errores_de_formato_al_crear(string representation, Type exceptionType)
    {
        var ex = Record.Exception(() => new RomanNumber(representation));
        Assert.NotNull(ex);
        Assert.IsType(exceptionType, ex);
    }

    public static IEnumerable<object[]> ExtendedRomanTestCases()
    {
        // Casos básicos con números pequeños para verificar compatibilidad
        yield return new object[] { BigInteger.Parse("4000"), "IV•" };
        yield return new object[] { BigInteger.Parse("10001"), "X•I" };
        yield return new object[] { BigInteger.Parse("1000000"), "M•" };
        yield return new object[] { BigInteger.Parse("1000001"), "M•I" };
        yield return new object[] { BigInteger.Parse("1234567"), "MCCXXXIV•DLXVII" };
        yield return new object[] { BigInteger.Parse("2000000"), "MM•" };
        yield return new object[] { BigInteger.Parse("3000000"), "MMM•" };
        yield return new object[] { BigInteger.Parse("4000000"), "IV••" };
        yield return new object[] { BigInteger.Parse("5673987"), "V••DCLXXIII•CMLXXXVII" };
        yield return new object[] { BigInteger.Parse("5678987"), "V••DCLXXVIII•CMLXXXVII" };
        yield return new object[] { BigInteger.Parse("3678987"), "MMMDCLXXVIII•CMLXXXVII" };
        yield return new object[] { BigInteger.Parse("1000000000"), "M••" };
        yield return new object[] { BigInteger.Parse("1000000001"), "M••I" };
        yield return new object[] { BigInteger.Parse("2147483647"), "MMCXLVII••CDLXXXIII•DCXLVII" };
        yield return new object[] { BigInteger.Parse("4001000"), "IV••I•" };
        yield return new object[] { BigInteger.Parse("4000001"), "IV••I" };
        
        // Casos con números muy grandes que aprovechan BigInteger
        yield return new object[] { BigInteger.Parse("4000000000"), "IV•••" };
        yield return new object[] { BigInteger.Parse("18446744073709551615"), "XVIII••••••CDXLVI•••••DCCXLIV••••LXXIII•••DCCIX••DLI•DCXV" };
        
        // Casos adicionales con BigInteger para números extremadamente grandes
        yield return new object[] { BigInteger.Parse("999999999999999999"), "CMXCIX•••••CMXCIX••••CMXCIX•••CMXCIX••CMXCIX•CMXCIX" };
        yield return new object[] { BigInteger.Parse("1000000000000000000"), "M•••••" };
    }

    [Theory]
    [MemberData(nameof(ExtendedRomanTestCases))]
    public void RomanNumber_GeneratesExtendedFormat(BigInteger value, string expectedRoman)
    {
        var roman = new RomanNumber(value);
        Assert.Equal(expectedRoman, roman.Representation);
    }

    public static IEnumerable<object[]> ExtendedRomanParseTestCases()
    {
        yield return new object[] { "IV•", BigInteger.Parse("4000") };
        yield return new object[] { "X•I", BigInteger.Parse("10001") };
        yield return new object[] { "M•", BigInteger.Parse("1000000") };
        yield return new object[] { "M•I", BigInteger.Parse("1000001") };
        yield return new object[] { "MCCXXXIV•DLXVII", BigInteger.Parse("1234567") };
        yield return new object[] { "MM•", BigInteger.Parse("2000000") };
        yield return new object[] { "MMM•", BigInteger.Parse("3000000") };
        yield return new object[] { "IV••", BigInteger.Parse("4000000") };
        yield return new object[] { "V••DCLXXIII•CMLXXXVII", BigInteger.Parse("5673987") };
        yield return new object[] { "V••DCLXXVIII•CMLXXXVII", BigInteger.Parse("5678987") };
        yield return new object[] { "MMMDCLXXVIII•CMLXXXVII", BigInteger.Parse("3678987") };
        yield return new object[] { "M••", BigInteger.Parse("1000000000") };
        yield return new object[] { "M••I", BigInteger.Parse("1000000001") };
        yield return new object[] { "MMCXLVII••CDLXXXIII•DCXLVII", BigInteger.Parse("2147483647") };
        yield return new object[] { "IV••I•", BigInteger.Parse("4001000") };
        yield return new object[] { "IV••I", BigInteger.Parse("4000001") };
        
        // Casos adicionales para BigInteger
        yield return new object[] { "IV•••", BigInteger.Parse("4000000000") };
        yield return new object[] { "XVIII••••••CDXLVI•••••DCCXLIV••••LXXIII•••DCCIX••DLI•DCXV", BigInteger.Parse("18446744073709551615") };
    }

    [Theory]
    [MemberData(nameof(ExtendedRomanParseTestCases))]
    public void RomanNumber_ParseExtendedRoman(string roman, BigInteger expected)
    {
        var number = new RomanNumber(roman);
        Assert.Equal(expected, number.AbsValue);
    }
    
    
    public static IEnumerable<object[]> RomanGroupTestCases
    {
        get
        {
            yield return new object[]
            {
                "DCII•••••••CCXIV••••••LXXV•••••CMXCIX••••CMXCIX•••CMLXXXVII••XXIII•DCCCLXXII",
                new List<(uint, int)>
                {
                    (602, 7), (214, 6), (75, 5), (999, 4), (999, 3), (987, 2), (23, 1), (872, 0)
                }
            };

            yield return new object[]
            {
                "DCII•••••••LXXV•••••CMXCIX••••CMXCIX•••CMLXXXVII••XXIII•DCCCLXXII",
                new List<(uint, int)>
                {
                    (602, 7), (75, 5), (999, 4), (999, 3), (987, 2), (23, 1), (872, 0)
                }
            };

            yield return new object[]
            {
                "DCII•••••••CCXIV••••••LXXV•••••CMXCIX••••CMLXXXVII••XXIII•DCCCLXXII",
                new List<(uint, int)>
                {
                    (602, 7), (214, 6), (75, 5), (999, 4), (987, 2), (23, 1), (872, 0)
                }
            };

            yield return new object[]
            {
                "DCII•••••••CCXIV••••••CMXCIX••••CMXCIX•••CMLXXXVII••DCCCLXXII",
                new List<(uint, int)>
                {
                    (602, 7), (214, 6), (999, 4), (999, 3), (987, 2), (872, 0)
                }
            };

            yield return new object[]
            {
                "DCII•••••••CCXIV••••••LXXV•••••CMXCIX••••XXIII•DCCCLXXII",
                new List<(uint, int)>
                {
                    (602, 7), (214, 6), (75, 5), (999, 4), (23, 1), (872, 0)
                }
            };
        }
    }


    [Theory]
    [MemberData(nameof(RomanGroupTestCases))]
    public void RomanNumber_GroupSkipping_IsValidWhenHierarchical(string roman, List<(uint, int)> expected)
    {
        var result = RomanNumber.ExtractRomanGroups(roman);
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> RomanGroupInvalidCases
    {
        get
        {
            // ❌ Separadores al principio
            yield return new object[] { "••DCII••CCXIV" };

            // ❌ Separadores al final
            yield return new object[] { "DCII••CCXIV••" };
            
            // ❌ Orden incorrecto: potencias suben (VI•XI••• → potencia 1→2→4, error)
            yield return new object[] { "VI•XI•••" };

            // ❌ Potencia se mantiene o vuelve a subir: CMXC•DCX••CD•CM
            yield return new object[] { "CMXC•DCX••CD•CM" }; // potencias: 3→2→1→2 (sube al final) → error
            
            // ❌ Separadores dentro del grupo (rompe letra romana)
            yield return new object[] { "D•CII••CCXIV" };

            // ❌ Potencias iguales repetidas sin salto explícito
            yield return new object[] { "DCII••CCXIV••LXXV••LXXX" }; // potencias: 3,2,1,1 → error: potencia no baja
        }
    }
    [Theory]
    [MemberData(nameof(RomanGroupInvalidCases))]
    public void RomanNumber_InvalidGroupSeparators_ShouldThrowFormatException(string roman)
    {
        Assert.Throws<FormatException>(() => RomanNumber.ExtractRomanGroups(roman));
    }


    [Theory]
    [InlineData("X", "IV", "XIV")]
    [InlineData("X", "X", "XX")]
    public void Suma_De_Romanos(string a, string b, string resultadoEsperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        var resultado = r1 + r2;
        Assert.Equal(resultadoEsperado, resultado.Representation);
    }

    [Theory]
    [InlineData("X", "IV", "VI")]
    [InlineData("X", "X", "")] // 0, si usas "" para representar el cero
    public void Resta_De_Romanos(string a, string b, string resultadoEsperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        var resultado = r1 - r2;
        Assert.Equal(resultadoEsperado, resultado.Representation);
    }

    [Theory]
    [InlineData("X", "IV", "XL")]
    [InlineData("X", "X", "C")]
    public void Multiplicacion_De_Romanos(string a, string b, string resultadoEsperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        var resultado = r1 * r2;
        Assert.Equal(resultadoEsperado, resultado.Representation);
    }

    [Theory]
    [InlineData("X", "IV", "II")]
    [InlineData("X", "X", "I")]
    public void Division_Entera_De_Romanos(string a, string b, string resultadoEsperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        var resultado = r1 / r2;
        Assert.Equal(resultadoEsperado, resultado.Representation);
    }

    [Theory]
    [InlineData("X", "IV", "II")]
    [InlineData("X", "X", "")]
    public void Modulo_De_Romanos(string a, string b, string resultadoEsperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        var resultado = r1 % r2;
        Assert.Equal(resultadoEsperado, resultado.Representation);
    }

    [Theory]
    [InlineData("IV", "X")] // 4 - 10 no permitido
    public void Resta_Negativa_Ya_no_Lanza_Excepcion(string a, string b)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(new RomanNumber(-6), r1 - r2);
    }

    [Theory]
    [InlineData("X", "")] // división por cero
    public void Division_Por_Cero_Lanza_Excepcion(string a, string b)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Throws<DivideByZeroException>(() => _ = r1 / r2);
    }

    // Tests adicionales específicos para BigInteger
    [Fact]
    public void RomanNumber_VeryLargeNumber_Creation()
    {
        var veryLargeNumber = BigInteger.Parse("1123456789012345678901234567890");
        var roman = new RomanNumber(veryLargeNumber);
        Assert.NotNull(roman);
        Assert.Equal(veryLargeNumber, roman.AbsValue);
        Assert.Equal("MCXXIII•••••••••CDLVI••••••••DCCLXXXIX•••••••XII••••••CCCXLV•••••DCLXXVIII••••CMI•••CCXXXIV••DLXVII•DCCCXC", roman.Representation);
    }

    [Fact]
    public void RomanNumber_BigInteger_Zero_Representation()
    {
        var zero = new RomanNumber(BigInteger.Zero);
        Assert.Equal("", zero.Representation);
    }

    [Fact]
    public void RomanNumber_BigInteger_Arithmetic_Operations()
    {
        var big1 = new RomanNumber(BigInteger.Parse("1000000"));
        var big2 = new RomanNumber(BigInteger.Parse("500000"));
        
        var sum = big1 + big2;
        Assert.Equal(BigInteger.Parse("1500000"), sum.AbsValue);
        
        var diff = big1 - big2;
        Assert.Equal(BigInteger.Parse("500000"), diff.AbsValue);
        
        var product = big2 * new RomanNumber(BigInteger.Parse("2"));
        Assert.Equal(BigInteger.Parse("1000000"), product.AbsValue);
    }

}

public class RomanNumberComparisonTests
{
    [Theory]
    [InlineData("X", "X", true)]
    [InlineData("X", "IX", false)]
    public void Igualdad_De_Numeros_Romanos(string a, string b, bool esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(esperado, r1 == r2);
        Assert.Equal(!esperado, r1 != r2);
    }

    [Theory]
    [InlineData("IX", "X", true)]
    [InlineData("X", "X", false)]
    public void Menor_Que(string a, string b, bool esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(esperado, r1 < r2);
    }

    [Theory]
    [InlineData("X", "IX", true)]
    [InlineData("X", "X", false)]
    public void Mayor_Que(string a, string b, bool esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(esperado, r1 > r2);
    }

    [Theory]
    [InlineData("X", "X", true)]
    [InlineData("IX", "X", true)]
    public void Menor_O_Igual_Que(string a, string b, bool esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(esperado, r1 <= r2);
    }

    [Theory]
    [InlineData("X", "X", true)]
    [InlineData("X", "IX", true)]
    public void Mayor_O_Igual_Que(string a, string b, bool esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(esperado, r1 >= r2);
    }

    [Theory]
    [InlineData("X", "X", 0)]
    [InlineData("X", "IX", 1)]
    public void Comparacion_Con_CompareTo(string a, string b, int esperado)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Equal(Math.Sign(esperado), Math.Sign(r1.CompareTo(r2)));
    }

    // Tests adicionales de comparación con BigInteger
    [Fact]
    public void Comparacion_BigInteger_Numeros_Grandes()
    {
        var big1 = new RomanNumber(BigInteger.Parse("999999999999"));
        var big2 = new RomanNumber(BigInteger.Parse("1000000000000"));
        
        Assert.True(big1 < big2);
        Assert.True(big2 > big1);
        Assert.False(big1 == big2);
        Assert.True(big1 != big2);
    }

}

public class RomanNumberNegativeTests
{
    [Theory]
    [InlineData(0, "")]
    [InlineData(-1, "-I")]
    [InlineData(-4, "-IV")]
    [InlineData(-9, "-IX")]
    [InlineData(-44, "-XLIV")]
    [InlineData(-99, "-XCIX")]
    [InlineData(-400, "-CD")]
    [InlineData(-944, "-CMXLIV")]
    [InlineData(-1000, "-M")]
    [InlineData(-3999, "-MMMCMXCIX")]
    [InlineData(-10000, "-X•")]
    [InlineData(-4567890, "-IV••DLXVII•DCCCXC")]
    [InlineData(-3567890, "-MMMDLXVII•DCCCXC")]
    [InlineData(-10000000, "-X••")]
    public void RomanNumber_ShouldHaveCorrectNegativeRepresentation(long value, string expected)
    {
        var roman = new RomanNumber(new BigInteger(value));
        Assert.Equal(expected, roman.Representation);
    }

    [Theory]
    [InlineData("-I", 1, true)]
    [InlineData("-IV", 4, true)]
    [InlineData("-MMMCMXCIX", 3999, true)]
    [InlineData("-X•", 10000, true)]
    [InlineData("-IV••DLXVII•DCCCXC", 4567890, true)]
    [InlineData("-MMMDLXVII•DCCCXC", 3567890, true)]
    [InlineData("-X••", 10000000, true)]
    public void RomanNumber_ShouldParseNegativeRepresentationCorrectly(string representation, long value_expected, bool negative_expected)
    {
        var roman = new RomanNumber(representation);
        Assert.Equal(new BigInteger(value_expected), roman.AbsValue);
        Assert.Equal(negative_expected, roman.Negative);
    }

    [Fact]
    public void Zero_ShouldHaveEmptyRepresentation()
    {
        var roman = new RomanNumber(BigInteger.Zero);
        Assert.Equal("", roman.Representation);
    }

    [Fact]
    public void NegativeZero_ShouldAlsoBeEmpty()
    {
        var roman = new RomanNumber(BigInteger.Zero * -1);
        Assert.Equal("", roman.Representation);
    }
}
