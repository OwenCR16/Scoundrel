namespace Scoundrel
{
    public class ScoundrelHand : Hand
    {
        public int MaxHandSize { get; set; } = 4;
        public void DrawToHandSize(List<Card> deckList)
        {
            if (HandList.Count < MaxHandSize)
                DrawCards(deckList, MaxHandSize - HandList.Count);
        }
    }
}