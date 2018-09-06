using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Minions
{
	public abstract class Minion : MonoBehaviour
	{
		[Header("Base Settings")]
		public float runSpeed;

		public ParticleSystem hitEffect;
		public ParticleSystem feetDust;
		public GameObject alertSprite;
		public Text debug;

		protected Transform target;
		protected MinionAnimationController animationController;
		protected Rigidbody rb;
		protected NavMeshAgent agent;

		protected enum STATE { Alerted, Chasing, Attacking, Idle };
		protected STATE state;

		public abstract IEnumerator Alert();

		public abstract IEnumerator Attack();
		
		public void KnockBack(Vector3 direction)
		{
			rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
			rb.AddForce(direction * -7.5f, ForceMode.Impulse);
		}

		public void LookAtPlayer()
		{
			// Rotate smoothly to look at the player
			Vector3 lookRotation = (target.position - transform.position);
			lookRotation.y = 0f;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), 2f * Time.deltaTime);
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.tag == Tags.BULLET)
			{
				GetComponent<HealthController>().RemoveHealth(1);
				hitEffect.Play();
			}
		}
	}
}
