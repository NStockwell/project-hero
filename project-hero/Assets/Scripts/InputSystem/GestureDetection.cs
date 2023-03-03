using UnityEngine;

namespace InputSystem
{
	public enum SwipeDirection
	{
		Up,
		Down,
		Left,
		Right
	}
	
	[DefaultExecutionOrder(-1)]
	public class GestureDetection : Singleton<GestureDetection>
	{
		public delegate void SwipeEvent(SwipeDirection direction);
		public event SwipeEvent OnSwipeDetected;

		public delegate void TouchEvent();
		public event TouchEvent OnTouchDetected;
	
		private InputManager _inputManager;
	
		[SerializeField] private float minimumDistance = 10.0f;
		[SerializeField] private float maximumTime = 1f;
		[SerializeField, Range(0f, 1f)] private float verticalDirectionThreshold = .9f;
		[SerializeField, Range(0f, 1f)] private float horizontalDirectionThreshold = .65f;
		
		private Vector2 _startPosition;
		private float _startTime;
	
		private Vector2 _endPosition;
		private float _endTime;

		public override void Awake()
		{
			base.Awake();
			
			_inputManager = InputManager.Instance;
		}

		private void OnEnable()
		{
			_inputManager.OnStartTouch += TouchStart;
			_inputManager.OnEndTouch += TouchEnd;
		}

		private void OnDisable()
		{
			_inputManager.OnStartTouch -= TouchStart;
			_inputManager.OnEndTouch -= TouchEnd;
		}

		private void TouchStart(Vector2 position, float time)
		{
			_startPosition = position;
			_startTime = time;
		}
		
		private void TouchEnd(Vector2 position, float time)
		{
			_endPosition = position;
			_endTime = time;
		
			DetectSwipe();
		}

		private void DetectSwipe()
		{
			if (Vector3.Distance(_startPosition, _endPosition) >= minimumDistance &&
			    _endTime - _startTime <= maximumTime)
			{
				Debug.Log("Swipe Detected");
				//Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);

				Vector3 direction = _endPosition - _startPosition;
				Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
				SwipeDirection(direction2D);
			}
			else
			{
				if (OnTouchDetected != null)
				{
					OnTouchDetected();
				}
			}
		}

		private void SwipeDirection(Vector2 direction)
		{
			// Dot Product (Produto Externo)
			// See if we are close enough to the threshold
			if (Vector2.Dot(Vector2.up, direction) > verticalDirectionThreshold)
			{
				if (OnSwipeDetected != null)
				{
					OnSwipeDetected(InputSystem.SwipeDirection.Up);
				}
				
			}
			else if (Vector2.Dot(Vector2.down, direction) > verticalDirectionThreshold)
			{
				if (OnSwipeDetected != null)
				{
					OnSwipeDetected(InputSystem.SwipeDirection.Down);
				}
			}
			else if (Vector2.Dot(Vector2.left, direction) > horizontalDirectionThreshold)
			{
				if (OnSwipeDetected != null)
				{
					OnSwipeDetected(InputSystem.SwipeDirection.Left);
				}
			} 
			else if (Vector2.Dot(Vector2.right, direction) > horizontalDirectionThreshold)
			{
				if (OnSwipeDetected != null)
				{
					OnSwipeDetected(InputSystem.SwipeDirection.Right);
				}
			}
		}
	}
}
