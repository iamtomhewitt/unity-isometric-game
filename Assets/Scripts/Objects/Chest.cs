using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	public GameObject rareItem;
	public GameObject commonItem;

	public GameObject openSprite;

	public Transform spawn;

	private Animator animator;

	public float spawnPower;

	private bool playerInRange;
	private bool opened;

	private void Start()
	{
		animator = GetComponent<Animator>();
		openSprite.SetActive(false);
	}

	private void Update()
	{
		if (playerInRange && !opened && Input.GetButtonDown(XboxController.xboxXButton))
		{
			OpenChest();
		}
	}

	void OpenChest()
	{
		animator.Play("Chest Open");
		opened = true;
		openSprite.SetActive(false);
	}

	/// <summary>
	/// Called from the Unity animator.
	/// </summary>
	public void SpawnItem()
	{
		int number = Random.Range(0, 100);

		GameObject item;

		if (number <= 85)
			item = Instantiate(commonItem, spawn.position, spawn.rotation) as GameObject;
		else
			item = Instantiate(rareItem, spawn.position, spawn.rotation) as GameObject;

		item.GetComponent<Rigidbody>().AddForce(spawn.forward * spawnPower, ForceMode.Impulse);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			if (!opened)
				openSprite.SetActive(true);
			playerInRange = true;
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == Tags.PLAYER)
		{
			openSprite.SetActive(false);
			playerInRange = false;
		}
	}
}
