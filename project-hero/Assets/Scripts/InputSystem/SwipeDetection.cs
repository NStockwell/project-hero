using InputSystem;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	private InputManager _inputManager;

	private Vector2 _startPosition;
	private float _startTime;
	
	private Vector2 _endPosition;
	private float _endTime;

	private void Awake()
	{
		_inputManager = InputManager.Instance;
	}

	private void OnEnable()
	{
		_inputManager.OnStartTouch += SwipeStart;
		_inputManager.OnEndTouch += SwipeEnd;
	}

	private void OnDisable()
	{
		_inputManager.OnStartTouch -= SwipeStart;
		_inputManager.OnEndTouch -= SwipeEnd;
	}

	private void SwipeStart(Vector2 position, float time)
	{
		_startPosition = position;
		_startTime = time;
	}

	private void SwipeEnd(Vector2 position, float time)
	{
		DetectSwipe();
	}

	private void DetectSwipe()
	{
		
	}
}
