using System.Numerics;

namespace RomanLib.Tests;

public class RomanNumberTests
{
    [Fact]
    public void Uno_DeberiaSer_I()
    {
        Assert.Equal("I", new RomanNumber(1).Representation);
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

    public void Arabigo_a_romano_menor_10(ulong value, string representation)
    {
        var result = new RomanNumber(value);
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

    public void Romano_a_arabigo_menor_4000(string representation, ulong value)
    {
        var result = new RomanNumber(representation);
        Assert.Equal(value, result.Value);
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

    [Theory]
    [InlineData(4000, "IV•")]
    [InlineData(10001, "X•I")]
    [InlineData(1000000, "M•")]
    [InlineData(1000001, "M•I")]
    [InlineData(1234567, "MCCXXXIV•DLXVII")]
    [InlineData(2000000, "MM•")]
    [InlineData(3000000, "MMM•")]
    [InlineData(4000000, "IV••")]
    [InlineData(5673987, "V••DCLXXIII•CMLXXXVII")]
    [InlineData(5678987, "V••DCLXXVIII•CMLXXXVII")]
    [InlineData(3678987, "MMMDCLXXVIII•CMLXXXVII")]
    [InlineData(1000000000, "M••")]
    [InlineData(1000000001, "M••I")]
    [InlineData(2147483647, "MMCXLVII••CDLXXXIII•DCXLVII")]
    [InlineData(4001000, "IV••I•")]
    [InlineData(4000001, "IV••I")]
    [InlineData(4000000000, "IV•••")] // solo si se permitieran long; si no, ignóralo
    [InlineData(18446744073709551615, "XVIII••••••CDXLVI•••••DCCXLIV••••LXXIII•••DCCIX••DLI•DCXV")]
    public void RomanNumber_GeneratesExtendedFormat(ulong value, string expectedRoman)
    {
        var roman = new RomanNumber(value);
        Assert.Equal(expectedRoman, roman.Representation);
    }

    [Theory]
    [InlineData("IV•", 4000)]
    [InlineData("X•I", 10001)]
    [InlineData("M•", 1000000)]
    [InlineData("M•I", 1000001)]
    [InlineData("MCCXXXIV•DLXVII", 1234567)]
    [InlineData("MM•", 2000000)]
    [InlineData("MMM•", 3000000)]
    [InlineData("IV••", 4000000)]
    [InlineData("V••DCLXXIII•CMLXXXVII", 5673987)]
    [InlineData("V••DCLXXVIII•CMLXXXVII", 5678987)]
    [InlineData("MMMDCLXXVIII•CMLXXXVII", 3678987)]
    [InlineData("M••", 1000000000)]
    [InlineData("M••I", 1000000001)]
    [InlineData("MMCXLVII••CDLXXXIII•DCXLVII", 2147483647)]
    [InlineData("IV••I•", 4001000)]
    [InlineData("IV••I", 4000001)]
    public void RomanNumber_ParseExtendedRoman(string roman, ulong expected)
    {
        var number = new RomanNumber(roman);
        Assert.Equal(expected, number.Value);
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
    [InlineData("X", "X", "")] // 0, si usas "N" para representar el cero
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
    public void Resta_Negativa_Lanza_Excepcion(string a, string b)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Throws<ArgumentException>(() => _ = r1 - r2);
    }

    [Theory]
    [InlineData("X", "")] // división por cero
    public void Division_Por_Cero_Lanza_Excepcion(string a, string b)
    {
        var r1 = new RomanNumber(a);
        var r2 = new RomanNumber(b);
        Assert.Throws<DivideByZeroException>(() => _ = r1 / r2);
    }
    

}
