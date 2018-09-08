using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class Dynamite : DestroyableObject
{
	public float fuseTime;
	public float damageRadius;

	public GameObject explosion;

	private void Start()
	{
		Destroy();
	}

	public override void Destroy()
	{
		StartCoroutine(DelayedDestroy());
	}

	private IEnumerator DelayedDestroy()
	{
		yield return new WaitForSeconds(fuseTime);

		Collider[] objectsInRange = Physics.OverlapSphere(transform.position, damageRadius);

		for (int i = 0; i < objectsInRange.Length; i++)
		{
			GameObject obj = objectsInRange[i].gameObject;

			if (obj.GetComponent<HealthController>())
				obj.GetComponent<HealthController>().RemoveHealth(5);

			print("TODO: Explode walls.");
		}

		Instantiate(explosion, transform.position, Quaternion.identity);

		CameraController.instance.ShakeCamera(.5f, 1f);

		Destroy(gameObject);
	}
}
