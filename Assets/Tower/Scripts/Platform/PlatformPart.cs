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

		[Header("Material TriggerPart")]
		[SerializeField] private Material blockPart;
		[SerializeField] private Material crackPlatform;
		[SerializeField] private Material lastJumpPlatform;

		[Header("RigidBody Component")]
		[SerializeField] private float _radius;
		[SerializeField] private float _force;

		[Header("Healt PlatformPart")]
		[SerializeField] int _health = 100;

		[Header("Audio Component")]
		[SerializeField] private AudioClip destroyClip;

	
		
		private void Start()
		{
			if(platform == null)
			{
				platform = GetComponent<GameObject>();
			}
		}	
		public void TriggerPart(int strenght)
		{
			if(_health<=strenght)
			{
				platform.SetActive(false);
				destroyPlatform.SetActive(true);
				DestroyPlatform();
				return;
			}
			
			_health -= strenght;
			platform.GetComponent<MeshRenderer>().enabled = false;
			destroyPlatform.SetActive(true);
		}
		public void BlockPart()
		{
			GetComponentInChildren<Part>().DisablePart(blockPart);	
		}

		public void DestroyPlatform()
		{
			
			platform.SetActive(false);
			destroyPlatform.SetActive(true);
			EventManager.EventBlockPlatform?.Invoke();
			destroyPlatform.GetComponentInChildren<ParticleSystem>().Emit(1);
			foreach (GameObject item in destroyPlatforms)
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
