using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerCheckpointController : MonoBehaviour
	{
		public Checkpoint lastCheckpoint;

		public void RespawnAtCheckPoint()
		{
			transform.position = lastCheckpoint.respawnPoint.position;
		}

		public void UpdateCheckpoint(Checkpoint newCheckpoint)
		{
			lastCheckpoint = newCheckpoint;
		}
	}
}
