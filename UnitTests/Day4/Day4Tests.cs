using AdventOfCode.Day4;

namespace UnitTests;

public class Day4Tests
{
    [TestCase("Card   1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 1, new int[] { 41, 48, 83, 86, 17 }, new int[] { 83, 86, 6, 31, 17, 9, 48, 53 })]
    public void GetWinningNumbersAndCardNumbers(string cardLine, int cardID, int[] winningNumbers, int[] cardNumbers)
    {
        Card card = new Card(cardLine);

        Assert.Multiple(() =>
        {
            Assert.That(card.ID, Is.EqualTo(cardID));
            Assert.That(card.WinningNumbers, Is.EqualTo(winningNumbers));
            Assert.That(card.CardNumbers, Is.EqualTo(cardNumbers));
        });
    }

    [TestCase("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53", 8)]
    public void GetPointValueOfCard(string cardLine, int expectedPointValue)
    {
        Card card = new Card(cardLine);
        int pointValue = card.GetPointValue();

        Assert.That(pointValue, Is.EqualTo(expectedPointValue));
    }

    [TestCase(13)]
    public void GetSumOfPoints(int expectedSum)
    {
        // Specified in .csproj to include and copy to bin folder where the test is executed
        string currentDirectory = TestContext.CurrentContext.TestDirectory;
        string filePath = Path.Combine(currentDirectory, "Day4", "input.txt");

        string[] lines = File.ReadAllLines(filePath);
        int sum = Day4Parser.GetSumOfPoints(lines);

        Assert.That(sum, Is.EqualTo(expectedSum));
    }
}