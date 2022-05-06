using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PermanentUpgradeBoi : MonoBehaviour
{
    protected PlayerController PlayerStats;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerStats = other.GetComponent<PlayerController>();
        GimmeUpgrade();
        Destroy(gameObject);
    }

    protected abstract void GimmeUpgrade();
}
