using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnimator;
    PlayerMovement playerMovement;
    bool IsJumping = false;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.moveInput.magnitude > 0.01f)
        {
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        if (playerMovement.IsJumping && !IsJumping)
        {
            playerAnimator.SetTrigger("IsJumping");
            IsJumping = true;
            Invoke(nameof(ResetJump), 0.5f);
        }
    }
    void ResetJump()
    {
        IsJumping = false;
    }
}
