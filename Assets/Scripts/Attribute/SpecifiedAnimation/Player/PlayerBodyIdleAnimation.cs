﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBodyIdleAnimation : BodyAnimationBase {
	#region implemented abstract members of AnimationBase
	protected override void OnAnimationEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		ChangeAnimationClipByRandom ();
	}
	protected override void OnAnimationStay (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}
	protected override void OnAnimationExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		
	}
	#endregion
	
}