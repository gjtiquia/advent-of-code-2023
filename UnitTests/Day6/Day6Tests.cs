namespace UnitTests;

public class Day6Tests
{
    [Test]
    public void CanParseCorrectly()
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day6", "input.txt");

        string[] lines = File.ReadAllLines(filePath);

        List<int> times = [];
        List<int> distances = [];

        foreach (string line in lines)
        {
            if (line.Contains("Time:"))
                times = Day6Parser.ParseNumbers(line);

            if (line.Contains("Distance:"))
                distances = Day6Parser.ParseNumbers(line);
        }

        Assert.Multiple(() =>
        {
            Assert.That(times.Count, Is.EqualTo(3));
            Assert.That(distances.Count, Is.EqualTo(3));
        });
    }
}

public static class Day6Parser
{
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