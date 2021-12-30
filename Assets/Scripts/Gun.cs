using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public WeaponBase weaponBase;
    bool shooting, readyToShoot, reloading;
    int bulletsLeft,bulletsShot;
    public ParticleSystem muzzleFlash;
    public GameObject impactPoint;
    public Text text;
    private float finalDamage;
    private float finalRange;
    private int finalMagazineSize;
    private float finalFireDelay;
    private float finalAccuracy;

    private void Start()
    {
        bulletsLeft = weaponBase.magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        Values();
        text.text = bulletsLeft + " / " + finalMagazineSize;
    }

    private void MyInput()
    {
        if (weaponBase.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < finalMagazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = weaponBase.bulletsPerTap;
            Shoot();
        }
    }

    private void Values()
    {
        finalDamage = weaponBase.Damage + Modifiers.instance.Damage;
        finalRange = weaponBase.range + Modifiers.instance.Range;
        finalMagazineSize = weaponBase.magazineSize + Modifiers.instance.MagazineSize;
        finalFireDelay = weaponBase.timeBetweenShooting - Modifiers.instance.FireDelay;
        finalFireDelay = Mathf.Clamp(finalFireDelay, 0, 5);
        finalAccuracy = weaponBase.spread - Modifiers.instance.Accuracy;
        finalAccuracy = Mathf.Clamp(finalAccuracy, 0, 1);
    }


    private void Shoot()
    {
        readyToShoot = false;
        //Spread
        float x = Random.Range(-finalAccuracy, finalAccuracy);
        float y = Random.Range(-finalAccuracy, finalAccuracy);

        Vector3 direction = Camera.main.transform.forward + new Vector3(x, y, 0);
        muzzleFlash.Play();
        RaycastHit hit;
       if(Physics.Raycast(Camera.main.transform.position, direction,out hit,finalRange))
        {
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(finalDamage);
            }
            GameObject impactGO = Instantiate(impactPoint, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.3f);
        }

        Debug.Log(finalDamage);
        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", finalFireDelay);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", weaponBase.timeBetweeShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", weaponBase.reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = finalMagazineSize;
        reloading = false;
    }
}
