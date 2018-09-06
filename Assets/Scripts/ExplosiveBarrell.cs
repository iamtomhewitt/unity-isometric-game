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
			// Return from the loop if we are evaluating ourselves
			if (objectsInRange[i].gameObject == this.gameObject)
				break;

			if (objectsInRange[i].GetComponent<HealthController>())
				objectsInRange[i].GetComponent<HealthController>().RemoveHealth(2);

			if (objectsInRange[i].GetComponent<ExplosiveBarrell>())
				objectsInRange[i].GetComponent<ExplosiveBarrell>().Destroy();

			if (objectsInRange[i].GetComponent<Crate>())
				objectsInRange[i].GetComponent<Crate>().Destroy();
		}
			Destroy(this.gameObject);
	}
}
