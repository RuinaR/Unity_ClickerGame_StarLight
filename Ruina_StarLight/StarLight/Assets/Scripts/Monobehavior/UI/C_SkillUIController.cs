using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SkillUIController : C_SubUIController
{
    [SerializeField]
    private C_StateUIController m_cStateUI;

	private C_UserData m_cUserData;
	private C_SkillData[] m_arSkillData;

	[SerializeField]
	private C_ElementController_Skill[] m_arElement;

	public override void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arSkillData = C_BaseDataController.Instance.GetSkillData();

		for (int i = 0; i < m_arElement.Length; i++)
		{
			m_arElement[i].Init((E_Skill)i, m_cStateUI);
		}
	}

	public override void UpdateUI_ChangeResource()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateSkillElement((E_Skill)i);
		}
	}

	public override void UpdateUIALL()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateSkillElement((E_Skill)i);
		}
	}

	public void UpdateCooldownText(E_Skill eSkill)
	{
		m_arElement[(int)eSkill].UpdateCooldownText();
	}

	public void UpdateSkillElement(E_Skill eSkill)
	{
		m_arElement[(int)eSkill].UpdateElement();
	}

	public void SetActiveBtnInteractable(E_Skill eSkill, bool bIsInteractable)
	{
		m_arElement[(int)eSkill].SetActiveBtnInteractable(bIsInteractable);
	}

	public void SetCoolDownIMGFillAmount(E_Skill eSkill, float fAmount)
	{
		m_arElement[(int)eSkill].SetCoolDownIMGFillAmount(fAmount);
	}
}
