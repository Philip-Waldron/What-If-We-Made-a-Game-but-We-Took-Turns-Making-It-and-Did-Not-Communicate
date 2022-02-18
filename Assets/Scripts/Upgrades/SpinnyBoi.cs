using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpinnyBoi : MonoBehaviour
{
    [Header("I've got the Woozies")]
    public float spinnySpeed = 200.0f;

    [Header("Can't touch this")]
    private bool used = false;

    void Update()
    {
        DoASpinny();
        UsedRemover();
    }

    void DoASpinny()
    {
        transform.Rotate(0f, spinnySpeed * Time.deltaTime, 0f, Space.Self);
    }

    void UsedRemover()
    {
        if(used)
        {
            // was gonna do a flyaway thing here but *shrug*
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    protected abstract IEnumerator Pickup(Collider player);

    public void DisableMeDaddy()
    {
        GetComponent<Collider>().enabled = false;
        transform.position -= new Vector3(0f, 10f, 0f); // I don't know how to remove the mesh on this one so fuck it
    }

    public void ImBackBaby()
    {
        transform.position += new Vector3(0f, 10f, 0f);
        GetComponent<Collider>().enabled = true;
    }
}
