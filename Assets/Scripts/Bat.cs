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
    Vector3 wingBeatVelocity;

    int wingBeatDelay;
    int wingBeatDelayCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.wingBeatDelay = (int)(1 / (Time.fixedDeltaTime * this.wingBeatsPerSecond));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(flyingKey))
        {
            if (this.wingBeatDelayCounter == this.wingBeatDelay)
            {
                GetComponent<Rigidbody>().velocity += wingBeatVelocity;
                this.wingBeatDelayCounter = 0;
            }
            else
            {
                this.wingBeatDelayCounter++;
            }
            Debug.Log(this.wingBeatDelayCounter);
        }
        else
        {
            this.wingBeatDelayCounter = 0;
        }
    }
}
