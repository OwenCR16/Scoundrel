namespace Scoundrel
{
    public class Card
    {
        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }
        public int CardValue { get; set; }
        public Card(Rank rank, Suit suit)
        {
            CardRank = rank;
            CardSuit = suit;
            CardValue = DetermineCardValue();
        }
        public void WriteCard()
        {
            Console.Write($"{CardRank} of {CardSuit}");
        }
        public int DetermineCardValue()
        {
            switch (CardRank)
            {
                case Rank.Two:
                    return 2;
                case Rank.Three:
                    return 3;
                case Rank.Four:
                    return 4;
                case Rank.Five:
                    return 5;
                case Rank.Six:
                    return 6;
                case Rank.Seven:
                    return 7;
                case Rank.Eight:
                    return 8;
                case Rank.Nine:
                    return 9;
                case Rank.Ten:
                    return 10;
                case Rank.Jack:
                    return 11;
                case Rank.Queen:
                    return 12;
                case Rank.King:
                    return 13;
                case Rank.Ace:
                    return 14;
                default:
                    return 0;
            }
        }
    }
}