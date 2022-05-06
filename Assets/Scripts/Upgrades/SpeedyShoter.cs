namespace Upgrades
{
    public class SpeedyShoter : PermanentUpgradeBoi
    {
        public float moreShoot = .85f;

        protected override void GimmeUpgrade()
        {
            PlayerStats.ShootRate *= moreShoot;
        }
    }
}