using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
	[DefaultExecutionOrder(-1)]
	public class InputManager : Singleton<InputManager>
	{
		public delegate void StartTouchEvent(Vector2 position, float time);
		public event StartTouchEvent OnStartTouch;
		public delegate void EndTouchEvent(Vector2 position, float time);
		public event EndTouchEvent OnEndTouch;
	
		private TouchControls _touchControls;
		private Camera _mainCamera;

		public override void Awake()
		{
			base.Awake();
			_touchControls = new TouchControls();
			_mainCamera = Camera.main;
		}

		private void OnEnable()
		{
			_touchControls.Enable();
		}

		private void OnDisable()
		{
			_touchControls.Disable();
		}

		private void Start()
		{
			_touchControls.Touch.PrimaryContact.started += StartTouch;
			_touchControls.Touch.PrimaryContact.canceled += EndTouch;
		}

		private void StartTouch(InputAction.CallbackContext context)
		{
			Debug.Log("Touch started " + _touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
			if (OnStartTouch != null)
			{
				OnStartTouch(
					Utils.ScreenToWorld(_mainCamera, 
						_touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
					(float) context.startTime);
			}
		}

		private void EndTouch(InputAction.CallbackContext context)
		{
			Debug.Log("Touch ended");
			if (OnEndTouch != null)
			{
				OnEndTouch(
					Utils.ScreenToWorld(_mainCamera, 
						_touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()),
					(float) context.time);
			}
		}

		public Vector2 PrimaryPosition()
		{
			return Utils.ScreenToWorld(_mainCamera, 
				_touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
		}
	}
}
