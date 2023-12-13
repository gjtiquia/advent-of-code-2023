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

        // Only 1 kind of card, regardless if wild card or not, FiveOfAKind
        if (characterOccurences.Count == 1 && characterOccurences.First().Value == 5)
            return EHandType.FiveOfAKind;

        // 2 kinds of cards
        if (characterOccurences.Count == 2)
        {
            int numberOfWildCards = 0;
            EHandType potentialHandType = EHandType.HighCard;

            foreach (var (character, occurences) in characterOccurences)
            {
                if (character.ToString() == "J")
                    numberOfWildCards = occurences;

                // AAAAJ
                // JJJJA
                // AAAAB
                if (occurences == 4)
                    potentialHandType = EHandType.FourOfAKind;

                // AAAJJ
                // JJJAA
                // AAABB
                if (occurences == 3)
                    potentialHandType = EHandType.FullHouse;
            }

            if (potentialHandType == EHandType.FourOfAKind)
            {
                // AAAAJ
                // JJJJA
                if (numberOfWildCards > 0)
                    return EHandType.FiveOfAKind;

                // AAAAB
                return EHandType.FourOfAKind;
            }

            if (potentialHandType == EHandType.FullHouse)
            {
                // AAAJJ
                // JJJAA
                if (numberOfWildCards > 0)
                    return EHandType.FiveOfAKind;

                // AAABB
                return EHandType.FullHouse;
            }
        }

        if (characterOccurences.Count == 3)
        {
            int numberOfWildCards = 0;
            EHandType potentialHandType = EHandType.HighCard;

            foreach (var (character, occurences) in characterOccurences)
            {
                if (character.ToString() == "J")
                    numberOfWildCards = occurences;

                // AABBC
                // JJAAC
                // AABBJ
                if (occurences == 2)
                    potentialHandType = EHandType.TwoPair;

                // AAABC
                // JJJAB
                // AAAJB
                if (occurences == 3)
                    potentialHandType = EHandType.ThreeOfAKind;
            }

            if (potentialHandType == EHandType.TwoPair)
            {
                // JJAAC
                if (numberOfWildCards == 2)
                    return EHandType.FourOfAKind;

                // AABBJ
                if (numberOfWildCards == 1)
                    return EHandType.FullHouse;

                // AABBC
                return EHandType.TwoPair;
            }

            if (potentialHandType == EHandType.ThreeOfAKind)
            {
                // JJJAB
                // AAAJB
                if (numberOfWildCards > 0)
                    return EHandType.FourOfAKind;

                // AAABC
                return EHandType.ThreeOfAKind;
            }
        }

        if (characterOccurences.Count == 4)
        {
            int numberOfWildCards = 0;

            foreach (var (character, occurences) in characterOccurences)
            {
                if (character.ToString() == "J")
                    numberOfWildCards = occurences;
            }

            // ABCDD
            // JABCC
            // ABCJJ

            // JABCC
            // ABCJJ
            if (numberOfWildCards > 0)
                return EHandType.ThreeOfAKind;

            // ABCDD
            return EHandType.OnePair;

        }

        if (characterOccurences.Count == 5)
        {
            int numberOfWildCards = 0;

            foreach (var (character, occurences) in characterOccurences)
            {
                if (character.ToString() == "J")
                    numberOfWildCards = occurences;
            }

            // ABCDE
            // ABCDJ

            // ABCDJ
            if (numberOfWildCards == 1)
            {
                return EHandType.OnePair;
            }
        }

        // ABCDE
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

            // Wildcard requirement
            // case "J": return 11;
            case "J": return 1;

            case "T": return 10;
        }

        // 9 to 2 returns the inteer value
        if (int.TryParse(charString, out int result))
            return result;

        throw new InvalidOperationException($"Do not know ranking of character {charString}!");
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