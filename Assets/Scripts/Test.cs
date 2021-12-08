using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Target
{
    float Health = 10;
    public override void TakeDamage(float amount)
    {
        Health = Health - amount;
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            GetComponentInParent<EnemyCount>().CheckClear();
        }
    }
}
