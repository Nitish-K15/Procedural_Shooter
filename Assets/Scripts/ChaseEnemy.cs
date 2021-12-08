using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{
    protected Transform[] targets = new Transform[4];
    int j = 1;
    public FollowEnemy[] childs;
    int k = 0;
    private void Awake()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = GameObject.Find("ChasePoint" + j).transform;
            j++;
        }
        childs = gameObject.GetComponentsInChildren<FollowEnemy>();
    }

    private void OnEnable()
    {
        foreach(FollowEnemy child in childs)
        {
            child.target = targets[k];
            k++;
        }
    }
}
