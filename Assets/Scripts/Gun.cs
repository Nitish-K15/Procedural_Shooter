using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject impactPoint;
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
       if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out hit,range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(Damage);
            }

            GameObject impactGO = Instantiate(impactPoint, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

    }
}
