using System.Collections;
using System.Collections.Generic;
using SWNetwork;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    public enum BotState { Wandering, Chasing, Dead };

    public Transform Target;
    public NavMeshAgent Agent;
    public int Damage = 10;
    public float AttackRange = 2f;
    public float StateInterval = 1f;

    RaycastHit hit;
    Animator animator;
    CapsuleCollider capsuleCollider;
    BotState state = BotState.Wandering;
    float timer;
    Player targetPlayer;

    NetworkID networkID;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        networkID = GetComponent<NetworkID>();
    }

    void Update()
    {
        Agent.enabled = networkID.IsMine;

        if (networkID.IsMine)
        {
            switch (state)
            {
                case BotState.Wandering:
                    {
                        Wandering();
                        break;
                    }
                case BotState.Chasing:
                    {
                        Chasing();
                        break;
                    }
                case BotState.Dead:
                    {
                        break;
                    }
            }
        }
    }

    void Wandering()
    {
        timer += Time.deltaTime;

        if (timer < StateInterval)
        {
            return;
        }
        else
        {
            timer = 0;
        }

        PlayerHealth[] phs = FindObjectsOfType<PlayerHealth>();
        foreach (PlayerHealth ph in phs)
        {
            targetPlayer = ph.GetComponent<Player>();

            if (targetPlayer.Dead)
            {
                continue;
            }

            Target = ph.transform;
            state = BotState.Chasing;
        }
    }

    void Chasing()
    {
        if (Vector3.Distance(Target.position, transform.position) < AttackRange)
        {
            if (targetPlayer != null && targetPlayer.Dead)
            {
                Target = null;
                state = BotState.Wandering;
                animator.SetBool("Attacking", false);
                Agent.isStopped = false;
            }
            else
            {
                animator.SetBool("Attacking", true);
                transform.LookAt(Target);
                Agent.isStopped = true;
            }
        }
        else
        {
            if (targetPlayer != null && targetPlayer.Dead)
            {
                Target = null;
                state = BotState.Wandering;
                animator.SetBool("Attacking", false);
                Agent.isStopped = false;
            }
            else
            {
                Agent.destination = Target.position;
                animator.SetBool("Attacking", false);
                Agent.isStopped = false;
            }
        }
    }

    public void DealDamage()
    {
        if(Target != null)
        {
            PlayerHealth playerHealth = Target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GotHit(Damage);
            }
        }
    }

    public void Killed()
    {
        if (state != BotState.Dead)
        {
            state = BotState.Dead;
            Agent.isStopped = true;
            animator.SetBool("Dead", true);
            capsuleCollider.enabled = false;

            GameSceneManager gameSceneManager = FindObjectOfType<GameSceneManager>();
            gameSceneManager.PlayerScored("Local Player");
            Destroy(gameObject, 3);
        }
    }
}
