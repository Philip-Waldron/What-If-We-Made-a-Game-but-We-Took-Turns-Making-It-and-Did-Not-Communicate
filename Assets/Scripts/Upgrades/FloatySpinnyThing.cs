using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatySpinnyThing : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    public float floatingAmplitude = 0.2f;
    public float frequency = 1f;
    public bool shouldSpin = true;
    public bool shouldFloat = true;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        if (shouldSpin)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        }

        if (shouldFloat)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * floatingAmplitude;

            transform.position = tempPos;
        }
    }
}
