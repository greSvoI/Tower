using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDestroy
{
	public class Ball : MonoBehaviour
	{
		[SerializeField] private int _ballStrength = 50;
		[SerializeField] Rigidbody rigidBody;
		[SerializeField] private float _force = 5f;
		[SerializeField] private float _rayLenght = 0f;
		[SerializeField] private LayerMask layer;
		public int BallStrenght => _ballStrength;
		[SerializeField] private bool jump= false;
		private void Start()
		{
			rigidBody = GetComponent<Rigidbody>();
		}
		public void Jump(float force)
		{
			rigidBody.velocity = Vector3.zero;
			rigidBody.AddForce(Vector3.up *  _force);
		}
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position + Vector3.down * 0.05f, _rayLenght);
		}
		private void FixedUpdate()
		{

			if (Physics.CheckSphere(transform.position + Vector3.down * 0.05f, _rayLenght, layer))
			{
				rigidBody.velocity = Vector3.zero;
				rigidBody.AddForce(Vector3.up * _force,ForceMode.Impulse);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			//Debug.Log("collision enter ball");
			//Jump(_force);
		}
		private void OnCollisionStay(Collision collision)
		{
			//Debug.Log("collision stay ball");
		}
		private void OnCollisionExit(Collision collision)
		{
			//Debug.Log("collision exit ball");
		}
		private void OnTriggerEnter(Collider other)
		{
			//Debug.Log("trigger enter ball");
		}
		private void OnTriggerStay(Collider other)
		{
			//Debug.Log("trigger stay ball");
		}
		private void OnTriggerExit(Collider other)
		{
			//Debug.Log("trigger exit ball");
		}
	}

}