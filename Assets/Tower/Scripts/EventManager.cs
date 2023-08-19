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
		public static Action EventDestroyPlatform { get; set; }
		public static Action<Vector2> EventInput { get;set; }
		public static Action<float> EventGameOver { get; set; }
		public static Action<float> EventWinGame { get; set; }
		public static Action<BallData> EventUpdateBall { get; set; }

	}
}
