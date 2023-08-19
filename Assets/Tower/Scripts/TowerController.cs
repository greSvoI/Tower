using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class TowerController : MonoBehaviour
	{
		[SerializeField] private Tower towerPrefab;
		[SerializeField] private BallController ball;
		[SerializeField] private Vector2 _direction;
		[SerializeField] private float _rotationSpeed = 0.5f;

		private void Awake()
		{
			GameObject tower = Instantiate(towerPrefab.gameObject, Vector3.zero, Quaternion.identity, transform);
		}
		private void Start()
		{
			EventManager.EventInput += OnEventInput;
		}

		private void OnEventInput(Vector2 vector)
		{
			_direction = vector;
		}

		private void Update()
		{
			if (_direction != Vector2.zero)
				transform.rotation = transform.rotation * Quaternion.Euler(0f, -_direction.x * _rotationSpeed * Time.deltaTime, 0f);
		}
		private void OnDestroy()
		{
			EventManager.EventInput -= OnEventInput;
		}
	}
}
