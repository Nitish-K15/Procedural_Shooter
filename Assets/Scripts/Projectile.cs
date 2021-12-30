﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public BaseEnemy baseEnemy;
    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
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
