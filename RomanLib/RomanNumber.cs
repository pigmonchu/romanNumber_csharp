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
            foreach (RomanDigit d in RomanDigitHelper.Descending)
            {
                if (value >= (int)d)
                {
                    sb.Append(d.ToString());
                    value -= (int)d;
                    break;
                }
            }
        
        }
        return sb.ToString();
    }

    private int to_arabic(string representation)
    {
        int result = 0;
        while (representation.Length > 0)
        {
            foreach (RomanDigit d in RomanDigitHelper.Descending)
            {
                if (representation.StartsWith(d.ToString()))
                {
                    result += (int)d;
                    representation = representation.Substring(d.ToString().Length);
                }
            }
        }
        return result;
    }
    
    
}