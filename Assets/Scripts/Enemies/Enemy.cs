using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public float AttackDamage = 1f;
    [SerializeField] private Vector2 _helthRange;
    private float _health, _initial;

    public float lastHitTime { get; private set; } = 0f;

    [HideInInspector]
    public SpawnEnemies Spawner;

    public float RemainingValue() => _health / _initial;
    public float CurrentValuation() => _health;

    private void Awake()
    {
        _health = Random.Range(_helthRange.x, _helthRange.y);
        _initial = _health;
    }

    // RIP
    public void Kill()
    {
        KillCounter.Instance.Add();
        Spawner.SpawnedEnemies.Remove(this);
        Destroy(gameObject);
    }

    // Take damage.
    public void Damage(float damageTaken)
    {
        _health -= damageTaken;
        KillCounter.Instance.TankEconomy(damageTaken, transform.position);
        lastHitTime = Time.time;
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
