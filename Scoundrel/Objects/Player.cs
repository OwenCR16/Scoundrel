namespace Scoundrel
{
    public class Player
    {
        public Health PlayerHealth { get; set; } = new Health();
        public ScoundrelHand PlayerHand { get; set; } = new ScoundrelHand();
        public ScoundrelWeapon? PlayerWeapon { get; set; }
        public void DisplayPlayerProperties()
        {
            PlayerHand.DisplayHand();
            PlayerHealth.DisplayHealth();
            if(PlayerWeapon != null)
                PlayerWeapon.DisplayWeapon();
            else
                Console.WriteLine("No weapon equipped.");
        }
    }
}
