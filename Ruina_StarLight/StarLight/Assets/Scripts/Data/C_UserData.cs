using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class C_UserData
{
	public bool bIsTutorialCompleted;
	public int nTextCount;

	public double dCurrentStarLight;
	public int nCurrentFeverTapCount;

	public E_CharacterMode eCharacterMode;

	public int[] arPlayerUpgradeLevel;
	public int[] arStarLightUpgradeLevel;

	public bool[] arIsPurchaseFairy;
	public int[] arFairyUpgradeLevel;

	public int[] arSkillCooldown;

	public E_Language eLanguage;
	public float fSound_BGM_Volume;
	public float fSound_Effect_Volume;

	public bool bIsPurchaseNo_Ads;

}
