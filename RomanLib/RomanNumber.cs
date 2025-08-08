namespace RomanLib;

using System.Text;
using System.Numerics;
public class RomanNumber
{
    public readonly ulong value;
    public readonly string representation;

    public RomanNumber(ulong value)
    {
        this.value = value;
        representation = RomanNumber.to_roman(value);
    }

    public RomanNumber(string representation)
    {
        this.representation = representation;
        value = RomanNumber.to_arabic(representation);
    }

    private static ulong Pow(ulong baseValue, int exponent)
    {
        ulong result = 1;
        for (int i = 0; i < exponent; i++)
        {
            checked { result *= baseValue; }
        }
        return result;
    }
    
    private static List<uint> getGroups(ulong value)
    {
        var grupos = new List<uint>();
        while (value > 0)
        {
            grupos.Add((uint)(value % 1000));
            value /= 1000;
        }

        if (grupos[grupos.Count() - 1] < 4 && grupos.Count() > 1)
        {
            grupos[grupos.Count() - 2] += grupos[grupos.Count() - 1] * 1000;
            grupos.RemoveAt(grupos.Count() - 1);
        }
        grupos.Reverse();
        return grupos;
    }
    
    private static String to_roman(ulong value)
    {
        var grupos =  getGroups(value);
        var sb = new StringBuilder();

        foreach (var (group, i) in grupos.Select((value, i) => (value, grupos.Count - 1 - i)))
        {
            var roman = to_roman_less_4000(group);
            var points = new string('•', i);
            if (roman != "")
            {
                sb.Append(roman);
                sb.Append(points);
            }
        }    
        return sb.ToString();
    }
    
    private static String to_roman_less_4000(uint value)
    {
        var sb = new StringBuilder();   
        while (value > 0)
        {
            foreach (RomanDigitInfo info in RomanDigitHelper.Descending)
            {
                if (value >= info.Value)
                {
                    sb.Append(info.Digit.ToString());
                    value -= info.Value;
                    break;
                }
            }
        
        }
        return sb.ToString();
    }

    private static ulong to_arabic(string representation)
    {
        if (representation is null)
            throw new ArgumentNullException(nameof(representation));
        
        var groups = RomanNumber.ExtractRomanGroups(representation);
        ulong result = 0;
        foreach (var (value, order) in groups)
        {
            result += value * RomanNumber.Pow(1000, order);
        }

        return result;
    }
    private static uint to_arabic_less_4000(string representation)
    {
        uint result = 0;
        int lastOrder = 4;
        
        foreach (RomanDigitInfo info in RomanDigitHelper.Descending)
        {
            if (representation.StartsWith(info.Digit.ToString()) && info.Order < lastOrder)
            {
                result += info.Value;
                representation = representation.Substring(info.Digit.ToString().Length);
                lastOrder = info.Order;
                if (representation.Length == 0) break;
            }
        }
        if (representation.Length > 0)
            throw new FormatException();
        return result;
    }

    private static int updateResult(List<(uint, int)> result, int interval = 1)
    {
        var (value, order) = result[result.Count() - 1];
        order += interval;
        result[result.Count() - 1] = (value, order);
        return order;
    }
    
    public static List<(uint, int)> ExtractRomanGroups(string roman)
    {
        var grupos = roman.Split('•');
        var result = new List<(uint, int)>();
        uint value = 0;
        int order = 0;
        int lastorder = -1;
        foreach (var group in grupos)
        {
            if (group.Length != 0)
            {
                if (result.Count() <= 1)
                {
                    lastorder = order + 1;
                }
                else if (result.Count() > 1)
                {
                    lastorder = result[result.Count() - 2].Item2;
                }

                if (order >= lastorder)
                {
                    throw new FormatException();
                }

                value = RomanNumber.to_arabic_less_4000(group);
                order = 1;
                result.Add((value, order));
            }
            else if (result.Count() > 0)
            {
                order = RomanNumber.updateResult(result);
            }
            else
            {
                result.Add((0, 1));
                order = 1;
            }
        }

        order = RomanNumber.updateResult(result, -1);
        
        if (result.Count() > 1 && result[result.Count() - 2].Item2 <= order)
        {
            throw new FormatException();
        }

        return result;
    }
}