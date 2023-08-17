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
		public static Action<Vector2> EventInput { get;set; }
		public static Action<int> EventGameOver { get; set; }	

	}
}
