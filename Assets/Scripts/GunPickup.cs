using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : Interactable
{
    public GameObject gunContainer;
    public Gun gunscript;
    public delegate void GunPicked();
    public static event GunPicked ChangeGun;

    private void Start()
    {
        gunscript.enabled = false;
        gunContainer = GameObject.FindWithTag("GunHolder");
    }


    public override void Interact()
    {
        transform.SetParent(gunContainer.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        GetComponent<BoxCollider>().enabled = false;
        gunscript.enabled = true;
        WeaponSwitching.selectedWeapon++;
        ChangeGun();
        Invoke(nameof(DisableScipt), 0.8f);
    }

    void DisableScipt()
    {
        this.enabled = false;
    }
}
