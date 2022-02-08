using UnityEngine;

public class NFTGenerator : MonoBehaviour
{
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
