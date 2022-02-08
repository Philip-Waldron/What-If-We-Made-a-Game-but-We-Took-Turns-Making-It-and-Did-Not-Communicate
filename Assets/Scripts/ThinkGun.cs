using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThinkGun : MonoBehaviour
{
    public SpawnEnemies Spawner;

    public AudioSource AudioSource;
    public float ShootSoundPlayTime = 3;

    private bool _shotInProgress;
    private float _shootTimer;
    private bool _canShoot = true;
    private bool _rechargingShot;

    private void Update()
    {
        if (_rechargingShot)
        {
            UpdateRechargeTimer();
        }

        HandleInput();
        if (!_shotInProgress)
        {
            LookAtClosestEnemy();
        }

        Debug.DrawRay(transform.position, transform.forward * 100);
    }

    // Manage the internal clock for the shoot rate (recharge).
    private void UpdateRechargeTimer()
    {
        _shootTimer += Time.deltaTime;
        if (_shootTimer > PlayerController.Instance.ThinkGunRechargeTime)
        {
            _canShoot = true;
            _rechargingShot = false;
            _shootTimer = 0;;
        }
    }

    // Get your handles off my input. Get your own input.
    private void HandleInput()
    {
        if (_canShoot && Input.GetKeyDown(KeyCode.Space) && !AudioSource.isPlaying)
        {
            StartCoroutine(PrepareAndFireThinkGun());
        }
    }

    // Point the gun towards the closest enemy.
    private void LookAtClosestEnemy()
    {
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        Vector3 closestDirection = Vector3.zero;

        foreach (Enemy enemy in Spawner.SpawnedEnemies)
        {
            Vector3 direction =  enemy.transform.position - transform.position;
            float length = direction.sqrMagnitude;

            if (length < closestDistance)
            {
                closestDirection = direction;
                closestDistance = length;
                closestEnemy = enemy;
            }
        }

        // We rotate towards the enemy over time.
        if (closestEnemy != null)
        {
            Quaternion toRotation = Quaternion.LookRotation(closestDirection, transform.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 5 * Time.deltaTime);
        }
    }

    // Play the audio related to shooting.
    // This plays a random section of audio from the clip.
    private void PlayAudio(float sectionLength)
    {
        AudioSource.time = Random.Range(0f, AudioSource.clip.length - sectionLength);
        AudioSource.Play();
        AudioSource.SetScheduledEndTime(AudioSettings.dspTime + sectionLength);
    }

    // Take the shot.
    // Damages enemies and adds a force to them away from the player.
    private void Shoot(Vector3 position, Vector3 direction)
    {
        RaycastHit[] hits = Physics.SphereCastAll(position, PlayerController.Instance.ThinkGunShootRadius, direction, 100);
        foreach (RaycastHit hit in hits)
        {
            IDamageable damageable = hit.transform.gameObject.GetComponent<IDamageable>();

            if (damageable != null && !hit.transform.gameObject.CompareTag("Player"))
            {
                Vector3 forceHeading = hit.transform.position - PlayerController.Instance.transform.position;
                float forceDistance = forceHeading.magnitude;
                Vector3 forceDirection = forceHeading / forceDistance;
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(forceDirection * PlayerController.Instance.ThinkGunKnockbackStrength, ForceMode.Impulse);
                damageable.Damage(PlayerController.Instance.ThinkGunDamage);

                // lil knockback.
                PlayerController.Instance.GetComponent<Rigidbody>().AddForce(-forceDirection * (PlayerController.Instance.ThinkGunPlayerKnockbackStrength), ForceMode.Impulse);
            }
        }
    }

    // Handle all the gun states.
    // Grow to full size, Play audio, charge shot, fire shot, wait for audio to finish, shrink back to the nothing from whence it came.
    IEnumerator PrepareAndFireThinkGun()
    {
        _canShoot = false;

        PlayAudio(PlayerController.Instance.ThinkGunArmTime + PlayerController.Instance.ThinkGunChargeTime + ShootSoundPlayTime + (PlayerController.Instance.ThinkGunArmTime / 2));

        float elapsedTime = 0;
        transform.localScale = new Vector3(0, 0, 0);
        while (elapsedTime < PlayerController.Instance.ThinkGunArmTime)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2, 2, 2), elapsedTime / (PlayerController.Instance.ThinkGunArmTime / 2));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = new Vector3(2, 2, 2);

        _shotInProgress = true;
        elapsedTime = 0;
        while (elapsedTime < PlayerController.Instance.ThinkGunChargeTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Shoot(transform.position, transform.forward);

        elapsedTime = 0;
        while (elapsedTime < ShootSoundPlayTime)
        {
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime < PlayerController.Instance.ThinkGunArmTime / 2)
        {
            transform.localScale = Vector3.Lerp(new Vector3(2, 2, 2), new Vector3(0, 0, 0), elapsedTime / (PlayerController.Instance.ThinkGunArmTime / 2));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = new Vector3(0, 0, 0);
        _shotInProgress = false;
        _rechargingShot = true;

        yield return null;
    }
}
