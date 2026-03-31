using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator playerAnimator;
    PlayerMovement playerMovement;

    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.moveInput.magnitude > 0.01f)
        {
            playerAnimator.SetBool("IsWalkingForward", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalkingForward", false);
        }
    }
}
