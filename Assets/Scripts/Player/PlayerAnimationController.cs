using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : AnimationController
{
	public AnimationClip idle;
	public AnimationClip run;
	public AnimationClip groundPound;

	public override void ChangeAnimation(int state)
	{
		if (currentAnimation == state)
			return;

		if (animator == null)
		{
			Debug.LogWarning("Animator was null, it may have been destroyed.");
			return;
		}

		switch (state)
		{
			case Constants.ANIMATION_IDLE:
				animator.Play(idle.name);
				break;

			case Constants.ANIMATION_RUN:
				animator.Play(run.name);
				break;

			case Constants.ANIMATION_ATTACK:
				animator.Play(groundPound.name);
				break;

			default:
				print("Animation not found, or case hasn't been included.");
				break;
		}

		currentAnimation = state;
	}

	public override float GetAnimationLength(int state)
	{
		switch (state)
		{
			case Constants.ANIMATION_IDLE:
				return 0f;

			case Constants.ANIMATION_RUN:
				return run.length;

			case Constants.ANIMATION_ATTACK:
				return groundPound.length;

			default:
				print("Animation not found, or case hasn't been included.");
				return 0f;
		}
	}
}
