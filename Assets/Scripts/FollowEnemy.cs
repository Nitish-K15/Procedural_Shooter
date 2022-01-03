using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FollowEnemy : Target
{
    public Transform player;
    public float Health;
    public NavMeshAgent agent;
    public BaseEnemy baseEnemy;
    private Animator anim;
    private FirstPersonController _player;
    private bool isDead;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Health = baseEnemy.Health;
        anim = GetComponent<Animator>();
        _player = player.GetComponent<FirstPersonController>();;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(player.position, transform.position) >= baseEnemy.AttackRange)
                Pursue();
            else
                AttackPlayer();
        }
    }

    void Pursue()
    {
        anim.SetTrigger("isRunning");
        anim.SetBool("Run", true);
        anim.SetBool("Attack", false);

        Vector3 targetDir = player.transform.position - this.transform.position;
        float relativeHeading = Vector3.Angle(this.transform.forward, this.transform.TransformVector(player.transform.forward));
        float toTarget = Vector3.Angle(this.transform.forward, this.transform.TransformVector(targetDir));

        if((toTarget > 90 && relativeHeading < 20) || _player.currentSpeed < 0.01f)
        {
            agent.SetDestination(player.transform.position);
            return;
        }

        float lookAhead = targetDir.magnitude / (agent.speed + _player.currentSpeed);
        agent.SetDestination(player.transform.position + player.transform.forward * lookAhead * 2);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", true);
        transform.LookAt(player);
        transform.eulerAngles = new Vector3(0f, transform.rotation.eulerAngles.y, 0f);
        anim.SetTrigger("isAttacking");
    }


    public override void TakeDamage(float amount)
    {
        if (!isDead)
        {
            Health = Health - amount;
            if (Health <= 0)
            {
                isDead = true;
                StartCoroutine(Dying());
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        gameObject.GetComponent<FirstPersonController>().TakeDamage(baseEnemy.Damage);
    //    }
    //}

    IEnumerator Dying()
    {
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        anim.SetBool("isDead", true);
        GetComponentInParent<EnemyCount>().CheckClear();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
