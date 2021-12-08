﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FollowEnemy : Target
{
    public Transform player;
    public float Health;
    public NavMeshAgent agent;
    public Transform target;
    public BaseEnemy baseEnemy;
    private Animator anim;
    private bool isAttacking;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = baseEnemy.Health;
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > baseEnemy.AttackRange)
            ChasePlayer();
        else
            AttackPlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
        anim.SetTrigger("isRunning");
    }
    private void AttackPlayer()
    {
        isAttacking = true;
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        anim.SetTrigger("isAttacking");
    }

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
