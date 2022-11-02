using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ElementController_Skill : MonoBehaviour
{
    private C_StateUIController m_cStateUI;

	[SerializeField]
	private Text m_textContent;
	[SerializeField]
	private Button m_activeBtn;
	[SerializeField]
	private Button m_iconBtn;
	[SerializeField]
	private Image m_IMGCoolDown;

	private float m_fElapsedTime;

	private C_UserData m_cUserData;
	private C_SkillData[] m_arSkillData;
	private E_Skill m_eSkill;

	public void Init(E_Skill eSkill, C_StateUIController cStateUI)
	{
        m_cStateUI = cStateUI;

        m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arSkillData = C_BaseDataController.Instance.GetSkillData();
		m_eSkill = eSkill;
		m_activeBtn.onClick.AddListener(Active);
		m_iconBtn.onClick.AddListener(explain);

		m_iconBtn.gameObject.SetActive(true);
		
		m_activeBtn.interactable = true;
		
		UpdateElement();
	}

	public void SetTextContent(string strContent)
	{
		m_textContent.text = strContent;
	}
	public void SetActiveBtnInteractable(bool bIsInteractable)
	{
		m_activeBtn.interactable = bIsInteractable;
	}


	public void UpdateElement()
	{
		
			if (C_SkillManager.Instance.IsAbleToActive(m_eSkill))
			{

				SetActiveBtnInteractable(true);
				SetCoolDownIMGFillAmount(0.0f);
				
				SetTextContent(string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].ableToActive,
											 m_arSkillData[(int)m_eSkill].arName[(int)m_cUserData.eLanguage]));
			}
			else
			{
				SetCoolDownIMGFillAmount
					((float)m_cUserData.arSkillCooldown[(int)m_eSkill] / (float)m_arSkillData[(int)m_eSkill].nCooldownTime);
				UpdateCooldownText();
			}
			
	}

	public void UpdateCooldownText()
	{
		SetActiveBtnInteractable(false);
		char[][] tempArr = new char[(int)E_Language.max][];
		tempArr[(int)E_Language.korean] = new char[2];
		tempArr[(int)E_Language.english] = new char[2];

		tempArr[(int)E_Language.korean][0] = '분';
		tempArr[(int)E_Language.korean][1] = '초';
		tempArr[(int)E_Language.english][0] = 'm';
		tempArr[(int)E_Language.english][1] = 's';


		int nMin = m_cUserData.arSkillCooldown[(int)m_eSkill] / 60;
		int nSec = m_cUserData.arSkillCooldown[(int)m_eSkill] % 60;

		if (nMin <= 0)
		{
			SetTextContent(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].coolDownTime +
							"\n" + nSec + tempArr[(int)m_cUserData.eLanguage][1]);
		}
		else
		{
			SetTextContent(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].coolDownTime +
							"\n" + nMin + tempArr[(int)m_cUserData.eLanguage][0] +
							nSec + tempArr[(int)m_cUserData.eLanguage][1]);
		}

	}

	public void Active()
	{
		C_SkillManager.Instance.ActiveSkillPopUp(m_eSkill);
		UpdateElement();
	}
	private void explain()
	{
		C_DialogueController.Instance.Active(m_arSkillData[(int)m_eSkill].arExplanation[(int)m_cUserData.eLanguage],
											 C_BaseDataController.fWaitTimeDialogue_Explain);
	}

	public void SetCoolDownIMGFillAmount(float fAmount)
	{
		m_IMGCoolDown.fillAmount = Mathf.Clamp01(fAmount);
	}
}
