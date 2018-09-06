using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class ExplosiveBarrell : DestroyableObject
{
	public ParticleSystem destroyEffect;
	public float damageRadius;

	public override void Destroy()
	{
		Instantiate(destroyEffect, transform.position, Quaternion.identity);
		CameraController.instance.ShakeCamera(.25f, .25f);

		Collider[] objectsInRange = Physics.OverlapSphere(transform.position, damageRadius);

		for (int i = 0; i < objectsInRange.Length; i++)
		{
			if (objectsInRange[i].GetComponent<HealthController>())
			{
				objectsInRange[i].GetComponent<HealthController>().RemoveHealth(2);
			}
		}

		Destroy(this.gameObject);
	}
}
