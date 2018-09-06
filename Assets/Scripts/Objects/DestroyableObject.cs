using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestroyableObject : MonoBehaviour
{
	public abstract void Destroy();

	public void OnCollisionEnter(Collision other)
	{
		switch (other.gameObject.tag)
		{
			case Tags.BULLET:
				Destroy();
				break;

			case Tags.PLAYER:
				if (!other.gameObject.GetComponent<PlayerCollisionController>().onGround)
					Destroy();
				break;
		}

	}
}
