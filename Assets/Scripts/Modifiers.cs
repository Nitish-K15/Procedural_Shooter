using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifiers : MonoBehaviour
{
    public static Modifiers instance;

    public float Damage = 0;
    public float Range = 0;
    public float FireDelay = 0;
    public float Speed = 0;
    public float Accuracy = 0;
    public int MagazineSize = 0;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetModifiers()
    {
        Damage = 0;
        Range = 0;
        FireDelay = 0;
        Speed = 0;
        Accuracy = 0;
    }
}
