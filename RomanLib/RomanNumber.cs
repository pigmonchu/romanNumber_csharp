namespace RomanLib;

using System.Text;
public class RomanNumber
{
    private readonly int _value;
    public readonly string _representation;

    public RomanNumber(int value)
    {
        _value = value;
        _representation = to_roman(value);
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
}