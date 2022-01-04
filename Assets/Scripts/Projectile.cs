using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public BaseEnemy baseEnemy;
    public GameObject explosion;
    public bool isFire;
    public AudioClip blast;

    //private void Awake()
    //{
    //    if (isFire)
    //    {
    //        GetComponent<Rigidbody>().AddForce(transform.forward * 10f, ForceMode.Impulse);
    //        GetComponent<Rigidbody>().AddForce(transform.up * 3f, ForceMode.Impulse);
    //    }
    //    else
    //    {
    //        GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        SoundManager.Instance.Play(blast);
        if (explosion)
        {
            GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(e, 1f);
        }
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FirstPersonController>().TakeDamage(baseEnemy.Damage);
        }
        Destroy(this.gameObject);
    }
}
