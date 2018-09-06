using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	public class DialogueManager : MonoBehaviour
	{
		private Queue<string> sentences;

		void Start()
		{
			sentences = new Queue<string>();
		}

		public void StartDialogue(Conversation dialogue)
		{
			DialogueUI.instance.Show();
			DialogueUI.instance.icon.sprite = dialogue.icon;
			DialogueUI.instance.characterName.text = dialogue.characterName;

			sentences.Clear();

			foreach (string sentence in dialogue.sentences)
			{
				sentences.Enqueue(sentence);
			}

			DisplayNextSentence();
		}

		public void DisplayNextSentence()
		{
			if (sentences.Count == 0)
			{
				EndDialogue();
				return;
			}

			string sentence = sentences.Dequeue();
			StopAllCoroutines();
			StartCoroutine(TypeSentence(sentence));
		}

		IEnumerator TypeSentence(string sentence)
		{
			DialogueUI.instance.characterText.text = "";
			foreach (char letter in sentence.ToCharArray())
			{
				DialogueUI.instance.characterText.text += letter;
				yield return null;
			}
		}

		public void EndDialogue()
		{
			DialogueUI.instance.Hide();
		}
	}
}
[System.Serializable]
public class Conversation
{
	public Sprite icon;
	public string characterName;

	[TextArea(0, 10)]
	public string[] sentences;
}

