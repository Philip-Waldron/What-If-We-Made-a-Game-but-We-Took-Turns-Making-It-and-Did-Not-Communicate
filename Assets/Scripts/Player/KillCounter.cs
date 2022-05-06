using System;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class KillCounter : MonoBehaviour
{
    //I am to lazy to comment
    // I'm too lazy to implement a proper Singleton, plz only have 1 PlayerController or you will create a paradox and kill us all (REALLY BAD).
    // Use this to get a reference to the player from anywhere (it's static).
    public static KillCounter Instance;

    public TextMeshProUGUI killTex; //Text for GUI
    public int killerCount = 0;
    public float ecomomicIMPACT = 0f;

    [SerializeField] private EcomincDamageFloaty floaty;

    private void Awake() => Instance = this;

    private void Update() => killTex.text = $"{killerCount} | {ecomomicIMPACT}.eth";
    
    public void Add(Enemy enemy)
    {
        killerCount++;
        theBlockchain.Add(DeathHeadline(Random.Range(0, 3), enemy));
    }

    public void PlayerTookDamage() => theBlockchain.Add(PlayerDamageHeadline(Random.Range(0, 7)));

    private static string PlayerDamageHeadline(int i) =>
        i switch
        {
            0 => $"Surge in NFT market!",
            1 => $"Surge in NFT market!",
            2 => $"Surge in NFT market!",
            3 => $"Surge in NFT market!",
            4 => $"Surge in NFT market!",
            5 => $"Surge in NFT market!",
            6 => $"Surge in NFT market!",
            _ => $"Surge in NFT market!"
        };

    private static string DeathHeadline(int i, Enemy enemy) =>
        i switch
        {
            0 => $"{enemy.enemyName}, worth {enemy._initial}.eth was hacked by the hacker Anonymous, {DateTime.Now}",
            1 => $"{enemy.enemyName}, worth {enemy._initial}.eth was hacked by the hacker Anonymous, {DateTime.Now}",
            2 => $"{enemy.enemyName}, worth {enemy._initial}.eth was hacked by the hacker Anonymous, {DateTime.Now}",
            _ => $"{enemy.enemyName}, worth {enemy._initial}.eth was hacked by the hacker Anonymous, {DateTime.Now}"
        };

    public void TankEconomy(Enemy enemy, float damage, Vector3 location, bool cascade)
    {
        ecomomicIMPACT += damage;
        theBlockchain.Add($"{enemy.enemyName} HAS BEEN HACKED");
        if (cascade) return;
        EcomincDamageFloaty floaterer = Instantiate(floaty.gameObject, null).GetComponent<EcomincDamageFloaty>();
        floaterer.CreateFlaoty(damage, location);
        theBlockchain.Add(DamageHeadline(Random.Range(0, 7), damage, enemy));
    }
    
    private static string DamageHeadline(int i, float damage, Enemy enemy) =>
        i switch
        {
            0 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            1 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            2 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            3 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            4 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            5 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            6 => $"Local man loses {damage}.eth on {enemy.enemyName}",
            _ => $"Local man loses {damage}.eth on {enemy.enemyName}"
        };

    public List<string> theBlockchain = new List<string>();
}
