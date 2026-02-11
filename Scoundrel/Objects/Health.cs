namespace Scoundrel
{
    public class Health
    {
        public int HealthValue { get; set; } = 20;
        public void ChangeHealth(int changeValue)
        {
            HealthValue += changeValue;
            //USE VALUE MODIFIERS OBJECT FOR THE BELOW AND PASS INTO THE METHOD
            bool healthCanBeOverTwenty = false;
            if (!healthCanBeOverTwenty && HealthValue > 20)
                HealthValue = 20;
        }
        public void DisplayHealth()
        {
            Console.WriteLine($"Player health: {HealthValue}");
        }
    }
}