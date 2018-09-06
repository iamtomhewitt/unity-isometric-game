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
		[Header("Hammerhead Settings")]
		public float attackRange;
		public float chaseRange;
		public float alertRange;

		public ParticleSystem hammerDustCloud;

		private void Start()
		{
			agent				= GetComponent<NavMeshAgent>();
			animationController = GetComponent<MinionAnimationController>();
			rb					= GetComponent<Rigidbody>();
			target				= GameObject.FindGameObjectWithTag("Player").transform;

			runSpeed += Random.Range(-0.5f, 0.5f);
			agent.speed = runSpeed;
			state = STATE.Idle;
		}

		private void Update()
		{
			debug.text = state.ToString();
			float distance = Vector3.Distance(transform.position, target.position);

			// In ALERT range
			if (distance < alertRange && distance > chaseRange)
			{
				if (state == STATE.Attacking || state == STATE.Alerted)
					return;

				LookAtPlayer();
				state = STATE.Alerted;
				agent.SetDestination(transform.position);
				animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
			}

			// In CHASING range
			else if (distance < chaseRange && distance > attackRange)
			{
				if (state == STATE.Attacking)
					return;

				LookAtPlayer();
				state = STATE.Chasing;
				agent.SetDestination(target.position);
				animationController.ChangeAnimation(Constants.ANIMATION_RUN);
			}

			// In ATTACKING range
			else if (distance < attackRange && state != STATE.Attacking)
			{
				LookAtPlayer();
				agent.SetDestination(transform.position);
				StartCoroutine(Attack());
			}

			// Out of range
			else if (distance > alertRange)
			{
				if (state == STATE.Attacking)
					return;

				state = STATE.Idle;
				agent.SetDestination(transform.position);
				animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
			}
		}

		public override IEnumerator Attack()
		{
			// Cache the current state so we can return to it
			STATE s = state;
			state = STATE.Attacking;

			// Offset so multiple enemies do not all attack at the same time
			yield return new WaitForSeconds(Random.Range(0f, .5f));
			animationController.ChangeAnimation(Constants.ANIMATION_ATTACK);
			yield return new WaitForSeconds(animationController.GetAnimationLength(Constants.ANIMATION_ATTACK));
			yield return new WaitForSeconds(1f);
			state = s;
		}

		/// <summary>
		/// Called from the Animator so it is in time with the Hammer animation.
		/// </summary>
		public void HammerAttack()
		{
			hammerDustCloud.Play();

			Collider[] objectsInRange = Physics.OverlapBox(transform.position, new Vector3(2f, .5f, 2f));

			for (int i = 0; i < objectsInRange.Length; i++)
			{
				if (objectsInRange[i].tag == "Player")
					objectsInRange[i].GetComponent<HealthController>().RemoveHealth(2);
			}
		}

		public override IEnumerator Alert()
		{
			// Unused
			yield return null;
		}
	}
}