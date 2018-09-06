using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
	public class Weapon : MonoBehaviour
	{
		public string weaponName;
		public GameObject projectile;
		public Transform projectileSpawn;
		public Light muzzleLight;
		public ParticleSystem particles;

		[HideInInspector]
		public List<GameObject> bulletPool;

		public int maxBullets;

		public float fireRate;

		private float cooldown;
		private int bulletIndex = 0;

		public void Start()
		{
			cooldown = fireRate;
			InitialiseBulletPool();
			muzzleLight.enabled = false;
		}

		public void Shoot()
		{
			if (cooldown <= 0f)
			{
				cooldown = fireRate;
				GameObject newBullet = bulletPool[bulletIndex];

				newBullet.SetActive(true);
				newBullet.transform.position = projectileSpawn.position;
				newBullet.transform.rotation = projectileSpawn.rotation;
				newBullet.transform.Rotate(0f, Random.Range(-20f, 20f), 0f); // add some bullet spread
				newBullet.GetComponent<Bullet>().DeactivateAfterLifetime();

				muzzleLight.enabled = true;
				particles.time = 0f;
				particles.Play();

				bulletIndex++;

				if (bulletIndex >= bulletPool.Count)
					bulletIndex = 0;
			}
			else
			{
				muzzleLight.enabled = false;
			}
		}

		private void Update()
		{
			cooldown -= Time.deltaTime;
		}

		private void InitialiseBulletPool()
		{
			for (int i = 0; i < maxBullets; i++)
			{
				GameObject b = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation) as GameObject;
				b.SetActive(false);
				b.transform.parent = GameObject.Find("Bullets").transform;
				bulletPool.Add(b);
			}
		}
	}
}
