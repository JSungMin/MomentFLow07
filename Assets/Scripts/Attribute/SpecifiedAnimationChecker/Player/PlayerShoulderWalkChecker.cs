﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoulderWalkChecker : ShoulderAnimationCheckerBase {

	// Use this for initialization
	protected new void Start () {
		base.Start ();
	}

	#region implemented abstract members of ActionBase
	protected override bool CanTransition ()
	{
		return true;
	}
	protected override bool IsSatisfiedToAction ()
	{
		if (actor.useShoulder && actor.nowBodyAction.GetType () == typeof (PlayerBodyWalkChecker))
		{
			return true;
		}
		return false;
	}
	protected override void BeforeTransitionAction ()
	{
		nowActivated = false;
	}
	public override void DoSpecifiedAction ()
	{
		SetAnimationTrigger ();
		nowActivated = true;
	}
	public override void CancelSpecifiedAction ()
	{
		nowActivated = false;
	}
	#endregion
}
