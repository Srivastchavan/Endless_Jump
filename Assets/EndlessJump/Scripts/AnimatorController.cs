using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerController.isHit)
        {
            animator.SetBool("isHit", true);
        }
        if (playerController.startJump)
        {
            animator.SetBool("isStartJump", true);
        }
        else if (playerController.isJumping)
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isStartJump", false);
        }
    }
}
