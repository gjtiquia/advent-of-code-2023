namespace UnitTests;

public class Day2Tests
{
    [TestCase("Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "Game 1", "3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green")]
    public void ShouldSplitByColon(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitByColon(line);
        AssertThatLinesAreEqual(splitLines, lines);
    }

    [TestCase("Game 1", 1)]
    public void ShouldGetGameID(string line, int targetID)
    {
        int id = Day2Parser.GetGameID(line);
        Assert.That(id, Is.EqualTo(targetID));
    }

    [TestCase("3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "3 blue, 4 red", "1 red, 2 green, 6 blue", "2 green")]
    public void ShouldSplitBySemiColon(string line, params string[] lines)
    {
        string[] splitLines = Day2Parser.SplitBySemiColon(line);
        AssertThatLinesAreEqual(splitLines, lines);
    }

    private void AssertThatLinesAreEqual(string[] lines, string[] targetLines)
    {
        Assert.That(lines.Length, Is.EqualTo(targetLines.Length));
        for (int i = 0; i < lines.Length; i++)
        {
            Assert.That(lines[i], Is.EqualTo(targetLines[i]));
        }
    }
}

public class Day2Parser
{
    public static string[] SplitByColon(string line)
    {
        return line.Split(":").Select(x => x.Trim()).ToArray();
    }

    public static int GetGameID(string line)
    {
        string[] splitLines = line.Split(" ");
        return int.Parse(splitLines[1]);
    }

    public static string[] SplitBySemiColon(string line)
    {
        return line.Split(";").Select(x => x.Trim()).ToArray();
    }
}