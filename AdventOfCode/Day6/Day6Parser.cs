namespace AdventOfCode.Day6;

public static class Day6Parser
{
    public static int FindProductOfWaysToWin(string[] lines)
    {
        List<int> times = [];
        List<int> distances = [];

        foreach (string line in lines)
        {
            if (line.Contains("Time:"))
                times = Day6Parser.ParseNumbers(line);

            if (line.Contains("Distance:"))
                distances = Day6Parser.ParseNumbers(line);
        }

        int productOfWaysToWin = 1;
        for (int i = 0; i < times.Count; i++)
        {
            int time = times[i];
            int distance = distances[i];

            productOfWaysToWin *= Day6Parser.FindNumberOfWaysToWin(time, distance);
        }

        return productOfWaysToWin;
    }

    public static int FindNumberOfWaysToWin(int time, int distance)
    {
        int waysToWin = 0;

        for (int i = 0; i <= time; i++)
        {
            int timeHeld = i;
            int speedToMove = timeHeld;
            int timeMoved = time - timeHeld;
            int distanceMoved = timeMoved * speedToMove;

            if (distanceMoved > distance)
                waysToWin++;
        }

        return waysToWin;
    }

    public static List<int> ParseNumbers(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(":", line);
        string[] numberLine = Utilities.SplitAndTrim(" ", splitLines[1]);

        List<int> numbers = [];
        foreach (string element in numberLine)
        {
            if (!int.TryParse(element, out int result))
                continue;

            numbers.Add(result);
        }

        return numbers;
    }
}