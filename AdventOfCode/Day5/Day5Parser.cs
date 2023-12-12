namespace AdventOfCode.Day5;

public static class Day5Parser
{
    public static ulong GetNearestLocation(string[] lines)
    {
        string seedLine = "";

        List<Map> maps = new List<Map>();

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

            if (line.Contains("seeds:"))
            {
                seedLine = line;
                continue;
            }

            if (line.Contains("map:"))
            {
                maps.Add(new Map());
                continue;
            }

            Map currentMap = maps.Last();
            currentMap.ParseAndAddRange(line);
        }

        return BruteForceNearestLocationOfSeed(seedLine, maps);
    }

    public static ulong GetLocationOfSeed(ulong seed, List<Map> maps)
    {
        ulong mappedValue = seed;
        foreach (Map map in maps)
        {
            mappedValue = map.SourceToDestination(mappedValue);
        }

        return mappedValue;
    }

    public static ulong BruteForceNearestLocationOfSeed(string line, List<Map> maps)
    {
        // Console.WriteLine("Brute Force...");

        string[] splitLines = Utilities.SplitAndTrim(":", line);
        string[] splitNumbers = Utilities.SplitAndTrim(" ", splitLines[1]);

        List<ulong> parsedNumbers = [];
        foreach (string number in splitNumbers)
        {
            parsedNumbers.Add(ulong.Parse(number));
        }

        ulong currentStartingRange = 0;
        ulong nearestLocation = ulong.MaxValue;
        for (int i = 0; i < parsedNumbers.Count; i++)
        {
            ulong currentNumber = parsedNumbers[i];
            bool isStartOfRange = i % 2 == 0; // even number

            if (isStartOfRange)
            {
                currentStartingRange = currentNumber;
                continue;
            }

            // Console.WriteLine($"{DateTime.Now}: i = {i} / {parsedNumbers.Count}, start: {currentStartingRange}, range: {currentNumber}");

            for (ulong count = 0; count < currentNumber; count++)
            {
                ulong seed = currentStartingRange + count;

                // if (count % 100000 == 0)
                // Console.WriteLine($"{DateTime.Now}: i = {i} / {parsedNumbers.Count}, progress: {100 * count / currentNumber}%, seed: {seed}");

                ulong location = GetLocationOfSeed(seed, maps);

                if (location < nearestLocation)
                    nearestLocation = location;
            }
        }

        return nearestLocation;
    }
}

public class Map
{
    private List<Range> _ranges = new List<Range>();

    public void ParseAndAddRange(string line)
    {
        Range range = new Range(line);
        _ranges.Add(range);
    }

    public ulong SourceToDestination(ulong source)
    {
        foreach (Range range in _ranges)
        {
            if (range.IsSourceWithinRange(source))
            {
                return range.SourceToDestination(source);
            }
        }

        // Fallback is return the same
        return source;
    }
}

public class Range
{
    private ulong _destinationRangeStart;
    private ulong _sourceRangeStart;
    private ulong _rangeLength;

    public Range(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(" ", line);

        _destinationRangeStart = ulong.Parse(splitLines[0]);
        _sourceRangeStart = ulong.Parse(splitLines[1]);
        _rangeLength = ulong.Parse(splitLines[2]);
    }

    public bool IsSourceWithinRange(ulong source)
    {
        if (source < _sourceRangeStart)
            return false;

        // -1 to compensate, eg. 98 with range 2 includes 98, 99 only
        if (source > _sourceRangeStart + _rangeLength - 1)
            return false;

        return true;
    }

    public ulong SourceToDestination(ulong source)
    {
        ulong difference = source - _sourceRangeStart;
        return _destinationRangeStart + difference;
    }
}