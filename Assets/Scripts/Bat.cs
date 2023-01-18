using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int health;

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
              new Quaternion(0, this.cam_parent.transform.rotation.y,
              0, this.cam_parent.transform.rotation.w),
              this.rotFactor
            );
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            TakeDamage(1);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyPlayer), 0.5f);
    }

    private void DestroyPlayer()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("DeathScreen");
    }
}
