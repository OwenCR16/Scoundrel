namespace Scoundrel
{
    public class ScoundrelDeck : Deck
    {
        public List<Card> CreateScoundrelDeckList()
        {
            List<Card> deckList = [];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!((j == 1 || j == 2) && i > 8)) //no red face cards
                        deckList.Add(new Card((Rank)i, (Suit)j));
                }
            }
            return deckList;
        }
    }
}