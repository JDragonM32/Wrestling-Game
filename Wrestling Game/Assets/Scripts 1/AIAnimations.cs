using UnityEngine;
using UnityEngine.AI;

public class AIAnimations : MonoBehaviour
{
    Animator aiAnimator;
    NavMeshAgent agent;

    bool IsAttacking = false;
    void Start()
    {
        aiAnimator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.velocity.magnitude > 0.01f)
        {
            aiAnimator.SetBool("IsWalking", true);
            Debug.Log("Walking");
        } else
        {
            aiAnimator.SetBool("IsWalking", false);
        }
        if (IsAttacking == true) {
            aiAnimator.SetTrigger("IsAttacking");
            IsAttacking = true;
            Invoke(nameof(ResetAttack), 0.1f);
        }
    }

    void ResetAttack()
    {
        IsAttacking = false;
    }
}
