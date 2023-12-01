namespace Project.Core;

public class Day1Parser
{
    public int GetFirstDigit(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return (int)char.GetNumericValue(c);
            }
        }

        throw new Exception("No digits in input string!");
    }
}