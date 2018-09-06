using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
	public class DialogueUI : MonoBehaviour
	{
		public Image icon;
		public Text characterName;
		public Text characterText;

		public static DialogueUI instance;

		void Awake()
		{
			if (instance)
			{
				DestroyImmediate(gameObject);
			}
			else
			{
				DontDestroyOnLoad(gameObject);
				instance = this;
			}
		}

		private void Start()
		{
			Hide();
		}

		public void Show()
		{
			this.gameObject.SetActive(true);
		}

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}
	}
}