using System.Collections;
using TMPro;
using UnityEngine;

namespace TowerDestroy
{
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform playerTransform;
		[SerializeField] private Transform cameraTransformPos;

		[Header("Offset camera state")]
		[SerializeField] private Vector3 offsetPlayer;
		[SerializeField] private float _lerpValue;

		[Header("Camera shake force")]
		[SerializeField] private float _duration;
		[SerializeField] private float _magnitude;

		[Header("Rotate SkyBox")]
		[SerializeField] private float _rotateSpeedSky = 1f;
		[SerializeField] private Material _sky;


		
		void LateUpdate()
		{
			//SetCameraFollow();
		}

		private void SetCameraFollow()
		{
			cameraTransformPos.position = Vector3.Lerp(cameraTransformPos.position, new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z) + offsetPlayer, _lerpValue * Time.deltaTime);
			
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
