using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class BallDetection : MonoBehaviour
	{
		[SerializeField] BallData ballData;
		private void OnTriggerEnter(Collider other)
		{
			EventManager.EventUpdateBall?.Invoke(ballData);
			this.gameObject.SetActive(false);	
			Destroy(gameObject,1f);
		}
	}
}
