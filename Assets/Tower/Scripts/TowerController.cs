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

		[SerializeField] private Tower towerOne;
		[SerializeField] private Tower unlimTower;
		[SerializeField] private BallController ball;
		[SerializeField] private Slider slider;
		[SerializeField] private Vector2 _direction;
		[SerializeField] private float _rotationSpeed = 0.5f;
		[SerializeField] private bool _unlimTower = false;
		[SerializeField] private int _spawnTower = 0;
		[SerializeField] private int _heightTower = 10;

	 	[SerializeField] private GameObject lastTower;
		[SerializeField] private GameObject nextTower;

		public bool IsTower { get => _isTower;set => _isTower = value;  }

		private void Start()
		{
			//int tower = PlayerPrefs.GetInt("_isTower");
			//IsTower = tower == 1 ? true : false;
			IsTower = true;
			if(IsTower)
				lastTower = Instantiate(towerOne.gameObject, Vector3.zero, Quaternion.identity, transform);
			else
				lastTower = Instantiate(towerOne.gameObject, Vector3.zero, Quaternion.identity, transform);


			lastTower.GetComponent<Tower>().SpawnPlatform(-9,_heightTower); 
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
			Debug.Log("SpawnTower");
			nextTower = Instantiate(unlimTower.gameObject, new Vector3(0f,_spawnTower,0f), Quaternion.identity, transform);
			nextTower.GetComponent<Tower>().SpawnPlatform(_spawnTower,_spawnTower + _heightTower);

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
