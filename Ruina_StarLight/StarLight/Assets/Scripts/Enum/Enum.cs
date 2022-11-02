using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Scene
{
	start,
	main,
	max
}

public enum E_Language
{
	korean,
	english,
	max
}

public enum E_Effect
{
	starLight_Create,
	starLightTap_Default,
	starLightTap_Blue,
	starLightTap_Red,
	upgrade,
	max
}

public enum E_BGM
{
	start,
	game,
	fever,
	game_ed,
	max
}


public enum E_Sound_Effect
{
	starLight_Create,
	starLight_Default,
	starLight_Blue,
	starLight_Red,
	character_Tap,
	character_Voice,
	purchase,
	upgrade,
	gameStart,
	skill_PowerTap,
	skill_ChangeMode,
	UI_A,
	UI_D,
	Bonus_A,
	Bonus_Get,
	max
}


public enum E_SubUI
{
	playerUpgrade,
	starLightUpgrade,
	FairyUpgrade,
	skill,
	max
}

public enum E_CharacterMode
{
	DefaultMode,
	blueMode,
	redMode,
	max
}

public enum E_CharacterFaceType
{
	faceDefault,
	angry,
	confusion,
	cry,
	disappointment,
	joke,
	shameful,
	smile,
	max
}

public enum E_CharacterTalkType
{
	talk_no,
	talk_yes,
	max
}

public enum E_StarLightType
{
	starLightDefault,
	starLightBlue,
	starLightRed,
	starLightFairy,
	max
}



public enum E_PlayerUpgrade
{
	vehicle,
	starLightCritical_Percentage,
	fever_Time,
	fever_TapCount,
	max
}

public enum E_Skill
{
	initCooldown,
	changeMode,
	powerTap,
	autoTap,
	buff,
	max
}

public enum E_CatMotion
{
	Scratch,
	Attack,
	Ennui,
	Lick,
	Relax,
	Fly,
	Paw,
	Tail,
	Dig,
	Sway,
	Stretch,
	Sniff,
	Sleep1,
	Sleep2,
	Sleep3,
	max
}

public enum E_SpeechBubble
{
	Speech,
	Confusion,
	Embarrassed,
	Bulb,
	Heart,
	Angry,
	Shy,
	Question,
	Exclamation,
	QAndE,
	max
}
