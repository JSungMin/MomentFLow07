﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DynamicObjectType
{
	Actor,
	InteractableObject,
	Bullet
}

public enum RoomState
{
	Normal,
	Alert,
	Combat,
	Clear
}

public enum InteractableObjectType
{
	HideableObject,
	Item
}

public enum HumanType
{
	Player,
	Enemy,
	NPC
}

public enum SoundType
{
	GunFireSound,
	SuspiciousSound,
	YellSound
}

public enum ItemType
{
	Potion,
	Ammo,
	Key,
	SkillScroll,
	PuzzlePiece
}
	

public enum AnimationStateInfoEnum
{
	Enter,
	Progress,
	Exit
}

public enum EquipWeaponType
{
	Gun,
	MeleeWeapon
}

public enum AimState
{
	Normal,
	Bounce
}

public enum HideableFaceName
{
	leftFace = 0,
	rightFace = 1,
	forwardFace = 2,
	backFace = 3,
	upFace = 4,
	downFace = 5
}

public class EnumPool {
}
