using AdventOfCode.Day7;

namespace UnitTests;

public class Day7Tests
{
    [TestCase("AAAAA", EHandType.FiveOfAKind)]
    [TestCase("AA8AA", EHandType.FourOfAKind)]
    [TestCase("23332", EHandType.FullHouse)]
    [TestCase("TTT98", EHandType.ThreeOfAKind)]
    [TestCase("23432", EHandType.TwoPair)]
    [TestCase("A23A4", EHandType.OnePair)]
    [TestCase("23456", EHandType.HighCard)]

    [TestCase("T55J5", EHandType.FourOfAKind)]
    [TestCase("KTJJT", EHandType.FourOfAKind)]
    [TestCase("QJJQ2", EHandType.FourOfAKind)]
    public void ShouldDetermineTheTypeCorrectly(string line, EHandType expectedType)
    {
        Hand hand = new Hand(line);
        Assert.That(hand.GetHandType(), Is.EqualTo(expectedType));
    }

    [TestCase("AAAAA", "AA8AA")]
    [TestCase("33332", "2AAAA")]
    [TestCase("77888", "77788")]
    [TestCase("KTJJT", "T55J5")]
    [TestCase("QQQQ2", "JKKK2")]
    public void ShouldCompareCorrectly(string strongerLine, string weakerLine)
    {
        Hand strongerHand = new Hand(strongerLine);
        Hand weakerHand = new Hand(weakerLine);

        Assert.That(strongerHand.CompareTo(weakerHand), Is.GreaterThan(0));
    }

    [Test]
    [Ignore("TODO")]
    public void ShouldParseAndCalculateWinnings()
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day7", "input.txt");

        string[] lines = File.ReadAllLines(filePath);
        int winnings = Day7Parser.CalculateWinnings(lines);

        Assert.That(winnings, Is.EqualTo(5905));
    }
}