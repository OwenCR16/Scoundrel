namespace Scoundrel
{
    public class Health
    {
        public int HealthValue { get; set; } = 20;
        public void ChangeHealth(int changeValue)
        {
            HealthValue += changeValue;
        }
    }
}