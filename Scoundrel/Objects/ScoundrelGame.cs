namespace Scoundrel
{
    //https://bicyclecards.com/how-to-play/blackjack
    public class ScoundrelGame
    {
        public Player ActivePlayer { get; set; } = new Player();
        public ScoundrelDeck ActiveDeck { get; set; } = new ScoundrelDeck();
        public int RoundCount { get; set; } = 0;
        public bool RanPreviousRound { get; set; } = false;
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
                        break;
                }
            }
        }
        public int PerformGame()
        {
            ResetGame();
            ActiveDeck.ShuffleDeck();
            while (true)
            {
                if (ActiveDeck.DeckList.Count == 0 && ActivePlayer.PlayerHealth.HealthValue > 0)
                    return OnWin();
                NewRound();
                bool roundOver = false;
                while (!roundOver)
                {
                    ScoundrelAction();
                    if (ActivePlayer.PlayerHealth.HealthValue <= 0)
                        return OnLose();
                    if (ActivePlayer.PlayerHand.HandList.Count <= 1)
                        roundOver = true;
                }
            }
        }
        public void NewRound()
        {
            RoundCount++;
            Console.WriteLine($"Round {RoundCount}.");
            ActivePlayer.PlayerHand.DrawToHandSize(ActiveDeck.DeckList);
            //DISPLAY DECK SIZE
            //other roguelike stuff may occur here
        }
        public void ScoundrelAction()
        {
            bool actionTakenThisRound = false;
            while (true)
            {
                string? response = RequestScoundrelAction(actionTakenThisRound);

                if (CheckRun(response, actionTakenThisRound))
                {
                    Run();
                    return;
                }

                if (!string.IsNullOrEmpty(response) && int.TryParse(response, out int userChoiceIndex))
                {
                    userChoiceIndex -= 1;
                    if (userChoiceIndex > 0 || userChoiceIndex <= ActivePlayer.PlayerHand.HandList.Count - 1)
                    {
                        if (AttemptAction(userChoiceIndex))
                        {
                            actionTakenThisRound = true;
                            return;
                        }
                    }
                }
                Console.WriteLine("--------  invalid input  --------");
            }
        }
        public bool AttemptAction(int userChoiceIndex)
        {
            switch (ActivePlayer.PlayerHand.HandList[userChoiceIndex].CardSuit)
            {
                case Suit.Diamonds:
                    EquipNewWeaponFromHand(userChoiceIndex);
                    return true;
                case Suit.Hearts:
                    DrinkHealthPotionFromHand(userChoiceIndex);
                    return true;
                case Suit.Clubs:
                case Suit.Spades:
                    FightEnemy(userChoiceIndex, FightChoice());
                    return true;
                default:
                    return false;
            }
        }
        public string? RequestScoundrelAction(bool actionTakenThisRound)
        {
            ActivePlayer.DisplayPlayerProperties();
            Console.WriteLine("Please choose your action for the turn by selecting a card from your hand (for console app, type the position of the card e.g. 1,2,3,4...)");
            if (!RanPreviousRound && !actionTakenThisRound)
                Console.WriteLine("Otherwise, run from this round by typing \"r\".");
            return Console.ReadLine();
        }
        public bool CheckRun(string? response, bool actionTakenThisRound)
        {
            if (response == "r" && !RanPreviousRound && !actionTakenThisRound)
                return true;
            return false;
        }
        public void Run()
        {
            //in actual blazor app, player can decide order to put cards back on the bottom. let player drag cards around somehow?? at least elavate them slightly to show they are about to be put in deck, then have left/right buttons below the cards as a starting point?
            ActivePlayer.PlayerHand.HandToDeck(ActiveDeck.DeckList, true);
            RanPreviousRound = true;
        }
        public bool FightChoice()
        {
            if (ActivePlayer.PlayerWeapon == null)
                return false;
            while (true)
            {
                Console.WriteLine("Fight with weapon (1) or face (2)?");
                string? response = Console.ReadLine();
                if (!string.IsNullOrEmpty(response))
                {
                    switch (response.ToLower().Trim())
                    {
                        case "1":
                            return true;
                        case "2":
                            return false;
                        default:
                            break;
                    }
                }
                Console.WriteLine("invalid input");
            }
        }
        public void FightEnemy(int selectedCardIndex, bool withWeaponTrue, int armourValue = 0, int weaponValueModifier = 0)
        {
            int weaponValue = withWeaponTrue && (ActivePlayer.PlayerWeapon != null) ? ActivePlayer.PlayerWeapon.WeaponCard.CardValue : 0;

            weaponValue += weaponValueModifier;

            int damage = (armourValue + weaponValue) > ActivePlayer.PlayerHand.HandList[selectedCardIndex].CardValue ? 0 : ActivePlayer.PlayerHand.HandList[selectedCardIndex].CardValue - armourValue - weaponValue;

            ActivePlayer.PlayerHealth.ChangeHealth(-damage);
            ActivePlayer.PlayerHand.DiscardCards([selectedCardIndex], ActiveDeck.DiscardList);
            RanPreviousRound = false;
        }

        public void DrinkHealthPotionFromHand(int selectedCardIndex)
        {
            ActivePlayer.PlayerHealth.ChangeHealth(ActivePlayer.PlayerHand.HandList[selectedCardIndex].CardValue);

            ActivePlayer.PlayerHand.DiscardCards([selectedCardIndex], ActiveDeck.DiscardList);
            RanPreviousRound = false;
        }

        public void EquipNewWeaponFromHand(int selectedCardIndex)
        {
            DiscardCurrentWeapon();

            ActivePlayer.PlayerWeapon = new ScoundrelWeapon(new Card(ActivePlayer.PlayerHand.HandList[selectedCardIndex].CardRank, ActivePlayer.PlayerHand.HandList[selectedCardIndex].CardSuit));

            ActivePlayer.PlayerHand.HandList.RemoveAt(selectedCardIndex);
            RanPreviousRound = false;
        }
        public void DiscardCurrentWeapon()
        {
            if (ActivePlayer.PlayerWeapon == null)
                return;

            List<Card> tempList =
            [
                new Card(ActivePlayer.PlayerWeapon.WeaponCard.CardRank, ActivePlayer.PlayerWeapon.WeaponCard.CardSuit)
            ];

            foreach (Card card in ActivePlayer.PlayerWeapon.EnemyCache)
            {
                tempList.Add(new Card(card.CardRank, card.CardSuit));
            }

            ActiveDeck.DiscardList.AddRange(tempList);
            ActivePlayer.PlayerWeapon = null;
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
            RoundCount = 0;
            RanPreviousRound = false;
        }
    }
}