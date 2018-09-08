using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Minions
{
	public class WalkerMinion : Minion
	{
		[Header("Range Settings")]
		public float walkRange;
		public float attackRange;
		
		private bool attackCoroutineRunning;

		private void Start()
		{
			animationController = GetComponent<MinionAnimationController>();
			rb					= GetComponent<Rigidbody>();
			target				= GameObject.FindGameObjectWithTag("Player").transform;

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

		public override IEnumerator Alert()
		{
			// Not used
			throw new System.NotImplementedException();
		}

		public override IEnumerator Attack()
		{
			attackCoroutineRunning = true;

			float wait = Random.Range(0.75f, 1.5f);

			// Look at the player instantly (the nav mesh handles the smoothness)
			Vector3 lookRotation = (target.position - transform.position);
			lookRotation.y = 0f;
			transform.rotation = Quaternion.LookRotation(lookRotation);

			// Get a position that we should chsrge towards (in a straight line)
			Vector3 attackPosition = new Vector3(target.position.x, target.position.y, target.position.z) + (transform.forward * 2f);
						
			// Save the original speed;
			float savedSpeed = agent.speed;

			Stop();

			alertSprite.SetActive(true);

			// Wait to attack
			yield return new WaitForSeconds(wait);

			// Now lunge forward (our attack)
			agent.speed = savedSpeed * 5;
			MoveTo(attackPosition);

			// Wait for 1 second to stop
			yield return new WaitForSeconds(wait);

			alertSprite.SetActive(false);
			Stop();

			yield return new WaitForSeconds(wait);

			// Apply original speed and move towards player like normal
			agent.speed = savedSpeed;

			MoveTo(target.position);

			attackCoroutineRunning = false;
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
			animationController.ChangeAnimation(Constants.ANIMATION_ATTACK);
			if (!feetDust.isPlaying)
				feetDust.Play();
		}
	}
}