namespace AdventOfCode.Day5;

public static class Day5Parser
{
    public static double GetNearestLocation(string[] lines)
    {
        List<double> seeds = new List<double>();
        List<Map> maps = new List<Map>();

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) continue;

            if (line.Contains("seeds:"))
            {
                seeds = Day5Parser.ParseLineForSeeds(line);
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

        double nearestLocation = double.MaxValue;

        foreach (double seed in seeds)
        {
            double mappedValue = seed;
            foreach (Map map in maps)
            {
                mappedValue = map.SourceToDestination(mappedValue);
            }

            if (mappedValue < nearestLocation)
                nearestLocation = mappedValue;
        }

        return nearestLocation;
    }

    public static List<double> ParseLineForSeeds(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(":", line);
        string[] splitNumbers = Utilities.SplitAndTrim(" ", splitLines[1]);

        List<double> seeds = [];
        foreach (string number in splitNumbers)
        {
            seeds.Add(double.Parse(number));
        }

        return seeds;
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

    public double SourceToDestination(double source)
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
    private double _destinationRangeStart;
    private double _sourceRangeStart;
    private double _rangeLength;

    public Range(string line)
    {
        string[] splitLines = Utilities.SplitAndTrim(" ", line);

        _destinationRangeStart = double.Parse(splitLines[0]);
        _sourceRangeStart = double.Parse(splitLines[1]);
        _rangeLength = double.Parse(splitLines[2]);
    }

    public bool IsSourceWithinRange(double source)
    {
        if (source < _sourceRangeStart)
            return false;

        // -1 to compensate, eg. 98 with range 2 includes 98, 99 only
        if (source > _sourceRangeStart + _rangeLength - 1)
            return false;

        return true;
    }

    public double SourceToDestination(double source)
    {
        double difference = source - _sourceRangeStart;
        return _destinationRangeStart + difference;
    }
}