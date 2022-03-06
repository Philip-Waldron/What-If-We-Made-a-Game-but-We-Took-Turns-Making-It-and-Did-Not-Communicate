using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;




public class PlayerTimerAlive : MonoBehaviour
{

    public Text timerTex; //Text for GUI    
    public float levelTimer = 0f;
    public bool updateTimer = false;

    void start()
    {
        updateTimer = true;
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
            timerTex.text = levelTimer.ToString("f2");
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
