using UnityEngine;

public class DespawnOverTime : MonoBehaviour
{
    public Rigidbody Rigidbody;

    public float StartSink;
    public float TargetYSinkValue;
    public float DespawnTime;

    private float _timeAlive;
    private float _timeSinking;
    private bool _startedSinking;
    private Vector3 _startSinkPosition;

    // Mayhaps someone forgot to set the rigid body in the inspector?
    private void Start()
    {
        if (Rigidbody == null)
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    }

    // Sink the object over time after a delay, setting the rigid body to kinematic to stop other objects from affecting it.
    // Despawn the object after a delay.
    private void Update()
    {
        _timeAlive += Time.deltaTime;

        if (!_startedSinking)
        {
            if (_timeAlive > StartSink)
            {
                _startedSinking = true;
                _startSinkPosition = transform.position;
                Rigidbody.isKinematic = true;
            }
        }

        if (_startedSinking)
        {
            _timeSinking += Time.deltaTime / (DespawnTime - StartSink);
            transform.position = Vector3.Lerp(_startSinkPosition, new Vector3(_startSinkPosition.x, TargetYSinkValue, _startSinkPosition.z), _timeSinking);
        }

        if (_timeAlive >= DespawnTime)
        {
            Destroy(gameObject);
        }
    }
}
