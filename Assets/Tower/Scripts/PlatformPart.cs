using UnityEngine;

namespace TowerDestroy
{
	public class PlatformPart : MonoBehaviour
	{
		[SerializeField] private GameObject platform;
		[SerializeField] private GameObject destroyPlatform;
		[SerializeField] private GameObject[] destroyPlatforms;

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
			destroyPlatform.SetActive(false);
		}

		private void OnTriggerEnter(Collider other)
		{
			int health = other.GetComponent<Ball>().BallStrenght;
			_health -= health;

			if (_health <= 0)
			{
				platform.SetActive(false);
				DestroyPlatform();
			}
			if(_health - health <= 0)
			{
				foreach (GameObject item in destroyPlatforms)
				{
					if (item.TryGetComponent(out MeshRenderer mesh))
					{
						mesh.material = lastJumpPlatform;
					}
				}
			}
			platform.GetComponent<MeshRenderer>().enabled = false;			
			destroyPlatform.SetActive(true);
		}

		//Not Working
		private void OnCollisionEnter(Collision collision)
		{
			//if (_totalJump == _jumpToDestroy)
			//{
			//	platform.SetActive(false);
			//	DestroyPlatform();
			//}
			//platform.GetComponent<MeshRenderer>().enabled = false;
			//destroyPlatform.SetActive(true);
			//_totalJump++;
		}


		public void DestroyPlatform()
		{
			foreach(GameObject item in destroyPlatforms)
			{
				if(item.TryGetComponent(out Rigidbody rb))
				{
					rb.isKinematic = false;
					rb.AddExplosionForce(_force, transform.position, _radius);
				}
			}
		}
	}
}
