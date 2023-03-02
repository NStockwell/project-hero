using System.Collections;
using InputSystem;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
	private InputManager _inputManager;
	
	[SerializeField] private float minimumDistance = .2f;
	[SerializeField] private float maximumTime = 1f;
	[SerializeField, Range(0f, 1f)] private float verticalDirectionThreshold = .9f;
	[SerializeField, Range(0f, 1f)] private float horizontalDirectionThreshold = .65f;
	[SerializeField] private GameObject _trail;

	private Coroutine _trailCoroutine;
	
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
		
		_trail.SetActive(true);
		_trail.transform.position = position;
		_trailCoroutine = StartCoroutine(Trail());
	}

	private IEnumerator Trail()
	{
		while (true)
		{
			_trail.transform.position = _inputManager.PrimaryPosition();
			yield return null;
		}
	}

	private void SwipeEnd(Vector2 position, float time)
	{
		_trail.SetActive(false);
		StopCoroutine(_trailCoroutine);

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
			Debug.DrawLine(_startPosition, _endPosition, Color.red, 5f);

			Vector3 direction = _endPosition - _startPosition;
			Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
			SwipeDirection(direction2D);
		} 
	}

	private void SwipeDirection(Vector2 direction)
	{
		// Dot Product (Produto Externo)
		// See if we are close enough to the threshold
		var resultUp = Vector2.Dot(Vector2.up, direction);
		var resultDown = Vector2.Dot(Vector2.down, direction);
		var resultLeft = Vector2.Dot(Vector2.left, direction);
		var resultRight = Vector2.Dot(Vector2.right, direction);
		
		if (Vector2.Dot(Vector2.up, direction) > verticalDirectionThreshold)
		{
			Debug.Log("Swipe Up");
		}
		else if (Vector2.Dot(Vector2.down, direction) > verticalDirectionThreshold)
		{
			Debug.Log("Swipe Down");
		}
		else if (Vector2.Dot(Vector2.left, direction) > horizontalDirectionThreshold)
		{
			Debug.Log("Swipe Left");
		} 
		else if (Vector2.Dot(Vector2.right, direction) > horizontalDirectionThreshold)
		{
			Debug.Log("Swipe Right");
		}
	}
}
