using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
	public class PlayerUIController : MonoBehaviour
	{
		public Slider groundPoundSlider;

		public Text scoreText;
		public Text livesText;

		private PlayerController playerController;

		private float full;

		private bool groundPoundSliderFull;

		public static PlayerUIController instance;

		private void Start()
		{
			playerController = GetComponent<PlayerController>();
			full = groundPoundSlider.value;
			groundPoundSliderFull = true;

			instance = this;
		}

		public bool IsGroundPoundSliderFull()
		{
			return groundPoundSliderFull;
		}

		public void UpdateScoreText(string msg)
		{
			scoreText.text = msg;
		}

		public void UpdateLivesText(string msg)
		{
			livesText.text = msg;
		}

		public void RechargeGroundPound()
		{
			StartCoroutine(ChargeGroundPound());
		}

		private IEnumerator ChargeGroundPound()
		{
			// Reset to empty
			groundPoundSlider.value = 0f;
			groundPoundSliderFull = false;

			// Increase the slider
			do
			{
				groundPoundSlider.value += Time.deltaTime;
				yield return null;
			}
			while (groundPoundSlider.value < full);

			// Set to full
			groundPoundSlider.value = full;
			playerController.canGroundPound = true;
			groundPoundSliderFull = true;
		}
	}
}
