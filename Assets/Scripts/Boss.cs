using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
 
    public int attackRange = 3;
    private Transform player;
    private Animator anim;
    private int attackType;
    private bool setForce = false;
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
            direction = player.position -transform.position;

             if (direction.magnitude > 2f)
            {
                transform.LookAt(new Vector3(player.position.x, 0, player.position.z));
            }
            if (!anim.GetBool("isRunning") && attackType == 0)
            {
                attackType = Random.Range(1,attackRange + 1);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!anim.GetBool("isDead"))
        {
            if (direction.magnitude > 3f)
            {
                anim.SetBool("isRunning",true);
                attackType = 0;
                anim.SetInteger("isAttacking",0);
            }

            else
            {
                anim.SetBool("isRunning",false);
                anim.SetInteger("isAttacking",attackType);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime< 0.1f)
                {
                    setForce = true;
                }
                if (setForce && anim.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.5f)
                {
                        player.gameObject.GetComponent<FirstPersonController>().ApplyImpact(6f,10f);
                        if (direction.magnitude <=3f)
                        {
                        Debug.Log("Damage Player");
                        }
                        setForce = false;
                }
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetInteger("isAttacking", 0);
        }
    }

}