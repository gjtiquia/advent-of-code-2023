namespace UnitTests;

public class Day2Tests
{
    protected void AssertThatLinesAreEqual(string[] lines, string[] targetLines)
    {
        Assert.That(lines.Length, Is.EqualTo(targetLines.Length));
        for (int i = 0; i < lines.Length; i++)
        {
            Assert.That(lines[i], Is.EqualTo(targetLines[i]));
        }
    }

    protected void AssertDictionaryAreEqual<K, V>(Dictionary<K, V> dictionary, Dictionary<K, V> targetDictionary) where K : notnull
    {
        Assert.That(dictionary.Count, Is.EqualTo(targetDictionary.Count));

        foreach (var (key, value) in targetDictionary)
        {
            Assert.IsTrue(dictionary.ContainsKey(key));
            Assert.That(dictionary[key], Is.EqualTo(value));
        }
    }
}