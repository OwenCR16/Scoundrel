namespace Scoundrel
{
    public class ScoundrelWeapon
    {
        public ScoundrelWeapon(Card card)
        {
            WeaponCard = card;
            EnemyCache = [];
        }
        public Card WeaponCard {get; set;}
        public List<Card> EnemyCache {get; set;}
        public void DisplayWeapon()
        {
            Console.WriteLine($"Currently equipped weapon: {WeaponCard.CardRank} of {WeaponCard.CardSuit}");
            Console.Write("List of enemies fought: ");
            foreach (Card card in EnemyCache)
            {
                Console.Write($"{WeaponCard.CardRank} of {WeaponCard.CardSuit}, ");
            }
            Console.WriteLine();
        }
    }
}