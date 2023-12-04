namespace UnitTests;

public class Day3Tests
{
    [TestCase("467..114..", new int[] { })]
    [TestCase("...*......", new int[] { 3 })]
    [TestCase("...$.*....", new int[] { 3, 5 })]
    public void ShouldFindTheIndexesOfSymbols(string line, int[] targetIndexes)
    {
        int[] indexes = Day3Parser.FindIndexesOfSymbols(line);
        Assert.That(indexes, Is.EqualTo(targetIndexes));
    }


}

public class Day3Parser
{
    public static int[] FindIndexesOfSymbols(string line)
    {
        List<int> indexes = new List<int>();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (!char.IsLetterOrDigit(c) && c != char.Parse("."))
                indexes.Add(i);
        }

        return indexes.ToArray();
    }
}