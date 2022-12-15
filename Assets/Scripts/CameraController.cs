using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivityY;

    [SerializeField]
    float mouseSensitivityX;

    [SerializeField]
    int maxXRotation;

    [SerializeField]
    int minXRotation;

    Vector3 currentEulerAngles;

    // Start is called before the first frame update
    void Start()
    {
        currentEulerAngles = transform.eulerAngles;
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        currentEulerAngles += new Vector3(Input.GetAxis("Mouse Y") * this.mouseSensitivityY, Input.GetAxis("Mouse X") * this.mouseSensitivityX, 0);
        currentEulerAngles.x = Mathf.Clamp(currentEulerAngles.x, minXRotation, maxXRotation);
        transform.eulerAngles = currentEulerAngles;
    }
}
