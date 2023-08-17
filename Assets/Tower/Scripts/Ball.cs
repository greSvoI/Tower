using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDestroy
{
	public class Ball : MonoBehaviour
	{
		private int _ballStrength = 25;
		[SerializeField] Rigidbody rigidBody;
		[SerializeField] private float _force = 5f;
		[SerializeField] private float _rayLenght = 0f;
		[SerializeField] private LayerMask layer;
		public int BallStrenght => _ballStrength;

		private void Start()
		{
			rigidBody = GetComponent<Rigidbody>();
		}
		public void Jump(float force)
		{
			rigidBody.AddForce(Vector3.up *  _force,ForceMode.Impulse);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(transform.position, _rayLenght);
			Gizmos.DrawLine(transform.position, Vector3.down);
		}
		private void FixedUpdate()
		{			
			if(Physics.Raycast(transform.position,Vector3.down,_rayLenght))
			{
				if (Physics.CheckSphere(transform.position, _rayLenght, layer))
				{
					Jump(_force);
				}
			}
		}
		private void OnTriggerEnter(Collider other)
		{

				

			//if(!_jump)
			//{
			//	_jump = true;
			//	Debug.Log("Trigger " + other.tag);
			//	Jump(_force);
			//}
		}
		private void OnCollisionEnter(Collision collision)
		{
			//if (Physics.CheckSphere(transform.position, _rayLenght, layer))
			//{
			//	Jump(_force);
			//}


			//if(collision.gameObject.layer == LayerMask.NameToLayer("PlatformPart"))
			//{
			//	collision.gameObject.GetComponent<MeshRenderer>().enabled = false;

			//	rigidBody.AddForce(Vector3.up * _force, ForceMode.Impulse);
			//}
		}
	}

}