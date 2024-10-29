namespace EffectiveMobileTestTask;

public static class StringExtensions
{
    public static string FirstCharToUpper(this string s)
    {
        return char.ToUpper(s[0]) + s[1..];
    }
}