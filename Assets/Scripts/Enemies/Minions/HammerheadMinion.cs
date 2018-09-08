using Health;
using Minions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Minions
{
	public class HammerheadMinion : Minion
	{
		[Header("Range Settings")]
		public float walkRange;
		public float attackRange;

		[Header("Hammer Settings")]
		public Transform hammerHitPoint;
		public float hammerHitRadius;
		public ParticleSystem hammerDustCloud;

		private bool attackCoroutineRunning;

		private void Start()
		{
			animationController = GetComponent<MinionAnimationController>();
			rb = GetComponent<Rigidbody>();
			target = GameObject.FindGameObjectWithTag("Player").transform;

			runSpeed += Random.Range(-0.25f, 0.25f);

			agent = GetComponent<NavMeshAgent>();
			agent.isStopped = true;
			agent.speed = runSpeed;
		}

		private void Update()
		{
			if (attackCoroutineRunning)
				return;

			if (InAttackRange())
				StartCoroutine(Attack());

			else if (InWalkingRange())
				MoveTo(target.position);

			else
				Stop();
		}

		private bool InWalkingRange()
		{
			return (Vector3.Distance(transform.position, target.transform.position) <= walkRange);
		}

		private bool InAttackRange()
		{
			return (Vector3.Distance(transform.position, target.transform.position) <= attackRange);
		}

		private void Stop()
		{
			agent.isStopped = true;
			animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
			feetDust.Stop();
		}

		private void MoveTo(Vector3 position)
		{
			agent.isStopped = false;
			agent.SetDestination(position);
			animationController.ChangeAnimation(Constants.ANIMATION_RUN);
			if (!feetDust.isPlaying)
				feetDust.Play();
		}

		public override IEnumerator Alert()
		{
			throw new System.NotImplementedException();
		}

		public override IEnumerator Attack()
		{
			attackCoroutineRunning = true;

			Stop();

			// Look at the player instantly (the nav mesh handles the smoothness)
			Vector3 lookRotation = (target.position - transform.position);
			lookRotation.y = 0f;
			transform.rotation = Quaternion.LookRotation(lookRotation);

			// Offset so multiple enemies do not all attack at the same time
			yield return new WaitForSeconds(Random.Range(0.5f, 1f));

			animationController.ChangeAnimation(Constants.ANIMATION_ATTACK);
			yield return new WaitForSeconds(animationController.GetAnimationLength(Constants.ANIMATION_ATTACK));

			MoveTo(target.position);

			attackCoroutineRunning = false;
		}

		/// <summary>
		/// Called from the Animator so it is in time with the Hammer animation.
		/// </summary>
		public void HammerAttack()
		{
			hammerDustCloud.Play();

			Collider[] objectsInRange = Physics.OverlapCapsule(hammerHitPoint.position, hammerHitPoint.position + (Vector3.up / 10), hammerHitRadius);

			for (int i = 0; i < objectsInRange.Length; i++)
			{
				if (objectsInRange[i].tag == "Player")
					objectsInRange[i].GetComponent<HealthController>().RemoveHealth(2);
			}
		}
	}
}