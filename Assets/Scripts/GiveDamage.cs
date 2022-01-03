using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveDamage : MonoBehaviour
{
    public BaseEnemy baseEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<FirstPersonController>().TakeDamage(baseEnemy.Damage);
            Debug.Log("called");
        }
    }
}
