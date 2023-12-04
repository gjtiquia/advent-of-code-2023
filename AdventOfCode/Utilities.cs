public static class Utilities
{
    public static string[] SplitAndTrim(string seperator, string line)
    {
        return line.Split(seperator).Select(x => x.Trim()).ToArray();
    }
}