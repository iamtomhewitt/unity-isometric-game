using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
	public class MinionHealthController : HealthController
	{
		public GameObject gibs;
		public MeshRenderer[] bodyParts;

		private Color[] originalColours;

		private void Start()
		{
			healthbar.maxValue = health;
			healthbar.value = health;

			originalColours = new Color[bodyParts.Length];

			for (int i = 0; i < bodyParts.Length; i++)
				originalColours[i] = bodyParts[i].material.color;
		}

		private IEnumerator Flash()
		{
			for (int i = 0; i < bodyParts.Length; i++)
			{
				bodyParts[i].material.color = Constants.DAMAGE_COLOUR;
			}

			yield return new WaitForSeconds(.25f);

			for (int i = 0; i < bodyParts.Length; i++)
			{
				bodyParts[i].material.color = originalColours[i];
			}
		}

		public override void Die()
		{
			CameraController.instance.ShakeCamera(.15f, .2f);
			Instantiate(deathExplosion, transform.position, Quaternion.identity);
			Instantiate(gibs, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}

		public override void RemoveHealthEffects()
		{
			StartCoroutine(Flash());
		}
	}
}
