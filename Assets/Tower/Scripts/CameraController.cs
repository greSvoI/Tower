using System.Collections;
using UnityEngine;

namespace TowerDestroy
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform ball;
		[SerializeField] private Transform ballMinPosition;
		[SerializeField] private Transform tower;

		[Header("Camera Settings")]
		[SerializeField] private Vector3 _directionOffset;
		[SerializeField] private Vector3 _cameraPosition;
		[SerializeField] private Vector3 _minBallPosition;
		[SerializeField] private float _lerpValue;
		[SerializeField] private float _lenght;

		[Header("Camera shake force")]
		[SerializeField] private float _duration;
		[SerializeField] private float _magnitude;

		[Header("Rotate SkyBox")]
		//[SerializeField] private float _rotateSpeedSky = 1f;
		[SerializeField] private Material _sky;

		private void Start()
		{
			_cameraPosition = ball.position;
			_minBallPosition = ball.position;
		}
		private void Update()
		{

			RenderSettings.skybox.SetFloat("_Rotation", Time.time * 2f);
		}
		void LateUpdate()
		{
			if(ball.position.y > _minBallPosition.y)
			{
				//TrackBall();
				_minBallPosition = ball.position;
			}
			transform.position = Vector3.Lerp(transform.position, new Vector3(_minBallPosition.x, _minBallPosition.y, _minBallPosition.z) + _directionOffset, _lerpValue * Time.deltaTime);

		}
		private void TrackBall()
		{
			_cameraPosition = ball.position + _directionOffset;
			transform.position = _cameraPosition;

		}
		private void SetCameraFollow()
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(ball.position.x, ball.position.y, ball.position.z) + _directionOffset, _lerpValue * Time.deltaTime);
			
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

	}
}
