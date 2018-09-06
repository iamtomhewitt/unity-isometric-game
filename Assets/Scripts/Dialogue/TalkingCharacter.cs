using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	[RequireComponent(typeof(DialogueManager))]
	public class TalkingCharacter : MonoBehaviour
	{
		public Conversation dialogue;

		private DialogueManager manager;

		private bool playerInRange;
		private bool pressedDPad = false;

		private const int LEFT = -1;
		private const int RIGHT = 1;
		private const int NONE = 0;

		private void Start()
		{
			manager = GetComponent<DialogueManager>();
		}

		private void Update()
		{
			if (Input.GetAxis("XboxControllerDPadX") == RIGHT && playerInRange)
			{
				if (!pressedDPad)
				{
					manager.DisplayNextSentence();
					pressedDPad = true;
				}
			}
			if (Input.GetAxis("XboxControllerDPadX") == NONE)
				pressedDPad = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Player")
			{
				playerInRange = true;
				manager.StartDialogue(dialogue);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.tag == "Player")
			{
				playerInRange = false;
				manager.EndDialogue();
			}
		}
	}
}



