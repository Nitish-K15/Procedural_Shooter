using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponBase : ScriptableObject
{
    public float Damage;
    public float range;
    public float spread;
    public float reloadTime;
    public float timeBetweenShooting;
    public int magazineSize;
    public bool allowButtonHold;
    public int bulletsPerTap;
    public float timeBetweeShots;
}
