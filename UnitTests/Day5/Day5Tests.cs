using AdventOfCode.Day5;

namespace UnitTests;

public class Day5Tests
{
    [Test]
    public void ShouldGetNearestLocationFromTestInput()
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day5", "input.txt");

        string[] lines = File.ReadAllLines(filePath);
        int nearestLocation = Day5Parser.GetNearestLocation(lines);

        Assert.That(nearestLocation, Is.EqualTo(35));
    }

    [Test]
    public void ShouldMapToItselfByDefault()
    {
        Map map = new Map();
        int destination = map.SourceToDestination(3);

        Assert.That(destination, Is.EqualTo(3));
    }

    [TestCase("50 98 2")]
    public void ShouldParseAndAddRangeToMapCorrectly(string line)
    {
        Map map = new Map();
        map.ParseAndAddRange(line);

        Assert.That(map.SourceToDestination(3), Is.EqualTo(3));
        Assert.That(map.SourceToDestination(98), Is.EqualTo(50));
        Assert.That(map.SourceToDestination(99), Is.EqualTo(51));
    }
}

