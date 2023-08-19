using System.Collections;
using System.Collections.Generic;
using TowerDestroy;
using UnityEngine;

namespace TowerDestroy
{
	public class Part : MonoBehaviour
	{
		private PlatformPart part;
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
			part.TriggerPart(other.GetComponent<BallController>().BallStrenght);
		}
		private void Update()
		{
			//if (Physics.SphereCast(transform.position, 0.2f, Vector3.down * 0.5f, out RaycastHit hitInfo,0.5f,mask))
			//{
			//	if (hitInfo.collider != null)
			//		if (hitInfo.collider.gameObject.GetComponent<BallController>().BallStrenght > 100)
			//		{
			//			part.DestroyPlatform();
			//		}
			//}
		}
		public void DisablePart(Material material)
		{
			MeshCollider[] meshCollider = GetComponents<MeshCollider>();
			foreach(MeshCollider collider in meshCollider)
			{
				collider.isTrigger = false;
			}

			GetComponent<MeshRenderer>().material = material;
		}
	}
}
