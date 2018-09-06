using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Player
{
	public class PlayerController : MonoBehaviour
	{
		public float movementSpeed;
		public float rotationSpeed;
		public float jumpForce;

		[Space()]
		public bool canDoubleJump;
		public bool canGroundPound;
		public bool doingGroundPound;

		public float groundPoundRadius;

		private Vector3 isometricForward = new Vector3(-1f, 0f, 1f);
		private Vector3 isometricRight = new Vector3(1f, 0f, 1f);

		private AnimationController animationController;
		private PlayerCollisionController collisionController;
		private PlayerParticleController particleController;
		private PlayerWeaponsController weaponsController;
		private Rigidbody rb;

		private float originalMovementSpeed;

		private bool shooting;

		void Start()
		{
			animationController = GetComponent<AnimationController>();
			collisionController = GetComponent<PlayerCollisionController>();
			particleController	= GetComponent<PlayerParticleController>();
			weaponsController	= GetComponent<PlayerWeaponsController>();
			rb					= GetComponent<Rigidbody>();

			animationController.ChangeAnimation(Constants.ANIMATION_IDLE);

			originalMovementSpeed = movementSpeed;
		}

		void Update()
		{
			if (!doingGroundPound)
			{
				if (!IsLeftJoystickInput())
				{
					animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
					particleController.StopFeetDust();
				}
				else
				{
					float angle = (Mathf.Atan2(-Input.GetAxis(XboxController.leftJoystickX), -Input.GetAxis(XboxController.leftJoystickY)) * Mathf.Rad2Deg) + 45f;
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0f, -angle, 0)), rotationSpeed * Time.deltaTime);

					if (!shooting)
						animationController.ChangeAnimation(Constants.ANIMATION_RUN);

					if (!particleController.feetDust.isPlaying && collisionController.onGround)
						particleController.PlayFeetDust();
				}

				transform.position += isometricForward * Time.deltaTime * movementSpeed * -Input.GetAxis(XboxController.leftJoystickY);
				transform.position += isometricRight * Time.deltaTime * movementSpeed * Input.GetAxis(XboxController.leftJoystickX);

				//transform.position += isometricForward * Time.deltaTime * movementSpeed * Input.GetAxis("Vertical");
				//transform.position += isometricRight * Time.deltaTime * movementSpeed * Input.GetAxis("Horizontal");
			}

			if (Input.GetButtonDown(XboxController.xboxAButton))
			{
				Jump();
			}
			if (Input.GetButtonDown(XboxController.xboxXButton) && canGroundPound)
			{
				StartCoroutine(GroundPound());
			}

			if (Input.GetAxis(XboxController.rightTrigger) >= 1f)
			{
				shooting = true;
				weaponsController.Shoot();
				movementSpeed = 0f;
				animationController.ChangeAnimation(Constants.ANIMATION_IDLE);
			}
			else
			{
				shooting = false;
				movementSpeed = originalMovementSpeed;
			}
		}

		private IEnumerator GroundPound()
		{
			if (!collisionController.onGround)
			{
				doingGroundPound = true;
				animationController.ChangeAnimation(Constants.ANIMATION_ATTACK);

				rb.velocity = Vector3.zero;
				rb.AddForce(Vector3.up * 15f, ForceMode.Impulse);

				yield return new WaitForSeconds(animationController.GetAnimationLength(Constants.ANIMATION_ATTACK));

				rb.velocity = Vector3.zero;
				rb.AddForce(Vector3.down * 50f, ForceMode.Impulse);

				yield return new WaitForSeconds(.5f);				

				canGroundPound = false;
				doingGroundPound = false;
			}
		}

		private bool IsLeftJoystickInput()
		{
			if (Input.GetAxis(XboxController.leftJoystickY) == 0 && Input.GetAxis(XboxController.leftJoystickX) == 0)
				return false;
			else
				return true;
		}

		private void Jump()
		{
			particleController.StopFeetDust();

			if (collisionController.onGround)
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				collisionController.onGround = false;
				canDoubleJump = true;
			}
			else if (canDoubleJump)
			{
				canDoubleJump = false;

				// We can only ground pound if the slider is full
				if (GetComponent<PlayerUIController>().IsGroundPoundSliderFull())
					canGroundPound = true;

				rb.velocity = Vector3.zero;
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			}
		}

		public void Bounce()
		{
			rb.velocity = Vector3.zero;
			rb.AddForce(Vector3.up * jumpForce * 1f, ForceMode.Impulse);
		}
	}
}