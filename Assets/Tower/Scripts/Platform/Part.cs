using System.Collections;
using System.Collections.Generic;
using TowerDestroy;
using UnityEngine;

namespace TowerDestroy
{
	public class Part : MonoBehaviour
	{
		private PlatformPart platform;
		private bool _isNotDestroy = false;

		[Header("SphereCast parametrs")]
		[SerializeField] private float _sphereRadius = 0.2f;
		[SerializeField] private float _heightDown = 0.5f;
		[SerializeField] private float _maxDistance = 0.5f;
		[SerializeField] private LayerMask mask;
		private void Start()
		{
			platform = GetComponentInParent<PlatformPart>();
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position + Vector3.down * _heightDown, _sphereRadius);
		}
		private void OnTriggerEnter(Collider other)
		{
			platform.TriggerPart(other.GetComponent<BallController>().BallStrenght);
		}

		private void Update()
		{

			if (Physics.SphereCast(transform.position, _sphereRadius, Vector3.down * _heightDown, out RaycastHit hitInfo, _maxDistance, mask))
			{

				if (hitInfo.collider != null)
				{
					float strenght = hitInfo.collider.gameObject.GetComponent<BallController>().BallStrenght;

					if(strenght >= 100 && !_isNotDestroy || strenght >= 200)
					{
						if(_isNotDestroy)
						{
							EventManager.EventBlockPartDestroyed?.Invoke();
						}
						platform.DestroyPlatform();
					}	
				}
			}

		}

		public void DisablePart(Material material)
		{
			_isNotDestroy = true;
			MeshCollider[] meshCollider = GetComponents<MeshCollider>();
			foreach(MeshCollider collider in meshCollider)
			{
				collider.isTrigger = false;
			}

			GetComponent<MeshRenderer>().material = material;
		}
	}
}
