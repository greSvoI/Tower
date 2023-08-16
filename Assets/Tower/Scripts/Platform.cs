using UnityEngine;

namespace TowerDestroy
{
	public class Platform : MonoBehaviour
	{
	
		[SerializeField] private GameObject platform;
		[SerializeField] private GameObject[] destroyPlatforms;

		[Header("RigidBody Component")]
		[SerializeField] private float _radius;
		[SerializeField] private float _force;


		public int _totalJump = 0;
		public bool _forces = false;

		private void OnTriggerEnter(Collider other)
		{
			if(other.tag == "Ball")
			{
				if(_totalJump == 0)
				{
					platform.SetActive(false);
					platform.SetActive(true);
				}
				if(_totalJump == 3)
				{
					AddForce();
				}
			}
		}
		public void AddForce()
		{
			foreach(GameObject go in destroyPlatforms)
			{
				if(go.TryGetComponent(out Rigidbody rb))
				{
					rb.isKinematic = false;
					rb.AddExplosionForce(_force, transform.position, _radius);
				}
			}
		}
	}
}
