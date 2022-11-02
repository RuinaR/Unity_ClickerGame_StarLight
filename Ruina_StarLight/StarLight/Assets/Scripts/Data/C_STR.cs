using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_STR
{
    public string start;

	public string need_to_buy_fairyUpgrade;
	public string coolDownTime;
	public string ableToActive;
	public string ok;
	public string yes;
	public string no;
	public string change;
	public string apply;

	public string Option_BGM;
	public string Option_SoundEffect;

	public string PopUp_NoSkillCanCooldownInit;
	public string PopUp_CantActiveSkillInDuration;
	public string PopUp_skillActive;
	public string PopUp_gameEnd;
	public string PopUp_purchase;
	public string PopUp_Exchange;
	public string PopUp_Exchange_Completion;

	public string Skill_InitCooldown;

    public string AutoSave;

	public string Shop_NoAds;
	public string need_to_buy_NOAD; 

	public string Fairy_PurchaseExplanation;
	public string Fairy_PurchaseDialogue;
	public string Fairy_Explanation;


	public static string GetComleteWord(string strName, string strFirstValue, string strSecondValue)
	{
		char lastName = strName.ToCharArray()[strName.Length - 1];
		if (lastName < 0xAC00 || lastName > 0xD7A3)
		{
			return strName;
		}

		string seletedValue = (lastName - 0xAC00) % 28 > 0 ? strFirstValue : strSecondValue;
		return strName+seletedValue; }
		
}
