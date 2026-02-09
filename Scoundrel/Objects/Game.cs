namespace Scoundrel
{
    //https://bicyclecards.com/how-to-play/blackjack
    public class Game
    {
        public Player ActivePlayer { get; set; } = new Player();
        public Dealer ActiveDealer { get; set; } = new Dealer();
        public Deck ActiveDeck { get; set; } = new Deck();
        public int BananaPool { get; set; } = 0;
        public void PerformMatch()
        {
            Console.WriteLine("Welcome To BlackJack! Pays 2 to 1 (in Bananas), no max.\nDealer stands on 17 and above.\n\n");
            bool GameActive = true;
            while (GameActive)
            {
                PerformBet();
                switch (PerformGame())
                {
                    case 0://pay back 2 to 1
                        ActivePlayer.Bananas += 2 * BananaPool;
                        BananaPool = 0;
                        break;
                    case 1://banana pool empty, none payed back. Then if player has 0 bananas, game ends
                        BananaPool = 0;
                        if (ActivePlayer.Bananas <= 0)
                        {
                            Console.WriteLine("You have run out of Bananas. Game over.");
                            if (Restart())
                            {
                                ActivePlayer.Bananas = 10;
                                break;
                            }
                            else
                                return;
                        }
                        break;

                    case 2://all bananas in pool go back to player
                        ActivePlayer.Bananas += BananaPool;
                        BananaPool = 0;
                        break;
                }
            }
        }
        public void PerformBet()
        {
            while (true)
            {
                Console.WriteLine($"You have {ActivePlayer.Bananas} Bananas.");
                Console.WriteLine("How many Bananas Would you like to bet?\n");
                string? response = Console.ReadLine();
                if (!string.IsNullOrEmpty(response) && int.TryParse(response, out int responseInt))
                {
                    if (responseInt <= ActivePlayer.Bananas && responseInt >= 0)
                    {
                        BananaPool += responseInt;
                        ActivePlayer.Bananas -= responseInt;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("You have bet an invalid number of Bananas.\nPlease only bet a number more than 0 and not more than the number of Bananas you own.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid response. Please enter in numeric symbols.\n");
                }
            }
        }
        public int PerformGame()
        {
            ResetGame();
            ActiveDeck.ShuffleDeck();
            DealToStart();
            ActivePlayer.PlayerHand.DisplayHand();
            ActiveDealer.RevealCard();
            if (CheckNatural(ActivePlayer.PlayerHand))
                return OnWin();

            while (HitOrStand())
            {
                ActivePlayer.PlayerHand.DrawCards(ActiveDeck.DeckList, 1);
                ActivePlayer.PlayerHand.DisplayHand();
                if (CheckBust(ActivePlayer.PlayerHand))
                    return OnLose();
                if (ActivePlayer.PlayerHand.CardsInHand.Count == 5)
                {
                    Console.WriteLine("5 card charlie!");
                    return OnWin();
                }
            }

            Console.Write("Dealer has: ");
            ActiveDealer.DealerHand.DisplayHand();
            if (CheckNatural(ActiveDealer.DealerHand))
                return OnLose();

            while (ActiveDealer.DealerHand.HandValueTotal < 17)
            {
                Console.WriteLine("Dealer Hits...");
                ActiveDealer.DealerHand.DrawCards(ActiveDeck.DeckList, 1);
                ActiveDealer.DealerHand.DisplayHand();
                if (CheckBust(ActiveDealer.DealerHand))
                    return OnWin();
                if (ActiveDealer.DealerHand.CardsInHand.Count == 5)
                {
                    Console.WriteLine("5 card charlie!");
                    return OnLose();
                }
            }

            if (ActivePlayer.PlayerHand.HandValueTotal > ActiveDealer.DealerHand.HandValueTotal)
                return OnWin();
            else if (ActivePlayer.PlayerHand.HandValueTotal < ActiveDealer.DealerHand.HandValueTotal)
                return OnLose();
            else
                return OnPush();
        }
        public void DealToStart()
        {
            ActivePlayer.PlayerHand.DrawCards(ActiveDeck.DeckList, 2);
            ActiveDealer.DealerHand.DrawCards(ActiveDeck.DeckList, 2);
        }
        public bool HitOrStand()
        {
            while (true)
            {
                Console.WriteLine("Hit or stand? Type 1 for hit, type 2 for stand\n");
                string? response = Console.ReadLine();
                if (!string.IsNullOrEmpty(response))
                {
                    switch (response)
                    {
                        case "1":
                            return true;
                        case "2":
                            return false;
                        default:
                            Console.WriteLine("Invalid response.");
                            break;
                    }
                }
            }
        }
        public bool CheckBust(Hand hand)
        {
            if (hand.HandValueTotal > 21)
                return true;
            return false;
        }
        public bool CheckNatural(Hand hand)
        {
            if (hand.CardsInHand.Count == 2 && hand.HandValueTotal == 21)
                return true;
            return false;
        }
        public int OnWin()
        {
            Console.WriteLine("You Won!\n");
            return 0;
        }
        public int OnLose()
        {
            Console.WriteLine("You Lost!\n");
            return 1;
        }
        public int OnPush()
        {
            Console.WriteLine("Push!\n");
            return 2;
        }
        public bool Restart()
        {
            while (true)
            {
                Console.WriteLine("Do you want to restart? Enter Y/N\n");
                string? response = Console.ReadLine();
                if (!string.IsNullOrEmpty(response))
                {
                    response.ToLower();
                    switch (response)
                    {
                        case "y":
                            return true;
                        case "n":
                            return false;
                        default:
                            Console.WriteLine("Invalid response.");
                            break;
                    }
                }
            }
        }
        public void ResetGame()
        {
            ActivePlayer.PlayerHand = new Hand();
            ActiveDealer.DealerHand = new Hand();
            ActiveDeck = new Deck();
        }
    }
}