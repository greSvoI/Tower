using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class BallController : MonoBehaviour
	{
		[SerializeField] private BallData ballData;
		[SerializeField] private int _ballStrength = 50;
		[SerializeField] Rigidbody rigidBody;
		[SerializeField] private float _force = 5f;
		[SerializeField] private float _sphereRadius = 0.05f;
		[SerializeField] private float _rayLenght = 0f;
		[SerializeField] private LayerMask layer;
		[SerializeField] private MeshRenderer meshRenderer;

		public int BallStrenght { get => ballData.Strength; }

		public float _time=0;

		private void Start()
		{
			rigidBody = GetComponent<Rigidbody>();
			meshRenderer = GetComponent<MeshRenderer>();
			EventManager.EventUpdateBall += OnUpdateBall;
			EventManager.EventWinGame += OnWinGame;
			if (ballData != null)
			{
				_ballStrength = ballData.Strength;
			}
		}

		private void OnWinGame(float obj)
		{

			Debug.Log(Math.Round(_time, 2));
		}

		private void OnUpdateBall(BallData ball)
		{
			ballData = ball;
			meshRenderer.material = ballData.Material;
		}

		public void Jump(float force)
		{
			rigidBody.velocity = Vector3.zero;
			rigidBody.AddForce(Vector3.up * _force);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght);
		}
		private void Update()
		{
			if(transform.position.y > 10)
			{
				EventManager.EventWinGame?.Invoke((float)Math.Round(_time,2));
			}
		}
		private void FixedUpdate()
		{
			_time+= Time.deltaTime;
			if (Physics.CheckSphere(transform.position + Vector3.down * _sphereRadius, _rayLenght, layer))
			{
				rigidBody.velocity = Vector3.zero;
				rigidBody.AddForce(Vector3.up * ballData.Force, ForceMode.Impulse);
			}
		}
		private void OnDestroy()
		{
			EventManager.EventUpdateBall -= OnUpdateBall;
			EventManager.EventWinGame -= OnWinGame;
		}
	}

}