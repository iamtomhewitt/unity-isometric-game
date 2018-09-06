using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A class that creates a certain number of bullets at runtime, that can be reused throughout the game. This stops
/// uncessary creation and deletion of objects from the scene - better performance.
/// </summary>
[System.Serializable]
public class BulletPool : MonoBehaviour
{
	public GameObject bullet;
	public int poolSize;

	private int index = 0;

	[HideInInspector]
	public List<GameObject> bulletPool;

	private void Start()
	{
		InitialiseBulletPool();
	}

	private void InitialiseBulletPool()
	{
		for (int i = 0; i < poolSize; i++)
		{
			GameObject b = Instantiate(bullet) as GameObject;
			b.SetActive(false);
			b.transform.parent = this.transform;
			bulletPool.Add(b);
		}
	}

	public GameObject UseBulletFromPool(Transform spawn)
	{
		GameObject newBullet = bulletPool[index];

		newBullet.SetActive(true);
		newBullet.transform.position = spawn.position;
		newBullet.transform.rotation = spawn.rotation;
		newBullet.transform.Rotate(0f, Random.Range(-5f, 5f), 0f);
		newBullet.GetComponent<Bullet>().DeactivateAfterLifetime();

		index++;

		if (index >= bulletPool.Count)
			index = 0;

		return newBullet;
	}
}
