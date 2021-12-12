﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ShootingEnemy :Target
{
    public BaseEnemy Base;
    public Transform target,FireSpawn;
    private NavMeshAgent agent;
    public LayerMask whatIsPlayer;
    public GameObject projectile;
    public bool  playerInAttackRange;
    private bool alreadyAttacked;
    private float Health;
    private Animator anim;
    void Start()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Health = Base.Health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) > Base.AttackRange)
            playerInAttackRange = false;
        else
            playerInAttackRange = true;
        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        anim.SetTrigger("isRunning");
        anim.SetBool("Run",true);
        anim.SetBool("Attack", false);
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);
        anim.SetBool("Attack", true);
        anim.SetBool("Run", false);
        anim.SetTrigger("isAttacking");
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);

        if (!alreadyAttacked)
        {
            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), Base.timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public override void TakeDamage(float amount)
    {
        Health = Health - amount;
        if (Health <= 0)
            Destroy(this.gameObject);
    }

    public void ShootFire()
    {
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, FireSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
            rb.AddForce(transform.up * 3f, ForceMode.Impulse);
        }
    }

    public void ShootLightning()
    {
        Rigidbody rb = Instantiate(projectile, FireSpawn.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce((target.transform.position - this.transform.position).normalized*10f, ForceMode.Impulse);
    }
}
