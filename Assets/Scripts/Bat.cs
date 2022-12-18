using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField]
    KeyCode flyingKey;

    [SerializeField]
    KeyCode breakKey;

    [SerializeField]
    float wingBeatsPerSecond;

    [SerializeField]
    float wingBeatVelocity;

    [SerializeField]
    float speedFactor;

    [SerializeField]
    float dragFactor;

    

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
                comp_rb.AddForce(new Vector3(transform.forward.x,wingBeatVelocity,transform.forward.z), ForceMode.VelocityChange);
                this.wingBeatDelayCounter = 0;
            }
            else
            {
                this.wingBeatDelayCounter++;
            }
        }
        else if(Input.GetKey(breakKey)) 
        {
            comp_rb.AddForce(comp_rb.velocity/3f*-1, ForceMode.Force);
        }
        else if(transform.eulerAngles.x>3 && transform.eulerAngles.x<21) //when you look down you should get faster/ values found trough log
        {
            comp_rb.AddForce(transform.forward*transform.eulerAngles.x*speedFactor, ForceMode.Acceleration);
        }
        else
        {
            //loss of velocity when you look up
            comp_rb.AddForce(-1*(comp_rb.velocity*dragFactor)+transform.forward*dragFactor, ForceMode.Force);
            this.wingBeatDelayCounter = 0;
        }
        //places the transform to te transform of the camera parent(child with index 3) 
        if (Input.GetMouseButton(1))
        {
            //as to be true otherwise to prevent random tumbeling
            comp_rb.freezeRotation = false;
            //TO DO: make smooth
            transform.eulerAngles += transform.GetChild(3).eulerAngles-transform.eulerAngles;
        }
        else
        {
            comp_rb.freezeRotation = true;
        }
        

        
            
        
    }
    
}
