using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHelth : MonoBehaviour, IDamageable
{
    public AudioSource AudioSource;
    public AudioClip[] HurtSounds;
    public AudioClip DeathSound;

    private bool _invulnerable;
    private bool _ded;

    public UnityEvent dedHa;

    // Player is disable pls no missing references.
    private bool _forRealDed;

    private void Update()
    {
        if (_ded && !AudioSource.isPlaying && !_forRealDed)
        {
            PlayerController.Instance.gameObject.SetActive(false);
            _forRealDed = true;
        }
    }

    // This kills the player.
    public void Kill()
    {
        AudioSource.PlayOneShot(DeathSound);
        _ded = true;
        dedHa.Invoke();
    }

    // This damages the player.
    // Called from the Enemy class, when the enemy collides.
    public void Damage(float damageTaken, bool cascade)
    {
        if (_invulnerable || _ded)
        {
            return;
        }

        PlayerController.Instance.Helth -= damageTaken;
        
        

        if (PlayerController.Instance.Helth <= 0)
        {
            KillCounter.Instance.PlayerDied();
            Kill();
        }
        else
        {
            KillCounter.Instance.PlayerTookDamage();
            AudioSource.PlayOneShot(HurtSounds[Random.Range (0, HurtSounds.Length)]);
            StartCoroutine(InvulnerableTimer());
        }
    }

    // When you get hit let's give you a chance to run away.
    IEnumerator InvulnerableTimer()
    {
        _invulnerable = true;
        yield return new WaitForSeconds(PlayerController.Instance.OnDamageInvulnerableTime);
        _invulnerable = false;
    }
}
