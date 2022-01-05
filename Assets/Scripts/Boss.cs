using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Target
{
 
    public int attackRange = 3;
    private Transform player;
    private Animator anim;
    private int attackType;
    private bool setForce = false;
    public float Health = 100f;
    private Vector3 direction;
    public GameObject treasure;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
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
                if (setForce && anim.GetCurrentAnimatorStateInfo(0).normalizedTime> 0.5f && !anim.GetBool("isRunning"))
                {
                    if (attackType == 2 && direction.magnitude > 6f)
                    {
                        player.gameObject.GetComponent<FirstPersonController>().ApplyImpact(6f, 10);
                    }
                    else
                    {
                        if (direction.magnitude <= 3f)
                        {
                            player.gameObject.GetComponent<FirstPersonController>().ApplyImpact(6f, 10);
                        }
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

    public override void TakeDamage(float amount)
    {
        if (!anim.GetBool("isDead"))
        {
            Health = Health - amount;
            if (Health <= 0)
            {
                anim.SetBool("isDead", true);
                StartCoroutine(Dying());
            }
        }
    }

    IEnumerator Dying()
    {
        yield return new WaitForSeconds(0.5f);
        treasure.SetActive(true);
    }

}