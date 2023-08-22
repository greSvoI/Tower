using System.Collections;
using System.Collections.Generic;
using TowerDestroy;
using UnityEngine;

namespace TowerDestroy
{
	public class Part : MonoBehaviour
	{
		private PlatformPart part;
		private bool _isNotDestroy = false;
		[SerializeField] private LayerMask mask;
		private void Start()
		{
			part = GetComponentInParent<PlatformPart>();
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position + Vector3.down * 0.5f, 0.2f);
		}
		private void OnTriggerEnter(Collider other)
		{
			try
			{
				part.TriggerPart(other.GetComponent<BallController>().BallStrenght);
			}
			catch (System.Exception)
			{

				Debug.Log(GetComponentInParent<GameObject>().name);
			}
		}

		private void Update()
		{

			if (Physics.SphereCast(transform.position, 0.2f, Vector3.down * 0.5f, out RaycastHit hitInfo, 0.5f, mask))
			{

				if (hitInfo.collider != null)
				{
					float strenght = hitInfo.collider.gameObject.GetComponent<BallController>().BallStrenght;

					if(strenght >= 100 && !_isNotDestroy || strenght >= 200)
						part.DestroyPlatform();
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
