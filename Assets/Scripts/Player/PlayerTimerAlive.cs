using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class PlayerTimerAlive : MonoBehaviour
{

    public TextMeshProUGUI timerTex; //Text for GUI    
    public float levelTimer = 0f;
    public bool updateTimer = false;

    public static PlayerTimerAlive Instance;

    void start()
    {
        updateTimer = true;
    }

    private void Awake()
    {
        Instance = this;
    }

    void End()
    {
        updateTimer = false;
    }

    void Update()
    {
        if (updateTimer == true)
        {
            levelTimer += Time.deltaTime;
            timerTex.text = $"{levelTimer}s";
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            updateTimer = false;
        }
    }




}
