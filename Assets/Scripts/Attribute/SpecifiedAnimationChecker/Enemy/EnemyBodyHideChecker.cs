﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyHideChecker : BodyAnimationCheckerBase {
	public EnemyActor eActor;
	public EnemyOutsideInfo eOutsideInfo;
	public HideableObject targetHideableObj = null; 
	public HideableFace targetFace;
	public float autoBreakDistance;

	[Header ("StateMaintainOption")]
	public float stateMaintainDuration = 5f;
	public float stateMaintainMinDuration = 5f;
	public float stateMaintainMaxDuration = 10f;
	public float stateMaintainTimer = 0f;
	[Range (0,1)]
	public float tensionThreshold = 0.5f;

	// Use this for initialization
	protected new void Start () {
		base.Start ();
		eActor = EnemyActor.GetEnemyActor<Actor> (actor);
		eOutsideInfo = eActor.GetEnemyOutsideInfo ();
		if (autoBreakDistance == 0)
		{
			autoBreakDistance = eActor.bodyCollider.bounds.extents.x;
		}
	}

	#region implemented abstract members of AnimationCheckerBase
	protected override bool CanTransition ()
	{
		return true;
	}
	protected override bool IsSatisfiedToAction ()
	{		
		var foundHideableObj = GetHideableObject () as HideableObject;

		if (eActor.tensionGauge >= tensionThreshold &&
			null != foundHideableObj &&
			stateMaintainTimer <= stateMaintainDuration
		)
		{
			targetHideableObj = eOutsideInfo.foundedHideableObjList [0];
			return true;
		}

		Debug.Log ("FH : " + foundHideableObj);
		Debug.Log ("SMT : " + stateMaintainTimer);
		return false;
	}
	protected override void BeforeTransitionAction ()
	{
		targetHideableObj.GetHideableFaceByName (targetFace.faceName).hideable = true;
		stateMaintainTimer = 0f;
		if (actor.stateInfo.isHiding)
		{
			actor.stateInfo.isHiding = false;
			stateMaintainDuration = Random.Range (stateMaintainMinDuration, stateMaintainMaxDuration);
		}
		nowActivated = false;
	}
	public override void DoSpecifiedAction ()
	{
		if (!eActor.stateInfo.isHiding) {
			if (RunToPoint (targetHideableObj.transform.position + targetFace.point)) {
				eActor.stateInfo.isHiding = true;
				targetHideableObj.GetHideableFaceByName (targetFace.faceName).hideable = false;
				eActor.SetToCrouch ();
				SetAnimationTrigger ();
			}
			else {
				Debug.Log ("RunToHide");
				eActor.GetSpecificAction<EnemyBodyChaseChecker> ().SetAnimationTrigger ();
			}
		} else {
			Debug.Log ("Hided");
			actor.DecreaseTension ();
			stateMaintainTimer += actor.customDeltaTime;
		}
		nowActivated = true;
	}
	public override void CancelSpecifiedAction ()
	{
		targetHideableObj.GetHideableFaceByName (targetFace.faceName).hideable = true;
		stateMaintainTimer = 0f;
		if (actor.stateInfo.isHiding)
		{
			actor.stateInfo.isHiding = false;
		}
		nowActivated = false;
	}
	#endregion

	private InteractableObject GetHideableObject ()
	{
		HideableObject hideableObj = null;
		for (int i = 0; i < eOutsideInfo.foundedHideableObjList.Count; i++)
		{
			hideableObj = eOutsideInfo.foundedHideableObjList [i];

			if (null == hideableObj)
				continue;
			
			var face = GetHideableFace (hideableObj, eActor.damagedDirection);
			if (null != face) {
				targetFace = face;
				return hideableObj;
			}
		}
		return hideableObj;
	}

	private HideableFace GetHideableFace (HideableObject hideableObj, Vector3 damagedDir)
	{
		var absX = Mathf.Abs (damagedDir.x);
		var absY = Mathf.Abs (damagedDir.y);
		var absZ = Mathf.Abs (damagedDir.z);

		HideableFace face = null;

		if (absX > absY) {
			if (absX > absZ) {
				if (damagedDir.x > 0) {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.rightFace);
					if (face.hideable)
						return face;
				} else {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.leftFace);
					if (face.hideable)
						return face;
				}
			} else {
				if (damagedDir.z > 0) {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.backFace);
					if (face.hideable)
						return face;
				} else {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.forwardFace);
					if (face.hideable)
						return face;
				}
			}
		}
		else {
			if (absY > absZ) {
				if (damagedDir.y > 0) {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.downFace);
					if (face.hideable)
						return face;
				} else {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.upFace);
					if (face.hideable)
						return face;
				}
			} else {
				if (damagedDir.z > 0) {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.backFace);
					if (face.hideable)
						return face;
				} else {
					face = hideableObj.GetHideableFaceByName (HideableFaceName.forwardFace);
					if (face.hideable)
						return face;
				}
			}
		}
		return face;
	}

	//  만약 point에 근접하면 return true, 아니면  return false
	public bool RunToPoint (Vector3 obstaclePoint)
	{
		obstaclePoint.y = eActor.transform.position.y;
		eActor.agent.destination = obstaclePoint;
		eActor.agent.SetDestination (obstaclePoint);

		if (Vector3.Distance (actor.transform.position, obstaclePoint) <= autoBreakDistance) {
			return true;
		}
		return false;
	}
}
