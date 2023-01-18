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
    public Transform target;
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
            if(allowInvoke && !rechargeInvoked && bulletsLeft<magazineSize && !reloading)
            {       
                rechargeInvoked = true;
                Invoke("AutoRecharge", autoRechargeTime);
            }
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        Vector3 direction = target.position - attackPoint.position + new Vector3( Random.Range(-spread, spread), Random.Range(-spread, spread),0);

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
