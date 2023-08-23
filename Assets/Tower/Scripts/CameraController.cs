using System.Collections;
using UnityEngine;

namespace TowerDestroy
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform ball;
		[SerializeField] private Transform ballMinPosition;
		[SerializeField] private Transform tower;

		[Header("Camera parametrs")]
		[SerializeField] private Vector3 _directionOffset;
		[SerializeField] private Transform _cameraPosition;
		[SerializeField] private float _lerpValue;
		[SerializeField] private float _lenght;

		[Header("Camera shake force")]
		[SerializeField] 
		[Range(0,1)] private float _duration;
		[SerializeField] 
		[Range(0, 1)] private float _magnitude;


		[Header("Ball parametrs")]
		[SerializeField]
		[Range(0,5)] private float _positionGameOver;
		[SerializeField] private Vector3 _minBallPosition;
		[SerializeField] private bool _isFell = false;

		private void Start()
		{
			
			_minBallPosition = ball.position;

			EventManager.EventBlockPartDestroyed += OnBlockDestroyed;
		}

		private void OnBlockDestroyed()
		{
			StartCoroutine(Shake(_duration, _magnitude));
		}


		private void LateUpdate()
		{
			_cameraPosition.position = Vector3.Lerp(_cameraPosition.position, new Vector3(_minBallPosition.x, _minBallPosition.y, _minBallPosition.z) + _directionOffset, _lerpValue * Time.deltaTime);
			BallPosition();
		}
		private void BallPosition()
		{
			if (ball.position.y > _minBallPosition.y)
			{
				_minBallPosition = ball.position;
				_isFell = false;
				return;
			}
			if (ball.position.y < _minBallPosition.y - _positionGameOver)
			{
				EventManager.EventGameOver?.Invoke();
			}
			if (ball.position.y < _minBallPosition.y - 1 && !_isFell)
			{
				_isFell = true;
				_minBallPosition = ball.position;
			}
		}
		private IEnumerator Shake(float duration, float magnitude)
		{
			Vector3 originPos = this.transform.localPosition;

			float elapsed = 0f;
			while (elapsed < duration)
			{
				float x = Random.Range(-1f, 1f) * magnitude;
				float y = Random.Range(-1f, 1f) * magnitude;

				transform.localPosition = new Vector3(x, y, originPos.z);
				elapsed += Time.deltaTime;

				yield return null;
			}
			this.transform.localPosition = originPos;
		}

		private void OnDestroy()
		{
			EventManager.EventBlockPartDestroyed -= OnBlockDestroyed;
		}

	}
}
