namespace UnitTests;

public class Day2Tests
{
    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "Game 1", "3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
    public void ShouldSplitByColon(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitByColon(line);

        Assert.That(splitLines.Length, Is.EqualTo(lines.Length));
        for (int i = 0; i < splitLines.Length; i++)
        {
            Assert.That(splitLines[i], Is.EqualTo(lines[i]));
        }
    }
}

public class Day2Parser
{
    public static string[] SplitByColon(string line)
    {
        return line.Split(":").Select(x => x.Trim()).ToArray();
    }
}