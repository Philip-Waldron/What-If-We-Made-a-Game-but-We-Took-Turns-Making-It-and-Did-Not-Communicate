using Player;
using UnityEngine.UI;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    //I am to lazy to comment
    // I'm too lazy to implement a proper Singleton, plz only have 1 PlayerController or you will create a paradox and kill us all (REALLY BAD).
    // Use this to get a reference to the player from anywhere (it's static).
    public static KillCounter Instance;

    public Text killTex; //Text for GUI
    public int killerCount = 0;
    public float ecomomicIMPACT = 0f;
    public bool updatekill = false;

    [SerializeField] private EcomincDamageFloaty floaty;

    void Start()
    {
        Instance = this;
        updatekill = true;
    }

    void End()
    {
        updatekill = false;
    }

    void Update() => killTex.text = $"{killerCount.ToString(("0"))} | {ecomomicIMPACT}.eth";
    
    public void Add() => killerCount++;

    public void TankEconomy(float damage, Vector3 location)
    {
        ecomomicIMPACT += damage;
        EcomincDamageFloaty floaterer = Instantiate(floaty.gameObject, null).GetComponent<EcomincDamageFloaty>();
        floaterer.CreateFlaoty(damage, location);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            updatekill = false;
        }

    }

}
