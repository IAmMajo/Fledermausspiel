using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField]
    KeyCode flyingKey;

    [SerializeField]
    float wingBeatsPerSecond;

    [SerializeField]
    float wingBeatVelocity;

    [SerializeField]
    float rotationFactor;

    

    int wingBeatDelay;
    int wingBeatDelayCounter = 0;

    Rigidbody comp_rb;
    // Start is called before the first frame update
    void Start()
    {
        this.wingBeatDelay = (int)(1 / (Time.fixedDeltaTime * this.wingBeatsPerSecond));
        comp_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(flyingKey))
        {
            if (this.wingBeatDelayCounter == this.wingBeatDelay)
            {
                comp_rb.AddForce(new Vector3(0,wingBeatVelocity,0)+transform.forward, ForceMode.VelocityChange);
                this.wingBeatDelayCounter = 0;
            }
            else
            {
                this.wingBeatDelayCounter++;
            }
        }
        else
        {
            this.wingBeatDelayCounter = 0;
        }
        //places the transform to te transform of the camera parent(child with index 3) 
        if (Input.GetMouseButton(1))
        {
            //TO DO: make smooth
            transform.eulerAngles += transform.GetChild(3).eulerAngles-transform.eulerAngles;
        }

        //when you look down you should get faster/ values found trough log
        if(transform.eulerAngles.x>15 && transform.eulerAngles.x<21){
            comp_rb.AddForce(transform.forward*transform.eulerAngles.x*5, ForceMode.Acceleration);
            Debug.Log(true);
        }
            
        
    }
    
}
