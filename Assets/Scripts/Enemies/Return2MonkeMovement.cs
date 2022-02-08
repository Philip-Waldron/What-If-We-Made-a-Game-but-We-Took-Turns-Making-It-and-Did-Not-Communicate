using System.Collections;
using UnityEngine;

public class Return2MonkeMovement : EnemyMovement
{
    // Move the monkey up and rotate it using forces.
    protected override void Move()
    {
        Rigidbody.AddForce(Vector3.up * UpwardForce, ForceMode.Impulse);
        Rigidbody.AddTorque(Vector3.up * RotationForce, ForceMode.Impulse);
        StartCoroutine(DelayedMove(1f));

        CanMove = false;
    }

    // After a delay, fire towards the player angry birds style.
    IEnumerator DelayedMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Point to player.
        Vector3 heading = Player.position - transform.position;
        // Get normalised direction to player.
        Vector3 direction = heading / heading.magnitude;

        Rigidbody.AddForce(direction * ForwardForce, ForceMode.Impulse);
    }
}
