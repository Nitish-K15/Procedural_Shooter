using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public int attackRange = 3;
    private Transform player;
    private Animator anim;
    private int attackType;
    private Vector3 direction;

 void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!anim.GetBool("isDead"))
        {
            //Find the direction
            direction = player.position - transform.position;

            if (direction.magnitude > 3f)
            {
                transform.LookAt(player);
                transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
                anim.SetBool("isRunning", true);
                attackType = 0;
                anim.SetInteger("isAttacking", 0);
            }
            else
            {
                attackType = Random.Range(1,attackRange + 1);
                anim.SetBool("isRunning", false);
                anim.SetInteger("isAttacking", attackType);
            }

            Debug.Log(direction.magnitude);
        }
    }
}
