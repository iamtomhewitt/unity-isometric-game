using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
	public class PlayerParticleController : MonoBehaviour
	{
		public ParticleSystem jumpDust;
		public ParticleSystem groundPound;
		public ParticleSystem feetDust;
		public ParticleSystem coinCollect;

		public void PlayJumpDust()
		{
			jumpDust.Play();
		}

		public void StopJumpDust()
		{
			jumpDust.Stop();
		}

		public void PlayGroundPound()
		{
			groundPound.Play();
		}

		public void StopGroundPund()
		{
			groundPound.Stop();
		}

		public void PlayFeetDust()
		{
			feetDust.Play();
		}

		public void StopFeetDust()
		{
			feetDust.Stop();
		}

		public void PlayCoinEffect(Vector3 position)
		{
			// Could there be a better way to do this?
			GameObject p = Instantiate(coinCollect.gameObject, position, Quaternion.identity) as GameObject;
			Destroy(p, 5f);
		}
	}
}
