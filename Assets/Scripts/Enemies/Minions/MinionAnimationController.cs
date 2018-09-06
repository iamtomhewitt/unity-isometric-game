using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minions
{
	public class MinionAnimationController : AnimationController
	{
		public AnimationClip idle;
		public AnimationClip attack;
		public AnimationClip shoot;
		public AnimationClip run;

		public override void ChangeAnimation(int state)
		{
			//if (currentAnimation == state)
			//	return;

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

				case Constants.ANIMATION_ATTACK:
					animator.Play(attack.name);
					break;

				case Constants.ANIMATION_SHOOT:
					animator.Play(shoot.name);
					break;

				case Constants.ANIMATION_RUN:
					animator.Play(run.name);
					break;

				default:
					print("Minion Animation Controller ChangeAnimation: Animation not found, or case hasn't been included.");
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

				case Constants.ANIMATION_ATTACK:
					return attack.length;

				case Constants.ANIMATION_SHOOT:
					return shoot.length;

				default:
					print("Minion Animation Controller GetAnimationLength: Animation not found, or case hasn't been included.");
					return 0f;
			}
		}

		public Animator GetAnimator()
		{
			return this.animator;
		}
	}
}
