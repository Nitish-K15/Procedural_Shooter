using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public int count;
    
    public void CheckClear()
    {
        count--;
        if (count <= 0)
            Debug.Log("Clear");
    }
}
