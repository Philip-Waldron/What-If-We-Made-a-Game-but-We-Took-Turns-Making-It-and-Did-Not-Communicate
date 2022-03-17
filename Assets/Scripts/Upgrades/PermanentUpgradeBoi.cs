using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgradeBoi : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController stats = other.GetComponent<PlayerController>();
            stats.ShootRate *= 0.85f;

            Destroy(gameObject);
        }
    }
}
