using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    public Rigidbody Rigidbody;
    // Force towards the player.
    public float ForwardForce;
    // Force into the sky if needed.
    public float UpwardForce;
    // Some rotational force if needed.
    public float RotationForce;

    // Range of move rates for _moveTime.
    public float MinMoveRate;
    public float MaxMoveRate;

    // Time to wait until move.
    private float _moveTime;
    private float _currentTime;

    // Time's up lets move.
    protected bool CanMove = true;

    protected Transform Player;

    private void Start()
    {
        Player = PlayerController.Instance.transform;

        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        // Set a random interval that agent will move for next move.
        _moveTime = Random.Range(MinMoveRate, MaxMoveRate);
    }

    private void Update()
    {
        UpdateMoveTimer();

        if (CanMove)
        {
            Move();
        }
    }

    // Manage the internal clock for the movement rate.
    private void UpdateMoveTimer()
    {
        if (CanMove)
        {
            _currentTime = 0;
        }

        if (!CanMove)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _moveTime)
            {
                CanMove = true;
                _currentTime = 0;

                // Set a new time that agent will move.
                _moveTime = Random.Range(MinMoveRate, MaxMoveRate);
            }
        }
    }

    // Specific enemies can override this.
    protected abstract void Move();
}
