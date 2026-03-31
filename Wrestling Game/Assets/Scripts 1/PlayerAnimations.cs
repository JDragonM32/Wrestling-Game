using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    void Start()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement.moveInput.magnitude > 0.01f)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
}
