//using Enemies;
using Health;
using Minions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{
	public class PlayerCollisionController : MonoBehaviour
	{
		public Transform feetPosition;
		
		public bool onGround;

		private PlayerController playerController;
		private PlayerParticleController particleController;

		private bool drawGizmo;

		void Start()
		{
			playerController = GetComponent<PlayerController>();
			particleController = GetComponent<PlayerParticleController>();

			drawGizmo = true;
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			if (drawGizmo)
				Gizmos.DrawWireCube(feetPosition.position, new Vector3(playerController.groundPoundRadius, .5f, 5f));
		}

		private void OnTriggerEnter(Collider other)
		{
			print("Player has Trigger collided with: " + other.gameObject.tag);

			switch (other.gameObject.tag)
			{
				case Tags.COIN:
					GetComponent<PlayerParticleController>().PlayCoinEffect(other.transform.position);
					other.gameObject.SetActive(false);
					break;
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			print("Player has collided with: " + other.gameObject.tag);

			// If we are in the air when we collide with something
			if (!onGround)
			{
				switch (other.gameObject.tag)
				{
					case Tags.MINION:
						if (playerController.doingGroundPound)
						{
							other.gameObject.GetComponent<HealthController>().Die();
						}
						else
						{
							other.gameObject.GetComponent<HealthController>().RemoveHealth(1);
							playerController.Bounce();
						}
						break;

					case Tags.BARRELL:
						if (!playerController.doingGroundPound)
							playerController.Bounce();
						break;

					case Tags.CRATE:
						if (!playerController.doingGroundPound)
							playerController.Bounce();
						break;
				}
			}

			switch (other.gameObject.tag)
			{
				case Tags.GROUND:
					// If we hit the ground as we are doing a ground pound
					if (playerController.doingGroundPound)
					{
						// Shake the camera and play the dust
						particleController.PlayGroundPound();
						CameraController.instance.ShakeCamera(.4f, .5f);

						GetComponent<PlayerUIController>().RechargeGroundPound();

						// Inflict damage upon enemies in range
						//Collider[] objectsInRange = Physics.OverlapBox(feetPosition.position, new Vector3(playerController.groundPoundRadius, .5f, 5f));
						Collider[] objectsInRange = Physics.OverlapCapsule(feetPosition.position, feetPosition.position + Vector3.up / 10, playerController.groundPoundRadius);
						for (int i = 0; i < objectsInRange.Length; i++)
						{
							GameObject obj = objectsInRange[i].gameObject;

							switch (obj.tag)
							{
								case Tags.MINION:
									Vector3 direction = obj.transform.position - transform.position;
									direction = -direction.normalized;
									obj.gameObject.GetComponent<HealthController>().RemoveHealth(1);
									obj.gameObject.GetComponent<Minion>().KnockBack(direction);
									break;

								case Tags.BARRELL:
									obj.GetComponent<DestroyableObject>().Destroy();
									break;

								case Tags.CRATE:
									obj.GetComponent<DestroyableObject>().Destroy();
									break;
							}
						}
					}
					else
					{
						if (!onGround)
							particleController.PlayJumpDust();
					}
					onGround = true;
					particleController.PlayFeetDust();
					break;

				case Tags.MINION:
					if (onGround)
						GetComponent<HealthController>().RemoveHealth(1);
					break;

				case Tags.BULLET:
					GetComponent<HealthController>().RemoveHealth(1);
					break;

				case Tags.PICKUP_BLASTER:
					GetComponent<PlayerWeaponsController>().ChangeWeapon("Blaster");
					Destroy(other.gameObject);
					break;

				case Tags.PICKUP_CANNON:
					GetComponent<PlayerWeaponsController>().ChangeWeapon("Cannon");
					Destroy(other.gameObject);
					break;

				case Tags.PICKUP_DYNAMITE:
					GetComponent<PlayerWeaponsController>().hasDynamite = true;
					GetComponent<PlayerUIController>().SetDynamiteIcon(true);
					Destroy(other.gameObject);
					break;

				case Tags.CHECKPOINT:
					Checkpoint c = other.gameObject.GetComponent<Checkpoint>();
					GetComponent<PlayerCheckpointController>().UpdateCheckpoint(c);
					c.Animate();
					break;
			}
		}

		//void OnFeetCollisionEnter()
		//{
		//	RaycastHit hit;
		//	Debug.DrawRay(feetPosition.position, Vector3.down, Color.blue, .25f);
		//	if (Physics.SphereCast(feetPosition.position, 0.3f, Vector3.down, out hit, .25f))
		//	{
		//		GameObject other = hit.collider.gameObject;
		//		switch (hit.collider.tag)
		//		{
		//			case "Minion":
		//				if (!onGround)
		//				{
		//					Minion m = other.gameObject.GetComponent<Minion>();

		//					if (playerController.doingGroundPound)
		//					{
		//						m.GetComponent<HealthController>().Die();
		//					}
		//					else
		//					{
		//						m.GetComponent<HealthController>().RemoveHealth();
		//						playerController.Bounce();
		//					}
		//				}
		//				break;

		//			case "Switch":
		//				other.GetComponent<Switch>().OpenBridge();
		//				other.GetComponent<Animator>().Play("Switch Pressed");
		//				break;
		//		}
		//	}
		//}
	}
}
