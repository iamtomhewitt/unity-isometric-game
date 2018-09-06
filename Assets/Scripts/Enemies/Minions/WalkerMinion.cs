using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Minions
{
	public class WalkerMinion : Minion
	{
		[Header("Walker Settings")]
		public float attackRange;
		public float alertRange;

		private float originalSpeed;

		private bool canLookAtPlayer = true;
		private bool doingRoutine;

		private void Start()
		{
			animationController = GetComponent<MinionAnimationController>();
			rb					= GetComponent<Rigidbody>();
			target				= GameObject.FindGameObjectWithTag("Player").transform;

			originalSpeed		= runSpeed;
			originalSpeed		+= Random.Range(-0.5f, 0.5f);
			runSpeed			= 0f;
		}

		private void Update()
		{
			debug.text = state.ToString();
			float distance = Vector3.Distance(transform.position, target.position);
			
			transform.position += transform.forward * runSpeed * Time.deltaTime;

			if (canLookAtPlayer)LookAtPlayer();

			if (!doingRoutine)
			{
				if (distance < attackRange)
				{
					StartCoroutine(Attack());
				}
				else
				{
					state = STATE.Idle;
					animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
					feetDust.Stop();
				}
			}
		}
		
		public override IEnumerator Alert()
		{
			state = STATE.Alerted;
			alertSprite.SetActive(true);
			yield return new WaitForSeconds(1.5f + Random.Range(-0.25f, 0.2f));
			alertSprite.SetActive(false);
		}

		public override IEnumerator Attack()
		{
			doingRoutine = true;
			yield return StartCoroutine(Alert());
			canLookAtPlayer = false;
			animationController.ChangeAnimation(Constants.ANIMATION_ATTACK);
			feetDust.Play();
			state = STATE.Attacking;
			runSpeed = originalSpeed;
			yield return new WaitForSeconds(3f);
			runSpeed = 0f;
			animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
			feetDust.Stop();
			yield return new WaitForSeconds(1f);
			doingRoutine = false;
			canLookAtPlayer = true;
			state = STATE.Idle;
		}		
	}
}