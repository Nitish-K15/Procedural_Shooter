using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseEnemy : ScriptableObject
{
    public float Health;
    public float Damage;
    public float AttackRange;
    public float timeBetweenAttacks;
}
