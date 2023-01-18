using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConstraint : MonoBehaviour
{
    void Update()
    {
        Vector3 currentEulerAngles = transform.eulerAngles;
        Debug.Log(currentEulerAngles);
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, 50, 360);
        transform.eulerAngles = currentEulerAngles;
    }
}
