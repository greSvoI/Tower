using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDestroy
{
	public class BallController : MonoBehaviour
	{

		private AudioSource audioSource;
		private Rigidbody rigidBody;

		[Header("Ball Settings")]
		[SerializeField] private BallData ballStandart;
		[SerializeField] private BallData ballData;

		[Space(2)]
		[Header("Jummp Settings")]
		[SerializeField] private float _force = 5f;
		[SerializeField] private float _sphereRadius = 0.05f;
		[SerializeField] private float _rayLenght = 0f;
		[SerializeField] private LayerMask layer;

		[Space(2)]
		[Header("VFX Effect")]
		[SerializeField] private ParticleSystemForceField particalForceField;
		[SerializeField] private GameObject effectBall;
		[SerializeField] private MeshRenderer meshRenderer;

		[Space(2)]
		[Header("Audio clip")]
		[SerializeField] private AudioClip jumpAudio;
		[SerializeField] private AudioClip destroyAudio;

		public int BallStrenght { get => ballData.Strength; }

		private void Start()
		{
			rigidBody = GetComponent<Rigidbody>();
			meshRenderer = GetComponent<MeshRenderer>();
			audioSource = GetComponent<AudioSource>();

			EventManager.EventUpdateBall += OnUpdateBall;
			EventManager.EventWinGame += OnWinGame;
			EventManager.EventPartDestroyed += OnDestroyPlatform;
		}

		[ContextMenu("Super Ball")]
		public void SuperBall()
		{

			meshRenderer.material = ballData.Material;
			if (effectBall != null) { Destroy(effectBall); }
			effectBall = Instantiate(ballData.Effect, transform);
			effectBall.transform.localPosition = Vector3.zero;
			foreach (var item in effectBall.GetComponentsInChildren<ParticleSystem>())
			{
				item.externalForces.AddInfluence(particalForceField);
				item.Emit(1);
			}
		}
		private void OnWinGame()
		{
			Time.timeScale = 0f;
		}
		private void OnDestroyPlatform()
		{
			audioSource.PlayOneShot(destroyAudio);
		}
		private void OnUpdateBall(BallData ball)
		{
			ballData = ball;
			meshRenderer.material = ballData.Material;
			if(effectBall != null) { Destroy(effectBall); }
			effectBall =  Instantiate(ball.Effect, transform);
			effectBall.transform.localPosition = Vector3.zero;
			foreach(var item in effectBall.GetComponentsInChildren<ParticleSystem>())
			{
				item.externalForces.AddInfluence(particalForceField);
				item.Emit(1);
			}
			StartCoroutine(UpdateBallTimer(ball.Time));
		}
		
		private void FixedUpdate()
		{
			if (Physics.CheckSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght, layer))
			{
				Jump();
			}
		}
		private void Jump()
		{
			audioSource.PlayOneShot(jumpAudio);
			rigidBody.velocity = Vector3.zero;
			rigidBody.AddForce(Vector3.up * ballData.Force, ForceMode.Impulse);
		}
		private IEnumerator UpdateBallTimer(float time)
		{
			yield return new WaitForSeconds(time);
			Destroy(effectBall, 1f);
			ballData = ballStandart;
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Finish"))
			{
				EventManager.EventWinGame?.Invoke();
			}
		}
		private void OnDestroy()
		{
			EventManager.EventUpdateBall -= OnUpdateBall;
			EventManager.EventWinGame -= OnWinGame;
			EventManager.EventPartDestroyed -= OnDestroyPlatform;
		}
	}

}