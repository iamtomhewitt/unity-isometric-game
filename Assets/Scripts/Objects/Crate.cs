using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
/// <summary>
/// A crate just gets destroyed and adds score to the player. Chests are the ones that give items.
/// </summary>
public class Crate : DestroyableObject
{
	public ParticleSystem destroyEffect;

	public override void Destroy()
	{
		Instantiate(destroyEffect, transform.position, Quaternion.identity);
		PlayerScoreController.instance.AddScore(10);
		CameraController.instance.ShakeCamera(.25f, .25f);
		Destroy(this.gameObject);
	}
}
