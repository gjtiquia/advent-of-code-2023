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
    public void ShouldDetermineTheTypeCorrectly(string line, EHandType expectedType)
    {
        Hand hand = new Hand(line);
        Assert.That(hand.GetHandType(), Is.EqualTo(expectedType));
    }
}

public class Hand
{
    private string _hand;

    public Hand(string hand)
    {
        _hand = hand;
    }

    public EHandType GetHandType()
    {
        Dictionary<char, int> characterOccurences = [];

        foreach (char a in _hand)
        {
            if (characterOccurences.ContainsKey(a))
                continue;

            int occurences = 0;

            foreach (char b in _hand)
            {
                if (a == b)
                    occurences++;
            }

            characterOccurences.Add(a, occurences);
        }

        if (characterOccurences.Count == 1 && characterOccurences.First().Value == 5)
            return EHandType.FiveOfAKind;

        if (characterOccurences.Count == 2)
        {
            foreach (var (_, occurences) in characterOccurences)
            {
                if (occurences == 4)
                    return EHandType.FourOfAKind;

                if (occurences == 3)
                    return EHandType.FullHouse;
            }
        }

        if (characterOccurences.Count == 3)
        {
            foreach (var (_, occurences) in characterOccurences)
            {
                if (occurences == 3)
                    return EHandType.ThreeOfAKind;
            }
        }

        int numberOfPairs = 0;
        foreach (var (_, occurences) in characterOccurences)
        {
            if (occurences == 2)
                numberOfPairs++;
        }

        if (numberOfPairs == 2)
            return EHandType.TwoPair;

        if (numberOfPairs == 1)
            return EHandType.OnePair;

        return EHandType.HighCard;
    }
}

public enum EHandType
{
    FiveOfAKind,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}