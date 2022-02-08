using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float Damage;

    private bool _hasCollidedOnce;

    // Damage the first thing collided with if it is damageable.
    private void OnCollisionEnter(Collision other)
    {
        if (_hasCollidedOnce)
        {
            return;
        }

        if (!other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>()?.Damage(Damage);
        }

        _hasCollidedOnce = true;
    }
}
