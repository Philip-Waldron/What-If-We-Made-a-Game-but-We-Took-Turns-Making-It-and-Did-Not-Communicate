using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public string enemyName;
    public float AttackDamage = 1f;
    [SerializeField] private Vector2 _helthRange;
    internal float _health, _initial;
    public bool deflationOnDeath = true;
    public float deflationRadius = 1f;
    public float deflationForce = 1f;
    public GameObject deflationEFFECT;

    public float lastHitTime { get; private set; } = 0f;

    [HideInInspector]
    public SpawnEnemies Spawner;

    public float RemainingValue() => _health / _initial;

    private void Awake()
    {
        _health = Random.Range(_helthRange.x, _helthRange.y);
        _initial = _health;
        enemyName += $" #{Random.Range(10000, 99999)}";
    }

    // RIP
    public void Kill()
    {
        KillCounter.Instance.Add(this);
        Spawner.SpawnedEnemies.Remove(this);
        GameObject death = Instantiate(deflationEFFECT, null);
        death.transform.position = transform.position;
        CameraShaker4000.Instance.ShakeCamera();
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
    }

    // Take damage.
    public void Damage(float damageTaken, bool cascade)
    {
        _health -= damageTaken;
        KillCounter.Instance.TankEconomy(this, damageTaken, transform.position, cascade);
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
