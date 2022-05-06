namespace Upgrades
{
    public class healthCap : PermanentUpgradeBoi
    {
        protected override void GimmeUpgrade()
        {
            PlayerStats.IncreaseMaxHelth();
            PlayerStats.Helth = PlayerStats.GetMaxHelth();         
        }
    }
}