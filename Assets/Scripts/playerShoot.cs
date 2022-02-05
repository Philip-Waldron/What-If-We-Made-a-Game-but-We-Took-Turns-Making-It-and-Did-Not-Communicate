using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    
    public Transform GlassEm;
    public GameObject Glass;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            gameObject.transform.Rotate(new Vector3(0,180,0));
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            gameObject.transform.Rotate(new Vector3(0,90,0));
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            gameObject.transform.Rotate(new Vector3(0,270,0));
            Shoot();
        }

        if(Input.GetKeyUp(KeyCode.K))
        {
            transform.rotation =Quaternion.Euler(0,90, 0);
            Shoot();
        }
        if(Input.GetKeyUp(KeyCode.I)||Input.GetKeyUp(KeyCode.J)||Input.GetKeyUp(KeyCode.L))
        {
           
            transform.rotation =Quaternion.Euler(0,90, 0);
        }
    }

    void Shoot()
    {
        Instantiate(Glass, GlassEm.position, GlassEm.rotation);
    }
}
