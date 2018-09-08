using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlockPlatform : MonoBehaviour
{
	public Transform startPosition;
	public Transform endPosition;

	public float moveSpeed;

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			StopAllCoroutines();
			StartCoroutine(MoveToPosition(endPosition));
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			StopAllCoroutines();
			StartCoroutine(MoveToPosition(startPosition));
		}
	}

	private IEnumerator MoveToPosition(Transform pos)
	{
		while (transform.position != pos.position)
		{
			transform.position = Vector3.MoveTowards(transform.position, pos.position, moveSpeed * Time.deltaTime);
			yield return null;
		}
	}
}
