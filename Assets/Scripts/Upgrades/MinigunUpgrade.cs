using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunUpgrade : SpinnyBoi
{
    [Header("Oh look some numbers")]
    public float sizeMultiplier = 1.4f;
    public float duration = 10.0f;
    public float shootRateMultiplier = 1000.0f;
    public float moveSpeedMultiier = 0.5f;
    public float projectileSpeedMultiplier = 2.0f;
    public float downTime = 60.0f;

    protected override IEnumerator Pickup(Collider player)
    {
        // Particles woo
        // Instantiate(pickupEffect, transform.position, transform.rotation);

        // Get players numbers to fuck with
        PlayerController stats = player.GetComponent<PlayerController>();

        // Apply powerup
        player.transform.localScale *= sizeMultiplier;
        stats.ShootRate /= shootRateMultiplier;
        stats.MoveSpeed *= moveSpeedMultiier;
        stats.GlassThrowForce = new Vector3(stats.GlassThrowForce.x, stats.GlassThrowForce.y, stats.GlassThrowForce.z * projectileSpeedMultiplier);

        // Disable powerup
        DisableMeDaddy();

        // Wait x seconds
        yield return new WaitForSeconds(duration);

        // Reverse powerup
        player.transform.localScale /= sizeMultiplier;
        stats.ShootRate *= shootRateMultiplier;
        stats.MoveSpeed /= moveSpeedMultiier;
        stats.GlassThrowForce = new Vector3(stats.GlassThrowForce.x, stats.GlassThrowForce.y, stats.GlassThrowForce.z / projectileSpeedMultiplier);

        // Powerup wait time
        yield return new WaitForSeconds(downTime);

        // Enable powerup
        ImBackBaby();
    }
}
