using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class Platform : MonoBehaviour
	{
		[SerializeField] private PlatformPart[]parts;
		private void Start()
		{
			parts = GetComponentsInChildren<PlatformPart>();
			parts[Random.Range(0, parts.Length - 1)].BlockPart();
		}
	}
}