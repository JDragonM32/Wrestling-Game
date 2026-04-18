using UnityEngine;
using UnityEngine.AI;

public class AIAnimations : MonoBehaviour
{
    Animator aiAnimator;
    NavMeshAgent agent;

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
        }
        else
        {
            aiAnimator.SetBool("IsWalking", false);
        }
    }
}
