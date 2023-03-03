using System;
using System.Collections;
using InputSystem;
using UnityEngine;
using Action = System.Action;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float clawAttackDelay = 5f;  // Time delay for claw attack trigger
    [SerializeField] private float chompDelay = 10f;      // Time delay for chomp trigger
    [SerializeField] private Animator animator;          // Reference to the animator component
    [SerializeField] private ActionSystem actionSystem;          // Reference to the animator component

    [SerializeField] private BossDamageSystem _bossDamageSystem; // Reference to the Boss Attack System which defines the attack effects

    private bool isImmune;
    private void Start()
    {
        // Start the activation loop
        InvokeRepeating("ActivateTriggers", 0f, chompDelay);
    }

    private void OnEnable()
    {
        actionSystem.OnActionTaken += ActionTaken;
    }

    private void OnDisable()
    {
        actionSystem.OnActionTaken -= ActionTaken;
    }

    private void ActionTaken(InputSystem.Action action)
    {
        if (action == InputSystem.Action.Attack)
        {
            SufferDamage();
        }
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
        isImmune = true;
        animator.SetTrigger("claw_attack_trigger");
        _bossDamageSystem.PerformAttack(BossDamageSystem.BossDamageType.LargeDamage);
        StartCoroutine(StopBeingImmuneAfterDelay(1.0f));
    }

    private void ActivateChompTrigger()
    {
        isImmune = true;
        animator.SetTrigger("chomp_trigger");
        _bossDamageSystem.PerformAttack(BossDamageSystem.BossDamageType.SmallDamage);
        StartCoroutine(StopBeingImmuneAfterDelay(1.0f));
    }

    IEnumerator StopBeingImmuneAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        isImmune = false;
    }

    public void SufferDamage()
    {
        if (!isImmune)
        {
            animator.SetTrigger("suffer_damage_trigger");
        }
    }
}
