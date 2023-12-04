namespace UnitTests;

public static class TestUtilities
{
    public static void AssertDictionaryAreEqual<K, V>(Dictionary<K, V> dictionary, Dictionary<K, V> targetDictionary) where K : notnull where V : notnull
    {
        // Assert.That(dictionary.Count, Is.EqualTo(targetDictionary.Count));
        if (dictionary.Count != targetDictionary.Count)
            throw new AssertionException($"Count not equal! Expected: {dictionary.Count}, But was: {targetDictionary.Count}");

        foreach (var (key, expectedValue) in targetDictionary)
        {
            if (!dictionary.ContainsKey(key))
                throw new AssertionException($"Dictionary does not contain key {key}!");

            if (!dictionary[key].Equals(expectedValue))
                throw new AssertionException($"For key {key}, Expected Value: {expectedValue}, But was: {dictionary[key]}");
        }
    }
}