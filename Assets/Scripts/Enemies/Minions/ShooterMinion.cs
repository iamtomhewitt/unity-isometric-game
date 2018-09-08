using Minions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Minions
{
	public class ShooterMinion : Minion
	{
		[Header("Range Settings")]
		public float walkRange;
		public float attackRange;

		[Header("Bullet Settings")]
		public GameObject bullet;
		public Transform bulletSpawn;
		public float fireRate;
		public int maxBullets;

		private float cooldown;
		private int bulletIndex = 0;

		[HideInInspector]
		public List<GameObject> bulletPool;

		private bool attackCoroutineRunning;

		void Start()
		{
			InitialiseBulletPool();

			animationController = GetComponent<MinionAnimationController>();
			rb = GetComponent<Rigidbody>();
			agent = GetComponent<NavMeshAgent>();
			target = GameObject.FindGameObjectWithTag("Player").transform;

			runSpeed += Random.Range(-0.5f, 0.5f);
			fireRate += Random.Range(-0.25f, 0.25f);
			cooldown = fireRate;
		}

		private void Update()
		{
			LookAtPlayer();

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

			Stop();

			cooldown = fireRate;

			GameObject newBullet = bulletPool[bulletIndex];

			newBullet.SetActive(true);
			newBullet.transform.position = bulletSpawn.position;
			newBullet.transform.rotation = bulletSpawn.rotation;
			newBullet.GetComponent<Bullet>().DeactivateAfterLifetime();

			bulletIndex++;

			if (bulletIndex >= bulletPool.Count)
				bulletIndex = 0;

			animationController.ChangeAnimation(Constants.ANIMATION_SHOOT);

			yield return new WaitForSeconds(animationController.GetAnimationLength(Constants.ANIMATION_SHOOT));

			animationController.ChangeAnimation(Constants.ANIMATION_IDLE);

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
			animationController.ChangeAnimation(Constants.ANIMATION_RUN);
			if (!feetDust.isPlaying)
				feetDust.Play();
		}

		private void InitialiseBulletPool()
		{
			for (int i = 0; i < maxBullets; i++)
			{
				GameObject b = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
				b.SetActive(false);
				b.transform.parent = GameObject.Find("Bullets").transform;
				bulletPool.Add(b);
			}
		}
	}
}