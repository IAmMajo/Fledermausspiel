using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScriptPlayer : MonoBehaviour
{

    //Bullet
    public GameObject bullet;

    //Bullet Force
    public float shootForce;

    //Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShoots, autoRechargeTime;
    public int magazineSize, bulletsLeft;

    //bools
    bool readyToShoot, reloading, rechargeInvoked;

    //Refrence
    public Transform attackPoint;
    public Camera cam;

    //Graphics
    public GameObject muzzleFlash;
    

    public bool allowInvoke = true;

    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        if(bulletsLeft == 0 && !reloading)
        {
            Reload();
        }
    }

    void MyInput()
    {

       
        if (readyToShoot && Input.GetKey(KeyCode.Mouse0) && !reloading && bulletsLeft > 0)
        {
            
            Shoot();
        }
        else
        {
            if(allowInvoke && !rechargeInvoked && bulletsLeft<magazineSize)
            {       
                 Debug.Log(bulletsLeft);
                rechargeInvoked = true;
                Invoke("AutoRecharge", autoRechargeTime);
            }
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        //ray cast towards the middle of the screen to find the point we are shooting at
        //(Needs to be changed to shoot in the direction the weapon is facing)
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point; //Point hit by the Ray
        }
        else
        {
            targetPoint = ray.GetPoint(75);//Point far away from Player
        }

        Vector3 direction = targetPoint - attackPoint.position + new Vector3( Random.Range(-spread, spread), Random.Range(-spread, spread),0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = direction.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        if(allowInvoke)
        {
            Invoke("ResetShoot", timeBetweenShooting);
        }

    }

    void ResetShoot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    void AutoRecharge()
    {
        bulletsLeft++;
        rechargeInvoked = false;    
    }

    //reload is handeld as a form of overheating in this
    void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
