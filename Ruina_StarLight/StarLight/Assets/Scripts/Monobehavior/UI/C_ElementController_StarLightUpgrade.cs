using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ElementController_StarLightUpgrade : MonoBehaviour {

	[SerializeField]
	private Text m_textContent;
	[SerializeField]
	private Button m_upgradeBtn;
	[SerializeField]
	private Button m_iconBtn;
	[SerializeField]
	private C_UserData m_cUserData;
	[SerializeField]
	private C_UpgradeData[] m_arStarLightUpgradeData;

	private E_StarLightType m_eStarLightType;

	public void SetTextContent(string strContent)
	{
		m_textContent.text = strContent;
	}
	public void SetUpgradeBtnInteractable(bool bIsInteractable)
	{
		m_upgradeBtn.interactable = bIsInteractable;
	}

	public void Init(E_StarLightType eStarLightType)
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arStarLightUpgradeData = C_BaseDataController.Instance.GetStarLightUpgradeData();
		m_eStarLightType = eStarLightType;
		m_upgradeBtn.onClick.AddListener(Upgrade);
		m_iconBtn.onClick.AddListener(explain);
	}

	private void Upgrade()
	{
		C_UpgradeManager.Instance.Upgrade(m_eStarLightType, m_iconBtn.gameObject.transform.position);
	}

	private void explain()
	{
		float fUpgradeValue_Real = m_arStarLightUpgradeData[(int)m_eStarLightType].fValue_Upgrade;
		float fApplyValue_Real = C_UpgradeManager.Instance.GetApplyValue(m_eStarLightType);

		fUpgradeValue_Real *= 100.0f;
		fApplyValue_Real = (fApplyValue_Real - 1.0f) * 100.0f;

		double dUpgradeValue_Expression = System.Math.Round(fUpgradeValue_Real, C_BaseDataController.nDecimalPlace_Expression);
		double dApplyValue_Expression = System.Math.Round(fApplyValue_Real, C_BaseDataController.nDecimalPlace_Expression);


		C_DialogueController.Instance.Active
		(string.Format(m_arStarLightUpgradeData[(int)m_eStarLightType].arExplanation[(int)m_cUserData.eLanguage],
		C_StringHandler.Instance.GetUnitText(dUpgradeValue_Expression),
		C_StringHandler.Instance.GetUnitText(dApplyValue_Expression)),
		C_BaseDataController.fWaitTimeDialogue_Explain);
	}
}
