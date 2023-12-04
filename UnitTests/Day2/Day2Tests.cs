namespace UnitTests;

public class Day2Tests
{
    protected static void AssertThatLinesAreEqual(string[] lines, string[] targetLines)
    {
        Assert.That(lines.Length, Is.EqualTo(targetLines.Length));
        for (int i = 0; i < lines.Length; i++)
        {
            Assert.That(lines[i], Is.EqualTo(targetLines[i]));
        }
    }
}