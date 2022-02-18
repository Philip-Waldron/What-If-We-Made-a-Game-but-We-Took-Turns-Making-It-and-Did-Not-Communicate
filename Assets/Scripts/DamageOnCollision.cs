using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    public float Damage;

    private bool _hasCollidedOnce;

    private float initializationTime;

    void Start()
    {
        initializationTime = Time.timeSinceLevelLoad;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad - initializationTime > 1)
        {
            _hasCollidedOnce = true;
        }
    }

    // Damage the first thing collided with if it is damageable.
    private void OnCollisionEnter(Collision other)
    {
        if (_hasCollidedOnce)
        {
            return;
        }

        if (!other.gameObject.CompareTag("Enemy"))
        {
            return;
        }

        other.gameObject.GetComponent<IDamageable>()?.Damage(Damage);
        _hasCollidedOnce = true;
    }
}
