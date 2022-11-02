using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class C_ChangeMode : MonoBehaviour {

	[SerializeField]
	private C_PlayerController m_cPlayer;

	[SerializeField]
	private GameObject window;

	[SerializeField]
	private Toggle[] m_arSelectToggle;

	[SerializeField]
	private Text[] m_arText;

	[SerializeField]
	private Text m_TextBtn;

	[SerializeField]
	private Button m_button;

	[SerializeField]
	private GameObject m_SkillEffect_ChangeMode;

	private C_UserData m_cUserData;
	private C_CharacterModeData[] m_arCharacterModeData;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arCharacterModeData = C_BaseDataController.Instance.GetCharacterModeData();

		UpdateCharacterModeToggle();
		UpdateText();
		m_button.interactable = false;
		window.gameObject.SetActive(false);
	}

	public void UpdateCharacterModeToggle()
	{
		for(int i = 0; i < m_arSelectToggle.Length; i++)
		{
			m_arSelectToggle[i].interactable = true;
			m_arSelectToggle[i].isOn = false;
		}
		m_arSelectToggle[(int)m_cUserData.eCharacterMode].interactable = false;
	}
	public void UpdateText()
	{
		for(int i = 0; i < m_arText.Length; i++)
		{
			m_arText[i].text = m_arCharacterModeData[i].arModeName[(int)m_cUserData.eLanguage];
		}
		m_TextBtn.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].change;
	}

	public void ActiveBtn()
	{
		bool b = false;
		for (int i = 0; i < m_arSelectToggle.Length; i++)
		{
			if(m_arSelectToggle[i].isOn == true)
			{
				b = true;
			}	
		}
		if (b)
		{
			m_button.interactable = true;
		}
		else
		{
			m_button.interactable = false;
		}
	}

	public void ChangeMode()
	{
		for (int i = 0; i < m_arSelectToggle.Length; i++)
		{
			if (m_arSelectToggle[i].isOn)
			{
				m_cUserData.eCharacterMode = (E_CharacterMode)i;
			}
		}

		C_MainUIController.Instance.UpdateUI_ChangeResource();
		m_button.interactable = false;
		window.gameObject.SetActive(false);

		m_SkillEffect_ChangeMode.transform.position = new Vector3(m_cPlayer.gameObject.transform.position.x - 0.26f,
																  m_cPlayer.gameObject.transform.position.y + 2.06f,
																  0.0f);
		m_SkillEffect_ChangeMode.SetActive(true);
		C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.skill_ChangeMode);
	}

	public void ActiveWindow()
	{
		UpdateCharacterModeToggle();
		window.gameObject.SetActive(true);
		m_button.interactable = false;
	}


}
