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

    private void Update() => killTex.text = $"{Math.Round(ecomomicIMPACT, 2)} ETH | {killerCount}";
    
    public void Add(Enemy enemy)
    {
        killerCount++;
        theBlockchain.Add(DeathHeadline(Random.Range(0, 4), enemy));
    }

    public void PlayerTookDamage() => theBlockchain.Add(PlayerDamageHeadline(Random.Range(0, 3)));
    public void PlayerDied() => theBlockchain.Add($"[{DateTime.Now}] NFT market itself was too strong, and in the end our valiant insider trading failed us.");

    private static string PlayerDamageHeadline(int i) =>
        i switch
        {
            0 => $"[{DateTime.Now}] New NFT project backed by The Vatican, there has been a surge in demand and prices are sky high!",
            1 => $"[{DateTime.Now}] New NFT project backed by Femboys International, there has been a surge in demand and prices are sky high!",
            2 => $"[{DateTime.Now}] Despite trepidation, the volume of new NFTs being sold is higher than ever!",
            _ => $"[{DateTime.Now}] New NFT project backed by The Vatican, there has been a surge in demand and prices are sky high!"
        };

    private static string DeathHeadline(int i, Enemy enemy) =>
        i switch
        {
            0 => $"[{DateTime.Now}] {enemy.enemyName}, value plummets from {enemy._initial} ETH to 0 ETH",
            1 => $"[{DateTime.Now}] Solar flare wipes local man's USB, wiping {enemy.enemyName}, valued at {enemy._initial} ETH",
            2 => $"[{DateTime.Now}] Hacked. All my {enemy.enemyName} gone.",
            3 => $"[{DateTime.Now}] Fraud? Local NFT project founder disappeared, along with {enemy.enemyName} worth {enemy._initial} ETH",
            _ => $"[{DateTime.Now}] Fraud? Local NFT project founder disappeared, along with {enemy.enemyName} worth {enemy._initial} ETH"
        };

    public void TankEconomy(Enemy enemy, float damage, Vector3 location, bool cascade)
    {
        ecomomicIMPACT += damage;
        if (cascade) return;
        EcomincDamageFloaty floaterer = Instantiate(floaty.gameObject, null).GetComponent<EcomincDamageFloaty>();
        floaterer.CreateFlaoty(damage, location);
        theBlockchain.Add(DamageHeadline(Random.Range(0, 4), damage, enemy));
    }
    
    private static string DamageHeadline(int i, float damage, Enemy enemy) =>
        i switch
        {
            0 => $"[{DateTime.Now}] Elon Musk tweets something stupid, {enemy.enemyName} loses {damage} ETH",
            1 => $"[{DateTime.Now}] {enemy.enemyName}'s value drops from {enemy._initial} ETH to {enemy._health} ETH",
            2 => $"[{DateTime.Now}] Inflation causing surge in devaluation as {enemy.enemyName} loses {damage} ETH",
            3 => $"[{DateTime.Now}] {enemy.enemyName} was seemingly more fungible than first thought and drops {damage} ETH in value",
            _ => $"[{DateTime.Now}] {enemy.enemyName} was seemingly more fungible than first thought and drops {damage} ETH in value"
        };

    public List<string> theBlockchain = new List<string>();
}
