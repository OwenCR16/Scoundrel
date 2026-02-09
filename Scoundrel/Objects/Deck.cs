namespace Scoundrel
{
    public class Deck
    {
        public List<Card> DeckList { get; set; }
        public List<Card> DiscardList {get; set; } = [];
        public Deck()
        {
            DeckList = CreateStandardDeckList();
        }
        public List<Card> CreateStandardDeckList()
        {
            List<Card> deckList = [];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    deckList.Add(new Card((Rank)i, (Suit)j));
                }
            }
            return deckList;
        }
        public void ShuffleDeck()
        {
            Random random= new Random();
            DeckList = [.. DeckList.OrderBy(_ => random.Next())];
        }
        public void DisplayDeckList()
        {
            foreach (Card card in DeckList)
            {
                card.WriteCard();
                Console.WriteLine();
            }
        }
        public void PeekDeck(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                DeckList[i].WriteCard();
                Console.WriteLine();
            }
        }
    }
}