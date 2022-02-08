using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float AttackDamage = 1f;
    [SerializeField]
    private float _health = 3f;

    [HideInInspector]
    public SpawnEnemies Spawner;

    // RIP
    public void Kill()
    {
        Spawner.SpawnedEnemies.Remove(this);
        Destroy(gameObject);
    }

    // Take damage.
    public void Damage(float damageTaken)
    {
        _health -= damageTaken;
        if (_health <= 0)
        {
            Kill();
        }
    }

    // Damage the player when colliding.
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>()?.Damage(AttackDamage);
        }
    }
}
