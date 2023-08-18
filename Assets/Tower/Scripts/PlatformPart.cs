using System;
using System.Collections;
using UnityEngine;

namespace TowerDestroy
{
	public class PlatformPart : MonoBehaviour
	{
		[SerializeField] private GameObject platform;
		[SerializeField] private GameObject destroyPlatform;
		[SerializeField] private GameObject[] destroyPlatforms;
		[SerializeField] private MeshCollider colliderConvex;

		[SerializeField] private Material crackPlatform;
		[SerializeField] private Material lastJumpPlatform;

		[Header("RigidBody Component")]
		[SerializeField] private float _radius;
		[SerializeField] private float _force;

		[Header("Healt PlatformPart")]
		[SerializeField] int _health = 100;
		

		private void Start()
		{
			if(platform == null)
			{
				platform = GetComponent<GameObject>();
			}
			
			//destroyPlatform.SetActive(true);


			//this.gameObject.AddComponent<MeshCollider>();
			//this.gameObject.GetComponent<MeshCollider>().convex = true;
			//this.gameObject.GetComponent<MeshCollider>().isTrigger = true;

		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log("Trigger part");

			int health = other.GetComponent<Ball>().BallStrenght;

			_health -= health;

			platform.GetComponent<MeshRenderer>().enabled = false;

			destroyPlatform.SetActive(true);

			if (_health <= 0)
			{
				platform.SetActive(false);
				DestroyPlatform();
			}

		}
		private void OnTriggerStay(Collider other)
		{
			Debug.Log("Trigger stay Part");
		}
		private void OnTriggerExit(Collider other)
		{
			Debug.Log("Trigger exit Part");
		}
		//Not Working
		private void OnCollisionEnter(Collision collision)
		{
			Debug.Log("Collision enter Part");
		}
		private void OnCollisionStay(Collision collision)
		{
			Debug.Log("Collision stay Part");
		}
		private void OnCollisionExit(Collision collision)
		{
			Debug.Log("Collision exit Part");
		}

		public void DestroyPlatform()
		{
			
			foreach (GameObject item in destroyPlatforms)
			{
				if(item.TryGetComponent(out Rigidbody rb))
				{
					rb.isKinematic = false;
					rb.AddExplosionForce(_force, transform.position, _radius);
				}
			}
			//StartCoroutine(ShowDestroyPlatforms(2));
		}

		private IEnumerator ShowDestroyPlatforms(int time)
		{
			yield return new WaitForSeconds(time);
			destroyPlatform.SetActive(false);
		}
	}
}
