using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public Transform respawnPoint;

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void Animate()
	{
		//animator.Play("Checkpoint");
		print("TODO: Animate the checkpoint.");
	}
}
