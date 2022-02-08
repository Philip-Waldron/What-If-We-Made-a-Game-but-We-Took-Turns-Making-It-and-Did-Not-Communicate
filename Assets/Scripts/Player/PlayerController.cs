using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Player Properties")]
    public Transform Body;
    public Transform TrueAimDirection;
    private bool _isAiming;

    [Header("Movement Values")]
    public float MoveSpeed = 5;
    [HideInInspector]
    public Vector3 MoveDirection;

    [Header("Basic Projectile")]
    public float ShootRate;
    public Vector3 GlassThrowForce;

    [Header("I Shouldn't Have Implemented This Lmao")]
    public float ThinkGunRechargeTime;
    public float ThinkGunShootRadius;
    public float ThinkGunArmTime;
    public float ThinkGunChargeTime;
    public float ThinkGunKnockbackStrength;
    public float ThinkGunDamage;

    [Header("Helth")]
    public float Helth;
    public float OnDamageInvulnerableTime;

    // I'm too lazy to implement a proper Singleton, plz only have 1 PlayerController or you will create a paradox and kill us all (REALLY BAD).
    // Use this to get a reference to the player from anywhere (it's static).
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateFacingDirection();
    }

    // Force the player body to aim in a direction, overriding their natural look direction.
    // This is used for shooting.
    public void ForceAim(Quaternion direction)
    {
        _isAiming = true;
        TrueAimDirection.rotation = direction;
        Body.rotation = Quaternion.RotateTowards(Body.rotation, direction, Time.deltaTime * 1000);
    }

    // Stop the forced aiming.
    public void StopAim()
    {
        _isAiming = false;

        if (MoveDirection.normalized * -1 == Vector3.zero)
        {
            Body.rotation = Quaternion.RotateTowards(Body.rotation, Quaternion.identity, Time.deltaTime * 1000);
        }
        else
        {
            Body.rotation = Quaternion.RotateTowards(Body.rotation, Quaternion.LookRotation(MoveDirection.normalized * -1), Time.deltaTime * 1000);
        }
    }

    // Face the direction of movement.
    private void UpdateFacingDirection()
    {
        if (_isAiming)
        {
            return;
        }

        if (MoveDirection.normalized * -1 == Vector3.zero)
        {
            Body.rotation = Quaternion.RotateTowards(Body.rotation, Quaternion.identity, Time.deltaTime * 1000);
        }
        else
        {
            Body.rotation = Quaternion.RotateTowards(Body.rotation, Quaternion.LookRotation(MoveDirection.normalized * -1), Time.deltaTime * 1000);
        }
    }
}
