//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Enemies
//{
//	public abstract class Enemy : MonoBehaviour
//	{
//		public float runSpeed;
//		public float attackRange;

//		protected Transform target;
//		protected AnimationController animationController;
//		protected Rigidbody rb;

//		protected bool alerted;

//		public Type type;
//		public enum Type { Rat, Zombie, Spider, Turret };

//		public abstract IEnumerator Alert();

//		public void KnockBack()
//		{
//			rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
//			rb.AddForce(transform.forward * -10f, ForceMode.Impulse);
//		}
//	}
//}