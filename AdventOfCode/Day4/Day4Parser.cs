namespace AdventOfCode.Day4;

public static class Day4Parser
{
    public static int GetTotalScratchcards(string[] lines)
    {
        List<Card> cards = new List<Card>();
        Dictionary<int, int> cardCountDictionary = new Dictionary<int, int>();

        foreach (string line in lines)
        {
            Card card = new Card(line);

            cards.Add(card);
            cardCountDictionary.Add(card.ID, 1);
        }

        foreach (Card card in cards)
        {
            int cardCount = cardCountDictionary[card.ID];
            int matchesCount = card.GetMatchingNumberCount();

            for (int i = 0; i < cardCount; i++)
            {
                for (int j = 0; j < matchesCount; j++)
                {
                    int idToAdd = card.ID + j + 1;
                    cardCountDictionary[idToAdd]++;
                }
            }
        }

        int sum = 0;
        foreach (var (id, count) in cardCountDictionary)
        {
            sum += count;
        }

        return sum;
    }

    public static int GetSumOfPoints(string[] lines)
    {
        int sum = 0;
        foreach (string line in lines)
        {
            Card card = new Card(line);
            sum += card.GetPointValue();
        }
        return sum;
    }
}