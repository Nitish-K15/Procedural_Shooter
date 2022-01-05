using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnter : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction RoomEntered;
    private bool Entered;
    public bool isItemRoom;


    private void OnTriggerEnter(Collider other)
    {
        if (!Entered)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Entered = true;
                if(!isItemRoom)
                    RoomEntered();
            }
        }
    }
}
