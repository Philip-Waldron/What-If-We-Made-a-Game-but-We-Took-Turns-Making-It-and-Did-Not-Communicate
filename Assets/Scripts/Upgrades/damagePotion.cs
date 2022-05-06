using UnityEngine;

namespace Upgrades
{
    public class damagePotion : PermanentUpgradeBoi
    {
        [SerializeField] private float moreDamagE = 1.25f;

        protected override void GimmeUpgrade() => PlayerStats.ThinkGunDamage *= moreDamagE;
    }
}