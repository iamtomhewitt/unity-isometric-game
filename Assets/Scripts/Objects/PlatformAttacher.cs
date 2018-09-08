using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Simple script for attaching the player to a platform when jumped on.
/// When the player jumps off, they are no longer attached.
/// </summary>
public class PlatformAttacher : MonoBehaviour
{
	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			other.transform.parent = transform;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			other.transform.parent = null;
		}
	}
}
