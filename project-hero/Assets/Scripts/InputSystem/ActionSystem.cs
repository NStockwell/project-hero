using UnityEngine;

namespace InputSystem
{
    public enum Action
    {
        Attack,
        SwipeLeft,
        SwipeRight,
        SwipeForward,
        SwipeBackwards
    }
    
    public class ActionSystem : MonoBehaviour
    {
        public delegate void ActionEvent(Action action);
        public event ActionEvent OnActionTaken;

        private void OnEnable()
        {
            GestureDetection.Instance.OnTouchDetected += TouchDetected;
            GestureDetection.Instance.OnSwipeDetected += SwipeDetected;
        }

        private void OnDisable()
        {
            GestureDetection.Instance.OnTouchDetected -= TouchDetected;
            GestureDetection.Instance.OnSwipeDetected -= SwipeDetected;
        }

        private void TouchDetected()
        {
            if (OnActionTaken != null)
            {
                OnActionTaken(Action.Attack);
            }
        }

        private void SwipeDetected(SwipeDirection direction)
        {
            if (OnActionTaken != null)
            {
                switch (direction)
                {
                    case SwipeDirection.Up:
                        OnActionTaken(Action.SwipeForward);
                        break;
                    case SwipeDirection.Down:
                        OnActionTaken(Action.SwipeBackwards);
                        break;
                    case SwipeDirection.Left:
                        OnActionTaken(Action.SwipeLeft);
                        break;
                    case SwipeDirection.Right:
                        OnActionTaken(Action.SwipeRight);
                        break;
                }
            }
        }
    }
}
