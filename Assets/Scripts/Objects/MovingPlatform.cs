using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public GameObject platform;

	public Transform startPosition;
	public Transform endPosition;

	private Transform targetPosition;

	public float movingSpeed;

	private void Start()
	{
		ChangeTargetPosition(endPosition);
	}

	private void Update()
	{
		if (platform.transform.position == startPosition.position)
			ChangeTargetPosition(endPosition);

		else if (platform.transform.position == endPosition.position)
			ChangeTargetPosition(startPosition);

		platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPosition.position, movingSpeed * Time.deltaTime);		
	}

	private void ChangeTargetPosition(Transform pos)
	{
		targetPosition = pos;
	}
}
