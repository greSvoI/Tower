using TowerDestroy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDestroy
{
	public class TowerRotate : MonoBehaviour
	{
		[SerializeField] private Vector2 _direction;
		[SerializeField] private float _rotationSpeed = 0.5f;
		[SerializeField] private float _angle = 0;

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
			
			//transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(0f,_angle,0f), _rotationSpeed * Time.deltaTime);
			
			if (_direction != Vector2.zero)
				transform.rotation = transform.rotation *  Quaternion.Euler(0f,-_direction.x * _rotationSpeed * Time.deltaTime,0f);
		}
		private void OnDestroy()
		{
			EventManager.EventInput -= OnEventInput;
		}
	}
}
