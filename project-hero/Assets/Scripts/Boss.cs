using System;
using InputSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Action = InputSystem.Action;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float clawAttackDelay = 5f;        // Time delay for claw attack trigger
    [SerializeField] private float chompDelay = 10f;            // Time delay for chomp trigger
    [SerializeField] private float swipeAnimationDelay = 1f;    // Time delay for the player swipe animation
    [SerializeField] private Animator animator;                 // Reference to the animator component

    [SerializeField] private float timeToWaitAfterSwipe = 0.5f;
    [SerializeField] private float rotationAnimationDuration = 0.5f;

    private Vector3 _initialPlayerPosition;
    private Vector3 _finalPlayerPosition;

    private bool _canRotateBoss = false;
    private bool _hasRotationInQueue = false;

    private float _elapsedRotationTime;
    
    [SerializeField] private BossDamageSystem bossDamageSystem; // Reference to the Boss Attack System which defines the attack effects
	[SerializeField] private ActionSystem actionSystem;

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

    public void SetPlayerPositionsForRotation(Vector3 initialPlayerPosition, Vector3 finalPlayerPosition)
    {
        if (!_hasRotationInQueue)
        {
            _initialPlayerPosition = initialPlayerPosition;
            _elapsedRotationTime = 0f;
            
            Invoke("FaceBossToPlayer", swipeAnimationDelay);
            _hasRotationInQueue = true;
        }

        _finalPlayerPosition = finalPlayerPosition;
    }

    private void FixedUpdate()
    {
        if (_canRotateBoss)
        {
            _elapsedRotationTime += Time.fixedDeltaTime;
            transform.LookAt(Vector3.Lerp(_initialPlayerPosition, _finalPlayerPosition, _elapsedRotationTime / rotationAnimationDuration));

            if (_elapsedRotationTime >= rotationAnimationDuration)
            {
                _canRotateBoss = false;
                _hasRotationInQueue = false;
                _elapsedRotationTime = 0f;
            }
        }
        
    }

    private void FaceBossToPlayer()
    {
        _canRotateBoss = true;
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
        bossDamageSystem.PerformAttack(BossDamageSystem.BossDamageType.LargeDamage);
        StartCoroutine(StopBeingImmuneAfterDelay(1.0f));
    }

    private void ActivateChompTrigger()
    {
        isImmune = true;
        animator.SetTrigger("chomp_trigger");
        bossDamageSystem.PerformAttack(BossDamageSystem.BossDamageType.SmallDamage);
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
