using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class AnimationController : MonoBehaviour
{
	protected Animator animator;

	protected int currentAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

	public abstract void ChangeAnimation(int state);

	public abstract float GetAnimationLength(int state);
}
