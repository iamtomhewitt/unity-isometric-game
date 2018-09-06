using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;
using UnityEngine.UI;

namespace Health
{
	public abstract class HealthController : MonoBehaviour
	{
		public float health;

		public Slider healthbar;
		public GameObject deathExplosion;

		public void RemoveHealth(float amount)
		{
			RemoveHealthEffects();

			health -= amount;
			healthbar.value = health;

			if (health <= 0)
			{
				Die();
			}
		}

		public abstract void RemoveHealthEffects();

		public abstract void Die();
	}
}
