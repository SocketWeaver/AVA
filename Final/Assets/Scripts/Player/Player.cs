using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public bool Dead = false;
    Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Killed()
    {
        animator.SetBool("Dead", true);
        Dead = true;
    }
}
