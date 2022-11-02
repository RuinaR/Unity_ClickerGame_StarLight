using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_StateUIController : MonoBehaviour
{
	private C_UserData m_cUserData;
	private C_CharacterModeData[] m_arCharacterModeData;
	[SerializeField]
	private Sprite[] m_arSpriteModeIcon;
	[SerializeField]
	private Image m_IMGModeIcon;

	[SerializeField]
	private Text m_textCharacterMode;
	[SerializeField]
	private Text m_textCurrentStarLight;

	[SerializeField]
	private C_StarLightGainUIPool m_cStarLightGainUIPool;
	[SerializeField]
	private C_StarLightGainUIController m_cStarLightGainUIOrigin;


    [SerializeField]
	private Image m_FeverBar;
	private Color m_FeverBar_DefaultColor;
	private Color m_FeverBar_FeverColor;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arCharacterModeData = C_BaseDataController.Instance.GetCharacterModeData();

		m_cStarLightGainUIPool.Init(m_cStarLightGainUIOrigin);

        m_IMGModeIcon.sprite = m_arSpriteModeIcon[(int)m_cUserData.eCharacterMode];
		m_FeverBar_DefaultColor = new Color(0.85f, 0.1f, 0.3f, 1.0f);
		m_FeverBar_FeverColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		SetFeverBarLength(0.0f);
		SetFeverBarColor_Default();
	}

	public void ActiveStarLightGainUI(E_StarLightType eType, double dStarLightGain)
	{
        C_StarLightGainUIController cStarLightGainUI = m_cStarLightGainUIPool.GetFromPool();

        if (dStarLightGain >= 0)
        {
            cStarLightGainUI.SetColor(C_StarLightManager.arColorByStarLightType[(int)eType]);
            cStarLightGainUI.SetText('+' + C_StringHandler.Instance.GetUnitText(dStarLightGain));
        }
        else
        {
            cStarLightGainUI.SetColor(Color.white);
            cStarLightGainUI.SetText(C_StringHandler.Instance.GetUnitText(dStarLightGain));
        }
        cStarLightGainUI.gameObject.SetActive(true);
    }

	public void SetFeverBarLength(float fFillAmount)
	{
		m_FeverBar.fillAmount = Mathf.Clamp01(fFillAmount);
	}

	public void SetFeverBarColor_Default()
	{
		m_FeverBar.color = m_FeverBar_DefaultColor;
	}

	public void SetFeverBarColor_Fever()
	{
		m_FeverBar.color = m_FeverBar_FeverColor;
	}

	public void UpdateUI()
	{	
		m_IMGModeIcon.sprite = m_arSpriteModeIcon[(int)m_cUserData.eCharacterMode];
		m_textCharacterMode.text = m_arCharacterModeData[(int)m_cUserData.eCharacterMode].arModeName[(int)m_cUserData.eLanguage];
		if(m_cUserData.dCurrentStarLight >= C_StringHandler.MAX)
		{
			m_textCurrentStarLight.text = "MAX";
		}
		{
			m_textCurrentStarLight.text = "" + C_StringHandler.Instance.GetUnitText(m_cUserData.dCurrentStarLight);
		}
		if (!C_FeverController.Instance.IsFever())
		{
			SetFeverBarLength((float)m_cUserData.nCurrentFeverTapCount / (int)C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.fever_TapCount));
		}
	}
}
