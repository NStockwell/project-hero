using InputSystem;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   [SerializeField]
   private Animator animator;
   public Animator Animator => animator;

   [SerializeField] 
   private ActionSystem actionSystem;
   
   
   private static readonly int IdleAnimation = Animator.StringToHash("2Hand-Sword-Idle");
   private static readonly int Attack1Animation = Animator.StringToHash("2Hand-Sword-Attack1");
   private static readonly int Attack2Animation = Animator.StringToHash("2Hand-Sword-Attack2");
   private static readonly int Attack3Animation = Animator.StringToHash("2Hand-Sword-Attack3");
   private static readonly int Attack4Animation = Animator.StringToHash("2Hand-Sword-Attack4");
   private static readonly int Attack5Animation = Animator.StringToHash("2Hand-Sword-Attack5");
   private static readonly int Attack6Animation = Animator.StringToHash("2Hand-Sword-Attack6");
   private static readonly int Attack7Animation = Animator.StringToHash("2Hand-Sword-Attack7");
   private static readonly int Attack8Animation = Animator.StringToHash("2Hand-Sword-Attack8");
   private static readonly int Attack9Animation = Animator.StringToHash("2Hand-Sword-Attack9");
   private static readonly int Attack10Animation = Animator.StringToHash("2Hand-Sword-Attack10");
   
   
   private static readonly int TriggerNumber = Animator.StringToHash("TriggerNumber");
   private static readonly int Weapon = Animator.StringToHash("Weapon");
   private static readonly int Trigger = Animator.StringToHash("Trigger");
   
   private static readonly int Action = Animator.StringToHash("Action");

   private const int MaxNumberAttacks = 9;
   private float elapsedTimeSinceLastAnimation = 0;
   private void Start()
   {
      animator.SetInteger(TriggerNumber, 4);
      animator.SetInteger(Weapon, 1);
      actionSystem.OnActionTaken += ActionSystemOnOnActionTaken;
   }

   private void ActionSystemOnOnActionTaken(Action action)
   {
      if (action is InputSystem.Action.SwipeLeft)
      {
         Dodge(false);
         return;
      }

      if (action is InputSystem.Action.SwipeRight)
      {
         Dodge(true);
         return;
      }
   }

   private int lastAttack = 1;
   private void FixedUpdate()
   {
      //Debug.Log($"PlayerAnimation - elapsedTime: {elapsedTimeSinceLastAnimation}, counter:{ComboSystem.Instance.currentHitCounter}");
      elapsedTimeSinceLastAnimation += Time.fixedDeltaTime;
      if (elapsedTimeSinceLastAnimation < .5f || ComboSystem.Instance.currentHitCounter == 0) return;
      elapsedTimeSinceLastAnimation = 0;
      lastAttack = lastAttack % MaxNumberAttacks + 1;
      animator.SetInteger(TriggerNumber, 4);
      animator.SetInteger(Action, lastAttack);
      animator.SetTrigger(Trigger);
   }

   private void Dodge(bool right)
   {
      GetComponentInParent<DummyPlayerCharacter>().Dodge(right);
      
      animator.SetInteger(Action, 1);
      animator.SetInteger(TriggerNumber, 28);
      animator.SetInteger(Weapon, 1);
      animator.SetTrigger(Trigger);
      
   }
}
