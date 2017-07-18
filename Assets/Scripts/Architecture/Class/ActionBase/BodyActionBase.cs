﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyActionBase : ActionBase {
	
	public List<BodyActionBase> canTransitionActionList;

	public List<string> bodyOnlyAnimationTriggerName;

	protected override IEnumerator TransitionAction ()
	{
		for (int i = canTransitionActionList.Count - 1; i >= 0; i--)
		{
			if (canTransitionActionList [i].IsSatisfiedToAction())
			{
				if (actor.nowBodyAction != canTransitionActionList [i]) {
					actor.nowBodyAction = canTransitionActionList [i];
					BeforeTransitionAction ();
					yield return new WaitForEndOfFrame ();
					actor.nowBodyAction.DoSpecifiedAction ();
				}
				break;
			}
		}
	}

	public void SetAnimationTrigger ()
	{
		if (animationIndex == -1) {
			if (actor.useShoulder)
				animationIndex = Random.Range (0, setAnimationTriggerName.Count - 1);
			else
				animationIndex = Random.Range (0, bodyOnlyAnimationTriggerName.Count - 1);
		} 
		if (actor.useShoulder) {
			actor.bodyAnimator.SetTrigger (setAnimationTriggerName [animationIndex].bodyAnimationName);
			actor.shoulderAnimator.SetTrigger (setAnimationTriggerName [animationIndex].shoulderAnimationName);
		} else {
			actor.bodyAnimator.SetTrigger (bodyOnlyAnimationTriggerName [animationIndex]);
		}
	}
}
