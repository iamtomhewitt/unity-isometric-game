using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
	public int score;

	public static PlayerScoreController instance;

	private void Start()
	{
		instance = this;
	}

	public void AddScore(int amount)
	{
		score += amount;
		PlayerUIController.instance.scoreText.text = score.ToString("00000");
	}
}
