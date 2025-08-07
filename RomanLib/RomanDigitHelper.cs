namespace RomanLib;

public static class RomanDigitHelper
{
    public static readonly List<RomanDigitInfo> Descending = 
        Enum.GetValues(typeof(RomanDigit))
            .Cast<RomanDigit>()
            .Select(d => new RomanDigitInfo(d, (int)d, (int)Math.Floor(Math.Log10((int)d))))
            .OrderByDescending(info => info.Value)
            .ToList();
}