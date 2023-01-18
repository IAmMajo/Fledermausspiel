using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstraint : MonoBehaviour
{
    public Transform parent;
    void FixedUpdate()
    {
        
            Vector3 currentEulerAngles = parent.eulerAngles;
            if (currentEulerAngles.x > 100 && currentEulerAngles.x < 180)
                currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 0, 100);
            else
            if (currentEulerAngles.x < 350 && currentEulerAngles.x >= 180)
                currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 350, 360);

            currentEulerAngles.z = 0;
            transform.eulerAngles = currentEulerAngles;
        
    }
}
