using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody PlayerRigidbody;
    private float dashCooldown = 0;

    private void Update()
    {
        CheckDashState();
    }

    // This -kills- I mean moves the player.
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        PlayerController.Instance.MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // Stop diagonals from moving the player faster.
        if (PlayerController.Instance.MoveDirection.magnitude > 1)
        {
            PlayerController.Instance.MoveDirection.Normalize();
        }

        //PlayerRigidbody.velocity = Time.fixedDeltaTime * PlayerController.Instance.MoveSpeed * PlayerController.Instance.MoveDirection;

        // There's probably a way to have better and snappier movement, but the other method above says no to forces acting on the player.
        PlayerRigidbody.AddForce(Time.fixedDeltaTime * PlayerController.Instance.MoveSpeed * PlayerController.Instance.MoveDirection, ForceMode.Acceleration);
    }

    private void CheckDashState()
    {
        if (Input.GetKeyDown("left shift") && dashCooldown <= 0)
        {
            DashPlayer();
            dashCooldown = PlayerController.Instance.DashCooldown;
        }

        if (dashCooldown >= 0)
        {
            dashCooldown -= Time.deltaTime;
        }
    }

    //vroom vroom
    void DashPlayer()
    {
        PlayerRigidbody.AddForce(PlayerController.Instance.DashSpeed * PlayerController.Instance.MoveDirection, ForceMode.VelocityChange);
    }
}
