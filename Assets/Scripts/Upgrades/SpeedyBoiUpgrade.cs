using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyBoiUpgrade : SpinnyBoi
{
    [Header("Oh look some numbers")]
    public float sizeMultiplier = 0.6f;
    public float duration = 10.0f;
    public float moveSpeedMultiier = 2.0f;
    public float downTime = 60.0f;

    // [Header("Oh look some particles")]
    // public GameObject pickupEffect;

    protected override IEnumerator Pickup(Collider player)
    {
        // Particles woo
        // Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get players numbers to fuck with
        PlayerController stats = player.GetComponent<PlayerController>();

        // Apply powerup
        player.transform.localScale *= sizeMultiplier;
        stats.MoveSpeed *= moveSpeedMultiier;

        // Disable powerup
        DisableMeDaddy();

        // Wait x seconds
        yield return new WaitForSeconds(duration);

        // Reverse powerup
        player.transform.localScale /= sizeMultiplier;
        stats.MoveSpeed /= moveSpeedMultiier;

        // Powerup wait time
        yield return new WaitForSeconds(downTime);

        // Enable powerup
        ImBackBaby();
    }
}
