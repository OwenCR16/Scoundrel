namespace Scoundrel
{
    public class Hand
    {
        public List<Card> HandList { get; set; } = new List<Card>();
        public int HandValueTotal {get; set; } = 0;
        public bool HandContainsAce { get; set; } = false;
        public void DrawCards(List<Card> deckList, int drawAmount)
        {
            int tempDrawAmount = deckList.Count < drawAmount ? deckList.Count : drawAmount;
            List<Card> selected = deckList.Where((item, index) => index < tempDrawAmount).ToList();
            foreach (Card card in selected)
            {
                HandList.Add(new Card(card.CardRank, card.CardSuit));
                deckList.Remove(card);
            }
        }
        public void DiscardCards(int[] discardList, List<Card> discardZone)
        {
            for (int i = discardList.Length - 1; i >= 0; i--)
            {
                discardZone.Add(new Card(HandList[discardList[i]].CardRank, HandList[discardList[i]].CardSuit));
                HandList.RemoveAt(discardList[i]);
            }
        }
        public void HandToDeck(List<Card> deckList, bool insertToBottomTrue)
        {
            foreach (Card card in HandList)
            {
                if (insertToBottomTrue)
                    deckList.Add(new Card(card.CardRank, card.CardSuit));
                else
                    deckList.Insert(0, new Card(card.CardRank, card.CardSuit));
            }
            HandList = [];
        }
        public void DisplayHand()
        {
            int counter = 0;
            foreach (Card card in HandList)
            {
                if (counter > 0)
                    Console.Write(", ");
                card.WriteCard();
                counter++;
            }
            Console.WriteLine();
        }
    }
}