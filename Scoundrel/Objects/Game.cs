namespace Scoundrel
{
    //https://bicyclecards.com/how-to-play/blackjack
    public class Game
    {
        public Player ActivePlayer { get; set; } = new Player();
        public ScoundrelDeck ActiveDeck { get; set; } = new ScoundrelDeck();
        public void PerformMatch()
        {
            Console.WriteLine("Welcome To BlackJack! Pays 2 to 1 (in Bananas), no max.\nDealer stands on 17 and above.\n\n");
            bool GameActive = true;
            while (GameActive)
            {
                switch (PerformGame())
                {
                    case 0://win - open shop, then make the game harder and increase level
                    case 1://lose - open shop, then restart level
                }
            }
        }
                //string? response = Console.ReadLine();
                //if (!string.IsNullOrEmpty(response) && int.TryParse(response, out int responseInt))
        public int PerformGame()
        {
            ResetGame();
            ActiveDeck.ShuffleDeck();
            while (true)
            {
                if (ActiveDeck.DeckList.Count == 0)
                    //return OnWin();
                NewRound();
                ActivePlayer.PlayerHand.DisplayHand();
                
                //return OnLose(); 
            }
            
            
        }
        public void NewRound()
        {
            ActivePlayer.PlayerHand.DrawToHandSize(ActiveDeck.DeckList);
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
            ActivePlayer.PlayerHand = new ScoundrelHand();
            ActiveDeck = new ScoundrelDeck();
        }
    }
}