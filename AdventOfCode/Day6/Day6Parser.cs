namespace AdventOfCode.Day6;

public static class Day6Parser
{
    public static long FindWaysToWinFromLongRace(string[] lines)
    {
        long time = 0;
        long distance = 0;

        foreach (string line in lines)
        {
            if (line.Contains("Time:"))
                time = ParseNumbersTogether(line);

            if (line.Contains("Distance:"))
                distance = ParseNumbersTogether(line);
        }

        return FindNumberOfWaysToWin(time, distance);
    }

    public static long FindProductOfWaysToWin(string[] lines)
    {
        List<int> times = [];
        List<int> distances = [];

        foreach (string line in lines)
        {
            if (line.Contains("Time:"))
                times = ParseNumbersSeperately(line);

            if (line.Contains("Distance:"))
                distances = ParseNumbersSeperately(line);
        }

        long productOfWaysToWin = 1;
        for (int i = 0; i < times.Count; i++)
        {
            int time = times[i];
            int distance = distances[i];

            productOfWaysToWin *= FindNumberOfWaysToWin(time, distance);
        }

        return productOfWaysToWin;
    }

    public static long FindNumberOfWaysToWin(long time, long distance)
    {
        long waysToWin = 0;

        for (long i = 0; i <= time; i++)
        {
            long timeHeld = i;
            long speedToMove = timeHeld;
            long timeMoved = time - timeHeld;
            long distanceMoved = timeMoved * speedToMove;

            if (distanceMoved > distance)
                waysToWin++;
        }

        return waysToWin;
    }

    public static long ParseNumbersTogether(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(":", line);
        string numberLine = splitLines[1].Replace(" ", "");

        return long.Parse(numberLine);
    }

    public static List<int> ParseNumbersSeperately(string line)
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