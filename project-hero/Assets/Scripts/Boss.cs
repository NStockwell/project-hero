using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float clawAttackDelay = 5f;  // Time delay for claw attack trigger
    [SerializeField] private float chompDelay = 10f;      // Time delay for chomp trigger
    [SerializeField] private Animator animator;          // Reference to the animator component

    private void Start()
    {
        // Start the activation loop
        InvokeRepeating("ActivateTriggers", 0f, chompDelay);
    }

    private void ActivateTriggers()
    {
        // Activate claw attack trigger after delay
        Invoke("ActivateClawAttackTrigger", clawAttackDelay);

        // Activate chomp trigger after delay
        Invoke("ActivateChompTrigger", chompDelay);
    }

    private void ActivateClawAttackTrigger()
    {
        animator.SetTrigger("claw_attack_trigger");
    }

    private void ActivateChompTrigger()
    {
        animator.SetTrigger("chomp_trigger");
    }
}
