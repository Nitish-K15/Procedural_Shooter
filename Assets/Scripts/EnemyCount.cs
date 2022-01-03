using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public int count;
    public GameObject[] arr = new GameObject[2];
    public GameObject spawnPoint;
    public delegate void DoorAction();
    public static event DoorAction Roomcleared;

    public void CheckClear()
    {
        count--;
        if (count <= 0)
        {
            Roomcleared();
            int num = Random.Range(0, 2);
            Debug.Log(num);
            Instantiate(arr[num], spawnPoint.transform);
        }
    }
}
