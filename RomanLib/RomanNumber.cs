namespace RomanLib;

using System.Text;
public class RomanNumber
{
    public readonly int value;
    public readonly string representation;

    public RomanNumber(int value)
    {
        this.value = value;
        representation = to_roman(value);
    }

    public RomanNumber(string representation)
    {
        this.representation = representation;
        value = to_arabic(representation);
    }

    private String to_roman(int value)
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

    private int to_arabic(string representation)
    {
        if (representation is null)
            throw new ArgumentNullException(nameof(representation));
        
        int result = 0;
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
    
    
}