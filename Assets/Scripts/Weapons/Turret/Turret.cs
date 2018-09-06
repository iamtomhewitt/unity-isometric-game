using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public float attackRange;
	public float fireRate;

	public Transform bulletSpawn;
	public Transform turretHead;

	public BulletPool bulletPool;

	private float cooldown;

	private Transform target;

	private void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		cooldown -= Time.deltaTime;

		float distance = Vector3.Distance(transform.position, target.position);

		if (distance <= attackRange)
		{
			LookAtPlayer();

			if (cooldown <= 0f)
			{
				Shoot();
			}
		}
	}

	private void Shoot()
	{
		cooldown = fireRate;
		bulletPool.UseBulletFromPool(bulletSpawn);
	}

	private void LookAtPlayer()
	{
		// Rotate smoothly to look at the player
		Vector3 lookRotation = (target.position - transform.position);
		//lookRotation.z = 0f;
		lookRotation.y = 0f;
		turretHead.rotation = Quaternion.Slerp(turretHead.rotation, Quaternion.LookRotation(lookRotation), 2f * Time.deltaTime);
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == Tags.BULLET)
		{
			GetComponent<TurretHealthController>().RemoveHealth(1f);
		}
	}
}