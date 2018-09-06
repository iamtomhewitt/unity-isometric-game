using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Health;

public class TurretHealthController : HealthController
{
	private void Start()
	{
		healthbar.maxValue = health;
		healthbar.value = health;
	}

	public override void Die()
	{
		CameraController.instance.ShakeCamera(.35f, .3f);
		Instantiate(deathExplosion, transform.position + new Vector3(0f, .5f, 0f), Quaternion.identity);
		Destroy(this.gameObject);
	}

	public override void RemoveHealthEffects()
	{
		// Empty
	}
}
