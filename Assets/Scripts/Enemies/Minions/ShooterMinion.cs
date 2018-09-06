using Minions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Minions
{
	public class ShooterMinion : Minion
	{
		[Header("Shooter Settings")]
		public float chaseRange;
		public float attackRange;

		public GameObject bullet;
		public Transform bulletSpawn;
		public float fireRate;
		public int maxBullets;

		private float cooldown;
		private int bulletIndex = 0;

		[HideInInspector]
		public List<GameObject> bulletPool;

		void Start()
		{
			InitialiseBulletPool();

			animationController = GetComponent<MinionAnimationController>();
			rb					= GetComponent<Rigidbody>();
			agent				= GetComponent<NavMeshAgent>();
			target				= GameObject.FindGameObjectWithTag("Player").transform;

			runSpeed			+= Random.Range(-0.5f, 0.5f);
			fireRate			+= Random.Range(-0.25f, 0.25f);
			cooldown			= fireRate;
		}

		void Update()
		{
			float distance = Vector3.Distance(transform.position, target.position);
			cooldown -= Time.deltaTime;

			if (distance > chaseRange && distance < chaseRange + 1f)
			{
				alertSprite.SetActive(true);
				LookAtPlayer();
			}
			else
				alertSprite.SetActive(false);

			// Chase
			if (distance < chaseRange && distance > attackRange)
			{
				LookAtPlayer();
				agent.SetDestination(target.position);
				animationController.ChangeAnimation(Constants.ANIMATION_RUN);
			}

			// Attack
			else if (distance < attackRange)
			{
				LookAtPlayer();

				if (cooldown <= 0f)
				{
					StartCoroutine(Attack());
				}
			}

			// Idle
			else
			{
				animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
				agent.SetDestination(transform.position);
				feetDust.Stop();
			}
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

		public override IEnumerator Alert()
		{
			alertSprite.SetActive(true);
			yield return new WaitForSeconds(1.5f + Random.Range(-0.25f, 0.2f));
			alertSprite.SetActive(false);
		}

		public override IEnumerator Attack()
		{
			agent.SetDestination(transform.position);

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
		}
	}
}