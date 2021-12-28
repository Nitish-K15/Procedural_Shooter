using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual bool isActive(Transform player)
    {
        if (Vector3.Distance(player.position, this.gameObject.transform.position) <= 5)
        {
            return true;
        }
        else
            return false;
    }

    public virtual void Interact() { }
}
