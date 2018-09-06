using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

namespace Player
{
	public class PlayerHealthController : HealthController
	{
		public List<MeshRenderer> bodyParts;
		public int lives = 3;

		private Color[] originalColours;

		private void Start()
		{
			healthbar.maxValue = health;
			healthbar.value = health;

			AddBodyPart(transform);

			originalColours = new Color[bodyParts.Count];

			for (int i = 0; i < bodyParts.Count; i++)
				originalColours[i] = bodyParts[i].material.color;
		}

		public override void Die()
		{
			if (lives > 0)
				GetComponent<PlayerCheckpointController>().RespawnAtCheckPoint();
			else
				print("TODO: Game over.");
		}

		private IEnumerator Flash()
		{
			for (int i = 0; i < bodyParts.Count; i++)
			{
				bodyParts[i].material.color = Constants.DAMAGE_COLOUR;
			}

			yield return new WaitForSeconds(.25f);

			for (int i = 0; i < bodyParts.Count; i++)
			{
				bodyParts[i].material.color = originalColours[i];
			}
		}

		public override void RemoveHealthEffects()
		{
			StartCoroutine(Flash());
		}

		private void AddBodyPart(Transform t)
		{
			foreach (Transform child in t)
			{
				if (child.GetComponent<MeshRenderer>() != null)
				{
					bodyParts.Add(child.GetComponent<MeshRenderer>());
				}

				AddBodyPart(child);
			}
		}
	}
}
