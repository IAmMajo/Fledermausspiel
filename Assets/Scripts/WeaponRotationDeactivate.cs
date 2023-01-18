using UnityEngine;
using UnityEngine.Animations;

public class WeaponRotationDeactivate : MonoBehaviour
{
    public RotationConstraint rotRestraint;
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            rotRestraint.constraintActive = false;
        }
        else
        {
            rotRestraint.constraintActive = true;
        }
    }
}
