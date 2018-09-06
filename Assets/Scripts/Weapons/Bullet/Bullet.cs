using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float lifetime;

	private void Start()
	{
		StartCoroutine(SetActiveAfterTime(false, lifetime));
	}

	private void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	public void DeactivateAfterLifetime()
	{
		StopAllCoroutines();
		StartCoroutine(SetActiveAfterTime(false, lifetime));
	}

	private IEnumerator SetActiveAfterTime(bool active, float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(active);
	}

	private void OnCollisionEnter(Collision collision)
	{
		StartCoroutine(SetActiveAfterTime(false, 0f));
	}
}
