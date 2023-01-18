using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstraint : MonoBehaviour
{   
    public Transform parent;
    void Update()
    {
        Vector3 currentEulerAngles = parent.eulerAngles;
        if(currentEulerAngles.x>20&&currentEulerAngles.x<180)
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 0, 20);
        else
        if(currentEulerAngles.x<340&&currentEulerAngles.x>=180)
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 350, 360);  
        

        if(currentEulerAngles.y>40&&currentEulerAngles.y<180)
        currentEulerAngles.y = Mathf.Clamp(currentEulerAngles.y, 0, 40);
        else
        if(currentEulerAngles.y<320&&currentEulerAngles.y>=180)
        currentEulerAngles.y = Mathf.Clamp(currentEulerAngles.y, 320, 360);

        currentEulerAngles.z = 0;
        Debug.Log(currentEulerAngles);
        transform.eulerAngles = currentEulerAngles;
    }
}
