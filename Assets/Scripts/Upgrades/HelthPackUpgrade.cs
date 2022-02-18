using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelthPackUpgrade : SpinnyBoi
{
    [Header("Oh look some numbers")]
    public float downTime = 90.0f;

    // [Header("Oh look some particles")]
    // public GameObject pickupEffect;

    protected override IEnumerator Pickup(Collider player)
    {
        // Particles woo
        // Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get players numbers to fuck with
        PlayerController stats = player.GetComponent<PlayerController>();

        if(stats.Helth < stats.GetMaxHelth())
        {
            // Apply powerup
            stats.Helth = stats.GetMaxHelth();

            // Disable powerup
            DisableMeDaddy();

            // Powerup wait time
            yield return new WaitForSeconds(downTime);

            // Enable powerup
            ImBackBaby();
        }
    }
}
