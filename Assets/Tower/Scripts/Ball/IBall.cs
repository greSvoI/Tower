using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace TowerDestroy
{
	public interface IBall
	{
		public Material Material { get;  }
		public GameObject Effect { get; }
		public int Strength { get; }
		public float Time { get;  }
		public float Force { get;  }
	}

}