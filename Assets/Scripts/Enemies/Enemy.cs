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
    public GameObject deflationEFFECT;

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
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceHeading * deflationForce, ForceMode.Impulse);
                if (!ReferenceEquals(damageable, this))
                {
                    damageable.Damage(deflationForce, true);
                }
            }
        }

        GameObject death = Instantiate(deflationEFFECT, null);
        death.transform.position = transform.position;
    }

    // Take damage.
    public void Damage(float damageTaken, bool cascade)
    {
        _health -= damageTaken;
        KillCounter.Instance.TankEconomy(damageTaken, transform.position, cascade);
        lastHitTime = Time.time;
        if (_health <= 0)
        {
            if (!cascade)
            {
                ImminentDeflation();
            }
            Kill();
        }
    }

    // Damage the player when colliding.
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<IDamageable>()?.Damage(AttackDamage, false);
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
