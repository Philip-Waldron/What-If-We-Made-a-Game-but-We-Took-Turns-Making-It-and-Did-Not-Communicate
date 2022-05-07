using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public AudioSource Music;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Music.mute = !Music.mute;
        }
    }
}
