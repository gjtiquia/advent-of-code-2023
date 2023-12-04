namespace AdventOfCode.Day4;

public static class Day4Parser
{
    public static int GetSumOfPoints(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            Card card = new Card(line);
            sum += card.GetPointValue();
        }
        return sum;
    }
}