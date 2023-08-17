using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("PlatformPart trigger");
	}
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("PlatformPart collision");
	}
}
