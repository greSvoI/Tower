using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TowerDestroy
{
	[CreateAssetMenu(fileName = "Ball data", menuName = "TowerDestroy/Ball data", order = 1)]
	public class BallData : ScriptableObject, IBall
	{
		[SerializeField] private Material materialBall;
		[SerializeField] private GameObject effect;
		[SerializeField] private int _strength;
		[SerializeField] private float _time;
		[SerializeField] private float _force;
		[SerializeField] private string _nameBall;

		public int Strength { get => _strength; }
		public float Time { get => _time; }
		public float Force { get => _force;}
		public Material Material { get => materialBall; }
		public GameObject Effect { get => effect; }

		public string NameBall => _nameBall;
	}
}
