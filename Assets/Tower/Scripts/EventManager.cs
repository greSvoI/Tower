using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDestroy
{
	public class EventManager : MonoBehaviour
	{
		public static EventManager instance;
		private void Awake()
		{
			if(instance == null)
				instance = this;
		}
		//Vibration
		public static Action EventPartDestroyed { get; set; }

		//Camera Shake, x10 score Part SphereCast Invoke
		public static Action EventBlockPartDestroyed { get; set; }

		//UI Game Input
		public static Action<Vector2> EventInput { get;set; }

		//CameraController Invoke
		public static Action EventGameOver { get; set; }

		//BallController OnCollision Invoke
		public static Action EventWinGame { get; set; }

		public static Action<BallData> EventUpdateBall { get; set; }
		public static Action<bool> EventUpdateX2Ball { get; set; }

	}
}
