using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class bigBoyMovement : EnemyMovement
    {
        protected override void Move()
        {
            Rigidbody.AddForce(Vector3.up * UpwardForce, ForceMode.Impulse);
            Rigidbody.AddTorque(Vector3.up * RotationForce, ForceMode.Impulse);
            StartCoroutine(DelayedMove(3f));

            CanMove = false;
        }

        // After a delay, fire towards the player angry birds style.
        private IEnumerator DelayedMove(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            // Point to player.
            Vector3 heading = Player.position - transform.position;
            // Get normalised direction to player.
            Vector3 direction = heading / heading.magnitude;

            Rigidbody.AddForce(direction * ForwardForce, ForceMode.Impulse);
        }
    }
}