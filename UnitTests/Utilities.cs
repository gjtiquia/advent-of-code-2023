namespace UnitTests;

public static class Utilities
{
    public static void AssertDictionaryAreEqual<K, V>(Dictionary<K, V> dictionary, Dictionary<K, V> targetDictionary) where K : notnull
    {
        // Assert.That(dictionary.Count, Is.EqualTo(targetDictionary.Count));
        if (dictionary.Count != targetDictionary.Count)
            throw new AssertionException($"Count not equal! Expected: {dictionary.Count}, But was: {targetDictionary.Count}");

        foreach (var (key, value) in targetDictionary)
        {
            Assert.IsTrue(dictionary.ContainsKey(key));
            Assert.That(dictionary[key], Is.EqualTo(value));
        }
    }
}