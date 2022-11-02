using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class C_OptionController : MonoBehaviour {

	private C_UserData m_cUserData;

	[SerializeField]
	private GameObject Option;

	[SerializeField]
	private Slider m_Sound_BGM_Slider;
	[SerializeField]
	private Slider m_Sound_Effect_Slider;

	[SerializeField]
	private Toggle[] m_arLanguage_Toggle;

	[SerializeField]
	private Button m_BtnApply;
	[SerializeField]
	private Text m_textApplyBtn;

	[SerializeField]
	private Text m_textBGM;
	[SerializeField]
	private Text m_textSoundEffect;

	[SerializeField]
	private Text m_FontInfoBtn;


	private E_Language m_eSelectLanguage;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();

		m_Sound_BGM_Slider.value = m_cUserData.fSound_BGM_Volume;
		m_Sound_Effect_Slider.value = m_cUserData.fSound_Effect_Volume;

		m_eSelectLanguage = m_cUserData.eLanguage;
		for (int i = 0; i < m_arLanguage_Toggle.Length; i++)
		{
			if (i == (int)m_eSelectLanguage)
			{
				m_arLanguage_Toggle[i].isOn = true;
			}
			else
			{
				m_arLanguage_Toggle[i].isOn = false;
			}
		}
		UpdateText();
		Option.SetActive(false);
	}

	public void UpdateText()
	{
		m_textApplyBtn.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].apply;
		m_textBGM.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Option_BGM;
		m_textSoundEffect.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Option_SoundEffect;
		if(m_cUserData.eLanguage == E_Language.korean)
		{
			m_FontInfoBtn.text = "폰트 정보";
		}
		else
		{
			m_FontInfoBtn.text = "Font Info";
		}
	}

	public void SelectLanguage(int nLanguage)
	{
		m_eSelectLanguage = (E_Language)nLanguage;
	}

	public void Apply()
	{
		m_cUserData.fSound_BGM_Volume = m_Sound_BGM_Slider.value;
		m_cUserData.fSound_Effect_Volume = m_Sound_Effect_Slider.value;

		C_SoundManager.Instance.ChangeBGMVolume(m_cUserData.fSound_BGM_Volume);

		if (m_cUserData.eLanguage != m_eSelectLanguage)
		{
			m_cUserData.eLanguage = m_eSelectLanguage;
			C_MainUIController.Instance.UpdateUIAll();
		}


		C_UserDataController.Instance.Save();
	}

	public void ActiveOption()
	{
		m_Sound_BGM_Slider.value = m_cUserData.fSound_BGM_Volume;
		m_Sound_Effect_Slider.value = m_cUserData.fSound_Effect_Volume;
		m_eSelectLanguage = m_cUserData.eLanguage;
		for (int i = 0; i < m_arLanguage_Toggle.Length; i++)
		{
			if (i == (int)m_eSelectLanguage)
			{
				m_arLanguage_Toggle[i].isOn = true;
			}
			else
			{
				m_arLanguage_Toggle[i].isOn = false;
			}
		}
		Option.gameObject.SetActive(true);
	}

	public void FontInfo()
	{
		if (m_cUserData.eLanguage == E_Language.korean)
		{
			C_PopUpController.Instance.ActivePopUp_OK("이 게임에는 '우아한형제들'에서 제공한 '배달의민족 주아체'가 적용되어 있습니다.");
		}
		else
		{
			C_PopUpController.Instance.ActivePopUp_OK("The game features 'Baemin Jua Font' provided by 'Woowa Brothers Corp'");
		}
	}
}
