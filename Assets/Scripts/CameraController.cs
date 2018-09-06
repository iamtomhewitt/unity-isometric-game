using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
    public Transform player;
    public Vector3 offset;
    public float moveSpeed;

	public static CameraController instance;

	private void Awake()
	{
		instance = this;
	}

	void Update()
    {
        transform.position = Vector3.Slerp(transform.position, player.position + offset, moveSpeed * Time.deltaTime);
    }

	public void ShakeCamera(float duration, float intensity)
	{
		StartCoroutine(Shake(duration, intensity));
	}

	private IEnumerator Shake(float duration, float intensity)
	{
		float timer = duration;

		do
		{
			float x = Random.Range(-intensity, intensity);
			float y = Random.Range(-intensity, intensity);
			transform.position += new Vector3(x, y, 0f);
			timer -= Time.deltaTime;
			yield return null;
		}
		while (timer >= 0f);
	}
}
