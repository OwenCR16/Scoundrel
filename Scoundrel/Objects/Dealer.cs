namespace Scoundrel
{
    public class Dealer
    {
        public Hand DealerHand { get; set; } = new Hand();
        public void RevealCard()
        {
            Console.WriteLine($"Dealer reveals a {DealerHand.CardsInHand[1].CardRank} of {DealerHand.CardsInHand[1].CardSuit}!\n");
            //add options for insurance later
        }
    }
}