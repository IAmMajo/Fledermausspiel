using UnityEngine;

public class Bat : MonoBehaviour
{

  [SerializeField]
  KeyCode[] keys;

  [SerializeField]
  Vector3[] velocities;

  [SerializeField]
  float rotFactor;

  [SerializeField]
  GameObject cam_parent;

  private Rigidbody comp_rb;

  // Start is called before the first frame update
  void Start()
  {
    this.comp_rb = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
    for (int i = 0; i < keys.Length; i++)
    {
      if (Input.GetKey(this.keys[i]))
      {
        this.comp_rb.AddRelativeForce(this.velocities[i], ForceMode.VelocityChange);
      }
    }

    if (!Input.GetMouseButton(1))
    {
      //places the transform to the transform of the camera parent
      this.transform.rotation = Quaternion.Slerp(
        this.transform.rotation,
        new Quaternion( 0,this.cam_parent.transform.rotation.y,
        0,this.cam_parent.transform.rotation.w),
        this.rotFactor
      );
    }
    
  }
}
