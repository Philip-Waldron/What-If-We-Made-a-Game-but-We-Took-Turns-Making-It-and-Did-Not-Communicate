using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody PlayerRigidbody;

    // This -kills- I mean moves the player.
    void FixedUpdate()
    {
        PlayerController.Instance.MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // Stop diagonals from moving the player faster.
        if (PlayerController.Instance.MoveDirection.magnitude > 1)
        {
            PlayerController.Instance.MoveDirection.Normalize();
        }

        PlayerRigidbody.velocity = Time.fixedDeltaTime * PlayerController.Instance.MoveSpeed * PlayerController.Instance.MoveDirection;
    }
}
