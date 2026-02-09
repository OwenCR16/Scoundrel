namespace Scoundrel
{
    public class Player
    {
        public Health PlayerHealth { get; set; } = new Health();
        public Hand PlayerHand { get; set; } = new Hand();
        public Weapon PlayerWeapon { get; set; } = new Weapon();
    }
}
