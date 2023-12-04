namespace UnitTests;

public class Day3Day2Tests
{
    [TestCase("467..114..", new int[] { })]
    [TestCase("...*......", new int[] { 3 })]
    [TestCase("...$.*....", new int[] { 5 })]
    public void ShouldFindTheIndexesOfGear(string line, int[] targetIndexes)
    {
        int[] indexes = Day3Parser.FindIndexesOfGear(line);
        Assert.That(indexes, Is.EqualTo(targetIndexes));
    }
}