using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStones : Interactable
{
    public bool damage, range, speed, firedelay, accuracy;
    private bool hasActivated;
    public override void Interact()
    {
        if (FirstPersonController.orbsCollected > 0 && !hasActivated)
        {
            if (damage)
            {
                Modifiers.instance.Damage++;
                Modifiers.instance.Damage = Mathf.Clamp(Modifiers.instance.Damage, 0, 50);
            }

            if (range)
            {
                Modifiers.instance.Range++;
                Modifiers.instance.Range = Mathf.Clamp(Modifiers.instance.Range, 0, 50);
            }

            if (speed)
            {
                Modifiers.instance.Speed++;
                Modifiers.instance.Speed = Mathf.Clamp(Modifiers.instance.Speed, 0, 10);
            }

            if (firedelay)
            {
                Modifiers.instance.FireDelay += 0.1f;
            }

            if (accuracy)
            {
                Modifiers.instance.Accuracy += 0.1f;
            }

            FirstPersonController.orbsCollected--;
            hasActivated = true;
            gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
