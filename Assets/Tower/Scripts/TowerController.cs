using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDestroy
{
	public class TowerController : MonoBehaviour
	{
		public bool _isTower = true;

		[Header("Tower prefab, parametrs")]
		[SerializeField] private Tower towerOne;
		[SerializeField] private Tower unlimTower;
		[SerializeField] private Vector2 _direction;
		[SerializeField] private int _spawnTower = 0;
		[SerializeField] private int _heightTower = 10;

		[Space(2)]
		[Header("Ball parametrs")]
		[SerializeField] protected BallController ball;
		[SerializeField] private float _minBallPosition;
		

		[Space(2)]
		[Header("Sensivity Menu, speed rotate tower")]
		[SerializeField] private Slider slider;
		[SerializeField] private float _rotationSpeed = 0.5f;


		/// <summary>
		/// ???
		protected bool IsTower { get => _isTower; set => _isTower = value;  }
		/// </summary>

		private void Start()
		{
			int tower = PlayerPrefs.GetInt("_isTower");

			IsTower = tower == 1 ? true : false;
			_minBallPosition = ball.transform.position.y;

			GameObject nextTower;
			if (IsTower)
			{
				nextTower = Instantiate(towerOne.gameObject, Vector3.zero, Quaternion.identity, transform);
			}
			else
			{
				nextTower = Instantiate(unlimTower.gameObject, Vector3.zero, Quaternion.identity, transform);
			}

			nextTower.GetComponent<Tower>().SpawnPlatform(-_heightTower, _heightTower, true);
			_spawnTower += _heightTower;

			EventManager.EventInput += OnEventInput;
		}


		private void OnEventInput(Vector2 vector)
		{
			_direction = vector;
		}

		private void Update()
		{

			if (_direction != Vector2.zero)
				transform.rotation = transform.rotation * Quaternion.Euler(0f, -_direction.x * _rotationSpeed, 0f);

			if(!_isTower)
			{
				if (ball.transform.position.y > (_spawnTower - _heightTower / 2))
				{
					SpawnTower();
					_spawnTower += _heightTower;
				}

			}

		}

		private void SpawnTower()
		{
		    GameObject nextTower = Instantiate(unlimTower.gameObject, new Vector3(0f,_spawnTower,0f), Quaternion.identity, transform);
			nextTower.GetComponent<Tower>().SpawnPlatform(_spawnTower ,_spawnTower + _heightTower, false);
		}

		private void OnDestroy()
		{
			EventManager.EventInput -= OnEventInput;
		}
		public void Sensivity()
		{		
			_rotationSpeed = slider.value;
		}
	}
}
