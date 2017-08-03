﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyChaseChecker : BodyAnimationCheckerBase {
	public EnemyActor eActor;
	public float disToChase = 2f;

	// Use this for initialization
	protected new void Start () {
		base.Start ();
		eActor = EnemyActor.GetEnemyActor<Actor> (actor);
	}

	#region implemented abstract members of AnimationCheckerBase
	protected override bool CanTransition ()
	{
		return true;
	}
	protected override bool IsSatisfiedToAction ()
	{
		if (null == eActor.targetActor) {
			if (!eActor.roomInfo.roomRectCollider.bounds.Contains (eActor.lastTargetPoint)) {
				return false;
			}
			if (eActor.disToSuspiciousPoint <= 0.1f) {
				return false;
			}
			if (eActor.roomInfo.roomState == RoomState.Combat)
				return true;
		} 
		else {
			if (eActor.disToTarget > disToChase &&
				eActor.roomInfo.roomName == eActor.targetActor.roomInfo.roomName
			)
			{
				return true;
			}
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
		eActor.FindSuspiciousObject ();
		if (null != eActor.targetActor) {
			eActor.agent.SetDestination (eActor.targetActor.transform.position);
		}
		else {
			eActor.agent.SetDestination (eActor.lastTargetPoint);
		}
		eActor.GetEnemyOutsideInfo ().SetViewDirection (eActor.agent.destination);
		nowActivated = true;
	}
	public override void CancelSpecifiedAction ()
	{
		nowActivated = false;
	}
	#endregion
}
