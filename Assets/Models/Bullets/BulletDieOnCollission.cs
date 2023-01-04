using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDieOnCollission : MonoBehaviour
{  

    void Start()
    {
        //Bullets only fly 10seconds
        Destroy(gameObject, 10f);  
    }
    void OnCollisionEnter(Collision collision)
    {
        //Bullets get destroyed when they hit someting
        Destroy(gameObject);
    }
}
