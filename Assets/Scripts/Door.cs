using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator dooranim;

    private void Start()
    {
        dooranim = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        EnemyCount.Roomcleared += DoorOpen;
        PlayerEnter.RoomEntered += DoorClosed;
    }

    private void OnDisable()
    {
        EnemyCount.Roomcleared -= DoorOpen;
        PlayerEnter.RoomEntered -= DoorClosed;
    }

    void DoorOpen()
    {
        dooranim.Play("DoorOpening");
        Debug.Log("Open");
    }

    void DoorClosed()
    {
        dooranim.Play("DoorClosing");
        Debug.Log("Closed");
    }
}
