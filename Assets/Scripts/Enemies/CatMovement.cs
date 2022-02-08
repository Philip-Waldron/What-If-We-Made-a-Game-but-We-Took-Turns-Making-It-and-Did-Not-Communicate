using UnityEngine;

public class CatMovement : EnemyMovement
{

    // Just a lil force jump towards the player.
    protected override void Move()
    {
        // Point to player.
        Vector3 heading = Player.position - transform.position;
        // Get normalised direction to player.
        Vector3 direction = heading / heading.magnitude;
        // Get the axis to add torque.
        Vector3 rotationVector = Quaternion.AngleAxis(90, Vector3.up) * direction;

        Rigidbody.AddForce(direction * ForwardForce, ForceMode.Impulse);
        Rigidbody.AddForce(Vector3.up * UpwardForce, ForceMode.Impulse);
        Rigidbody.AddTorque(rotationVector * RotationForce, ForceMode.Impulse);

        CanMove = false;
    }
}
