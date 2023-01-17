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

    if (Input.GetMouseButton(1))
    {

      //has to be true otherwise to prevent random tumbeling
      this.comp_rb.freezeRotation = false;

      //places the transform to the transform of the camera parent
      this.transform.rotation = Quaternion.Slerp(
        this.transform.rotation,
        this.cam_parent.transform.rotation,
        this.rotFactor
      );
    }
    else
    {
      this.comp_rb.freezeRotation = true;
    }
  }
}
