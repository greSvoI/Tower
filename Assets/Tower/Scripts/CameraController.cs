using System.Collections;
using TMPro;
using UnityEngine;

namespace CubeSurfer
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform playerTransform;
		[SerializeField] private Transform mouseTransform;
		[SerializeField] private Transform targetTransform;
		[SerializeField] private Transform cameraTransformPos;

		private Vector3 newPosition;
		private Vector3 targetOffset;

		[Header("Offset camera state")]
		[SerializeField] private Vector3 offsetMouse;
		[SerializeField] private Vector3 offsetPlayer;
		[SerializeField] private float _lerpValue;

		[Header("Camera shake force")]
		[SerializeField] private float _duration;
		[SerializeField] private float _magnitude;

		[Header("Rotate SkyBox")]
		[SerializeField] private float _rotateSpeedSky = 1f;
		[SerializeField] private Material _sky;

		void Start()
		{
			EventManager.EventGameOver += OnGameOver;
			targetOffset = offsetPlayer;
		}

		private void OnEventLostCube()
		{
			StartCoroutine(Shake(_duration, _magnitude));
		}

		private void OnGameOver(int score)
		{
			targetOffset = offsetMouse;
		}
		private void Update()
		{
			targetTransform = playerTransform;
			//RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotateSpeedSky);

		}
		
		void LateUpdate()
		{
			SetCameraFollow();
		}

		private void SetCameraFollow()
		{
			newPosition = Vector3.Lerp(cameraTransformPos.position, new Vector3(0f, targetTransform.position.y, targetTransform.position.z) + targetOffset, _lerpValue * Time.deltaTime);
			cameraTransformPos.position = newPosition;
		}
		private void OnDestroy()
		{

			EventManager.EventGameOver -= OnGameOver;
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
