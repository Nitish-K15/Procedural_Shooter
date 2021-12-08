using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    public GameObject[] walls; //0-Up,1-Down,2-Right,3-Left
    public GameObject[] doors;
    //public GameObject CoinSpawn;
    public int EnemyCount;
    

    private void Change()
    {
        if (EnemyCount == 0)
            Debug.Log("Cleared");
        else
            EnemyCount = EnemyCount - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UpdateRoom(bool[] status)
    {
        for(int i = 0;i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
