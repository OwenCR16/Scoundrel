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
    }
}