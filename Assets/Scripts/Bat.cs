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

    [SerializeField]
    float breakFactor;

    [SerializeField]
    float rotFactor;

    int wingBeatDelay;
    int wingBeatDelayCounter = 0;

    [SerializeField]
    GameObject cam_parent;

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
                comp_rb.AddRelativeForce(new Vector3(0,wingBeatVelocity,0.5f), ForceMode.VelocityChange);
                this.wingBeatDelayCounter = 0;
            }
            else
            {
                this.wingBeatDelayCounter++;
            }
        }
        else if(Input.GetKey(breakKey)) 
        {
            if(comp_rb.velocity.magnitude>1.3f)
            {
                 comp_rb.velocity -= new Vector3(comp_rb.velocity.x*breakFactor,comp_rb.velocity.y*breakFactor,comp_rb.velocity.z*breakFactor);
            }
            else
            {
                comp_rb.velocity = new Vector3(0,0,0);
            }
        }
        else if(transform.eulerAngles.x>=0 && transform.eulerAngles.x<21)//values that check if you look down
        {
            //when you look down you get faster
            comp_rb.AddRelativeForce( new Vector3(0,0,transform.eulerAngles.x*speedFactor), ForceMode.Acceleration);
        }
        else if(comp_rb.velocity.magnitude>15)//checks if the velocity is still high enough to keep momentum
        {
            //keep of velocity
            comp_rb.AddRelativeForce(
                new Vector3(
                    0,
                    0,
                    comp_rb.velocity.magnitude*dragFactor
                ),
                 ForceMode.Force);  
        }
        else
        {
            this.wingBeatDelayCounter = 0;
        }

        if (Input.GetMouseButton(1))
        {
            
            //has to be true otherwise to prevent random tumbeling
            comp_rb.freezeRotation = false;
            //places the transform to te transform of the camera parent(child with index 3) 

            transform.rotation = Quaternion.Slerp(transform.rotation, cam_parent.transform.rotation, rotFactor);
        }
        else
        {
            comp_rb.freezeRotation = true;
        }    
    }
    
}
