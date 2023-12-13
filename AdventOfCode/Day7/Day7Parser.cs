namespace AdventOfCode.Day7;

public static class Day7Parser
{
    public static int CalculateWinnings(string[] lines)
    {
        SortedDictionary<Hand, int> sortedHandsAndBids = [];

        foreach (string line in lines)
        {
            string[] splitLines = Utilities.SplitAndTrim(" ", line);

            Hand hand = new Hand(splitLines[0]);
            int bid = int.Parse(splitLines[1]);

            sortedHandsAndBids.Add(hand, bid);
        }

        int totalWinnings = 0;

        int currentRank = 0;
        foreach (var (_, bid) in sortedHandsAndBids)
        {
            currentRank++;

            totalWinnings += currentRank * bid;
        }

        return totalWinnings;
    }
}

public class Hand : IComparable<Hand>
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

    public int CompareTo(Hand? obj)
    {
        if (obj == null || obj is not Hand)
            return 1;

        Hand otherHand = (Hand)obj;

        EHandType thisHandType = this.GetHandType();
        EHandType otherHandType = otherHand.GetHandType();

        if ((int)thisHandType < (int)otherHandType)
            return 1;
        else if ((int)thisHandType > (int)otherHandType)
            return -1;

        for (int i = 0; i < _hand.Length; i++)
        {
            char thisChar = _hand[i];
            char otherChar = otherHand._hand[i];

            int thisRanking = GetCharRanking(thisChar);
            int otherRanking = GetCharRanking(otherChar);

            if (thisRanking == otherRanking)
                continue;

            if (thisRanking > otherRanking)
                return 1;

            return -1;
        }

        return 0;
    }


    private int GetCharRanking(char c)
    {
        string charString = c.ToString();

        switch (charString)
        {
            case "A": return 14;
            case "K": return 13;
            case "Q": return 12;
            case "J": return 11;
            case "T": return 10;
        }

        // 9 to 2 returns the inteer value
        if (int.TryParse(charString, out int result))
            return result;

        return 0;
    }
}

public enum EHandType
{
    FiveOfAKind = 0,
    FourOfAKind,
    FullHouse,
    ThreeOfAKind,
    TwoPair,
    OnePair,
    HighCard
}