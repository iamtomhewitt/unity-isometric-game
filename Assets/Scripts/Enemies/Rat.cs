//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//namespace Enemies
//{
//	public class Rat : Enemy
//	{
//		private bool canAttack;
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
//				StartCoroutine(Alert());
//			}
//			else
//			{
//				agent.SetDestination(this.transform.position);
//				this.animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
//				this.alerted = false;
//			}
//		}

//		public override IEnumerator Alert()
//		{
//			if (!this.alerted)
//			{
//				this.alerted = true;
//				yield return new WaitForSeconds(.75f);
//				canAttack = true;
//			}
//			if (canAttack)
//			{
//				Attack();
//			}
//		}

//		private void Attack()
//		{
//			agent.SetDestination(this.target.position);
//			this.animationController.ChangeAnimation(Constants.ANIMATION_RUN);
//		}
//	}
//}
