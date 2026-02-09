namespace Scoundrel
{
    public class Player
    {
        public Health PlayerHealth { get; set; } = new Health();
        public ScoundrelHand PlayerHand { get; set; } = new ScoundrelHand();
        public ScoundrelWeapon PlayerWeapon { get; set; } = new ScoundrelWeapon();
    }
}
