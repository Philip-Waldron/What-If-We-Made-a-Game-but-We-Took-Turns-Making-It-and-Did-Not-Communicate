using UnityEngine;

public class AudioOnCollision : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip[] AudioClips;

    private bool _hasCollidedOnce;

    // Play a sound once on first collision only.
    private void OnCollisionEnter(Collision other)
    {
        if (_hasCollidedOnce)
        {
            return;
        }

        AudioSource.PlayOneShot(AudioClips[Random.Range (0, AudioClips.Length)]);
        _hasCollidedOnce = true;
    }
}
