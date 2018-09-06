//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//namespace Enemies
//{
//	public class Zombie : Enemy
//	{
//		private bool stunned;
//		private NavMeshAgent agent;

//		private void Start()
//		{
//			this.animationController = GetComponent<AnimationController>();
//			this.rb = GetComponent<Rigidbody>();
//			this.target = GameObject.FindGameObjectWithTag("Player").transform;

//			agent = GetComponent<NavMeshAgent>();
//			agent.speed = this.runSpeed;
//		}

//		private void Update()
//		{
//			if (Vector3.Distance(transform.position, target.position) < this.attackRange)
//			{
//				Attack();
//			}
//			else
//			{
//				agent.SetDestination(this.transform.position);
//				if (!stunned)
//					animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
//			}
//		}

//		private void Attack()
//		{
//			if (!stunned)
//			{
//				agent.SetDestination(this.target.position);
//				animationController.ChangeAnimation(Constants.ANIMATION_RUN);
//			}
//		}

//		public IEnumerator Stun()
//		{
//			if (!stunned)
//			{
//				float originalSpeed = runSpeed;

//				stunned = true;
//				animationController.ChangeAnimation(Constants.ANIMATION_STUNNED);
//				runSpeed = 0f;

//				yield return new WaitForSeconds(3f);
//				animationController.ChangeAnimation(Constants.ANIMATION_UNSTUNNED);
//				yield return new WaitForSeconds(1f);

//				runSpeed = originalSpeed;
//				stunned = false;
//			}
//		}

//		public override IEnumerator Alert()
//		{
//			yield return null;
//		}
//	}
//}
