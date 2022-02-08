// Interface for the contract of damageable objects. Probably overkill, but this is what I'm using.
// Used by projectiles for example, when they hit a IDamageable Enemy.
// Also used by the PlayerHelth for when enemies hit them.
public interface IDamageable
{
    void Kill();
    void Damage(float damageTaken);
}