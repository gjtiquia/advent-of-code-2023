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

    [TestCase("Game 1", 1)]
    public void ShouldGetGameID(string line, int targetID)
    {
        int id = Day2Parser.GetGameID(line);
        Assert.That(id, Is.EqualTo(targetID));
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
}