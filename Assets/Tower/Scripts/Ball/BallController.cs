using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDestroy
{
	public class BallController : MonoBehaviour
	{
		private float _timeGame = 0;
		private int _destroyPlatform = 0;
		private float _heightBallY;
		private AudioSource audioSource;
		private Rigidbody rigidBody;

		[Header("Ball Settings")]
		[SerializeField] private BallData ballStandart;
		[SerializeField] private BallData ballData;
		[SerializeField] private int _ballStrength = 50;

		[Header("Jummp Settings")]
		[SerializeField] private float _force = 5f;
		[SerializeField] private float _sphereRadius = 0.05f;
		[SerializeField] private float _rayLenght = 0f;
		[SerializeField] private LayerMask layer;

		[Header("VFX Effect")]
		[SerializeField] private ParticleSystemForceField particalForceField;
		[SerializeField] private GameObject effectBall;
		[SerializeField] private MeshRenderer meshRenderer;

		[Header("Audio clip")]
		[SerializeField] private AudioClip jumpAudio;
		[SerializeField] private AudioClip destroyAudio;
		public int BallStrenght { get => ballData.Strength; }


		private void Start()
		{
			rigidBody = GetComponent<Rigidbody>();
			meshRenderer = GetComponent<MeshRenderer>();
			audioSource = GetComponent<AudioSource>();

			_heightBallY = transform.position.y;

			EventManager.EventUpdateBall += OnUpdateBall;
			EventManager.EventWinGame += OnWinGame;
			EventManager.EventBlockPlatform += OnDestroyPlatform;

			if (ballData != null)
			{
				_ballStrength = ballData.Strength;
			}
		}

		private void OnDestroyPlatform()
		{
			_destroyPlatform++;
			audioSource.PlayOneShot(destroyAudio);
		}

		private void OnWinGame()
		{
			Time.timeScale = 0f;
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
		private IEnumerator UpdateBallTimer(float time)
		{
			yield return new WaitForSeconds(time);
			Destroy(effectBall, 1f);
			ballData = ballStandart;
		}

		public void Jump()
		{			
			audioSource.PlayOneShot(jumpAudio);
			rigidBody.velocity = Vector3.zero;
			rigidBody.AddForce(Vector3.up * ballData.Force,ForceMode.Impulse);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght);
		}
		private void Update()
		{
			_timeGame += Time.deltaTime;
			//if(ballData == ballStandart)
			//{

			//	if(_heightBallY - 1 < transform.position.y)
			//	{
			//		Debug.Log("Game Over");
			//	}
			//}
		}
		private void FixedUpdate()
		{
			if (Physics.CheckSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght, layer))
			{
				Jump();
			}
		}

		//private void OnTriggerEnter(Collider other)
		//{
		//	if(other.tag == "Finish")
		//	{
		//		Time.timeScale = 0f;
		//		EventManager.EventWinGame?.Invoke();
		//	}
		//}
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
			EventManager.EventBlockPlatform -= OnDestroyPlatform;
		}
	}

}