using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public float AttackDamage = 1f;
    [SerializeField] private Vector2 _helthRange;
    private float _health, _initial;
    public bool deflationOnDeath = true;
    public float deflationRadius = 1f;
    public float deflationForce = 1f;

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

    private void ImminentDeflation()
    {
        if (!deflationOnDeath) return;
        
        foreach (Collider hit in Physics.OverlapSphere(transform.position, deflationRadius))
        {
            IDamageable damageable = hit.transform.gameObject.GetComponent<IDamageable>();

            if (damageable != null && !hit.transform.gameObject.CompareTag("Player"))
            {
                Vector3 forceHeading = hit.transform.position - transform.position;
                float forceDistance = forceHeading.magnitude;
                Vector3 forceDirection = forceHeading / forceDistance;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * deflationForce, ForceMode.Impulse);
                damageable.Damage(deflationForce);
            }
        }
    }

    // Take damage.
    public void Damage(float damageTaken)
    {
        _health -= damageTaken;
        KillCounter.Instance.TankEconomy(damageTaken, transform.position);
        lastHitTime = Time.time;
        if (_health <= 0)
        {
            ImminentDeflation();
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

    private void OnDrawGizmos()
    {
        if (deflationOnDeath)
        {
            Gizmos.DrawWireSphere(transform.position, deflationRadius);
        }
    }
}
