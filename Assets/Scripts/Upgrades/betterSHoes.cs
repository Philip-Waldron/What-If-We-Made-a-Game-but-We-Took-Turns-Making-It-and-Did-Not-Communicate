namespace Upgrades
{
    public class betterSHoes : PermanentUpgradeBoi
    {
        public float sppedBost = .25f;

        protected override void GimmeUpgrade() => PlayerStats.MoveSpeed *= sppedBost;
    }
}