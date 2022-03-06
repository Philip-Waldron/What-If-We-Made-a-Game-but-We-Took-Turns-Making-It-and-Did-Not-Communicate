using UnityEngine.UI;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    //I am to lazy to comment
    // I'm too lazy to implement a proper Singleton, plz only have 1 PlayerController or you will create a paradox and kill us all (REALLY BAD).
    // Use this to get a reference to the player from anywhere (it's static).
    public static KillCounter Instance;

    public Text killTex; //Text for GUI
    public float killerCount = 0f;
    public bool updatekill = false;

    void Start()
    {
        Instance = this;
        updatekill = true;
    }

    void End()
    {
        updatekill = false;
    }

    void Update()
    {
        if (updatekill == true)
        {
            killTex.text = killerCount.ToString("0");
        }

    }
    public void Add()
    {
       killerCount++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            updatekill = false;
        }

    }

}