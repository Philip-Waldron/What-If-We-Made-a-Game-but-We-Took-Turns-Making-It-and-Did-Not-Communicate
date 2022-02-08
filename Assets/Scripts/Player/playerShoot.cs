using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class playerShoot : MonoBehaviour
{
    [Serializable]
    public struct Projectile
    {
        public GameObject ThrowableObject;
        public int ThrowChanceWeight;
        public float Damage;
    }

    // Ladies and gentlemen, get your projectiles here.
    public Projectile[] Projectiles;
    public Transform Emitter;

    // For handling the shoot rate.
    private float _shootTimer;
    private bool _canShoot = true;

    void Update()
    {
        UpdateShootTimer();
        HandleInput();
    }

    // Manage the internal clock for the shoot rate.
    private void UpdateShootTimer()
    {
        if (_canShoot)
        {
            _shootTimer = 0;
        }

        if (!_canShoot)
        {
            _shootTimer += Time.deltaTime;
            if (_shootTimer > PlayerController.Instance.ShootRate)
            {
                _canShoot = true;
                _shootTimer -= PlayerController.Instance.ShootRate;
            }
        }
    }

    // Keyboard keys for aiming the player.
    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow))
        {
            Aim(Quaternion.Euler(0, 180, 0));
        }
        else if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
        {
            Aim(Quaternion.Euler(0, 90, 0));
        }
        else if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
        {
            Aim(Quaternion.Euler(0, 270, 0));
        }
        else if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow))
        {
            Aim(Quaternion.Euler(0, 0, 0));
        }
    }

    // Aim the player in the shooting direction.
    // Use a coroutine to delay resetting the look direction for when the player stops aiming.
    private void Aim(Quaternion direction)
    {
        PlayerController.Instance.ForceAim(direction);
        StopAllCoroutines();
        StartCoroutine(DelayedStopAim(0.5f));

        if (_canShoot)
        {
            Shoot();
        }
    }

    // It shoots.
    private void Shoot()
    {
        GameObject thrownLightbulb = RollRandomLightbulb();
        Rigidbody thrownLightbulbRigidbody = thrownLightbulb.GetComponent<Rigidbody>();

        // Add force to the forward direction of the projectile.
        thrownLightbulbRigidbody.AddRelativeForce(PlayerController.Instance.GlassThrowForce, ForceMode.Impulse);

        // Add force based on player movement.
        AddMovementForceToProjectile(thrownLightbulbRigidbody);

        // Spin the projectile in a random direction.
        thrownLightbulbRigidbody.AddRelativeTorque(new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), ForceMode.Impulse);

        // Wait for the shoot timer to allow us to shoot again.
        _canShoot = false;
    }

    // There's multiple kind of objects the player can throw.
    // Let's randomise that using some defined weighting to set rarity.
    private GameObject RollRandomLightbulb()
    {
        int totalWeight = 0;
        foreach (Projectile projectile in Projectiles)
        {
            totalWeight += projectile.ThrowChanceWeight;
        }

        float rollForInitiative = Random.Range(0f, totalWeight);

        int evaluatingWeight = 0;
        for (int index = 0; index < Projectiles.Length; index++)
        {
            evaluatingWeight += Projectiles[index].ThrowChanceWeight;
            if (rollForInitiative <= evaluatingWeight)
            {
                GameObject projectile = Instantiate(Projectiles[index].ThrowableObject, Emitter.position, Emitter.rotation);
                DamageOnCollision damageOnCollision = projectile.GetComponent<DamageOnCollision>();
                damageOnCollision.Damage = Projectiles[index].Damage;
                return projectile;
            }
        }

        Debug.LogError("We shouldn't have gotten here. If you see this message oh god oh fuck.");
        return null;
    }

    // This lets us add sideways movement to the player for fun projectile physics.
    // We want to avoid using forward direction as that would mean the projectile would go farther when moving forwards, or not as far when moving backwards.
    private void AddMovementForceToProjectile(Rigidbody rigidbody)
    {
        if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidbody.AddForce(new Vector3(PlayerController.Instance.MoveDirection.x * 5f, 0, 0), ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, PlayerController.Instance.MoveDirection.z * 5f), ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector3(0, 0, PlayerController.Instance.MoveDirection.z * 5f), ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.K) || Input.GetKey(KeyCode.DownArrow))
        {
            rigidbody.AddForce(new Vector3(PlayerController.Instance.MoveDirection.x * 5f, 0, 0), ForceMode.Impulse);
        }
    }

    // A delayed stopping of setting aim direction for a little delicious juice.
    private IEnumerator DelayedStopAim(float time)
    {
        yield return new WaitForSeconds(time);
        PlayerController.Instance.StopAim();
    }
}
