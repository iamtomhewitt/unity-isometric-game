using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants 
{
	// Animation States
    public const int ANIMATION_IDLE = 0;
    public const int ANIMATION_WALK = 1;
    public const int ANIMATION_RUN = 2;
    public const int ANIMATION_ATTACK = 3;
	public const int ANIMATION_STUNNED = 4;
	public const int ANIMATION_UNSTUNNED = 5;
	public const int ANIMATION_SHOOT = 6;

	// Colours
	public static Color32 DAMAGE_COLOUR { get { return new Color32(229, 57, 57, 255); } }
}
