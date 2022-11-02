using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ElementController_PlayerUpgrade : MonoBehaviour {

	[SerializeField]
	private Text m_textContent;
	[SerializeField]
	private Button m_upgradeBtn;
	[SerializeField]
	private Button m_iconBtn;
	[SerializeField]
	private C_UserData m_cUserData;
	[SerializeField]
	private C_UpgradeData[] m_arPlayerUpgradeData;

	private E_PlayerUpgrade m_ePlayerUpgrade;

	public void SetTextContent(string strContent)
	{
		m_textContent.text = strContent; 
	}
	public void SetUpgradeBtnInteractable(bool bIsInteractable)
	{
		m_upgradeBtn.interactable = bIsInteractable;
	}

	public void Init(E_PlayerUpgrade ePlayerUpgrade)
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arPlayerUpgradeData = C_BaseDataController.Instance.GetPlayerUpgradeData();
		m_ePlayerUpgrade = ePlayerUpgrade;
		m_upgradeBtn.onClick.AddListener(Upgrade);
		m_iconBtn.onClick.AddListener(explain);
	}

	private void Upgrade()
	{
		C_UpgradeManager.Instance.Upgrade(m_ePlayerUpgrade, m_iconBtn.gameObject.transform.position);
	}

	private void explain()
	{
		float fUpgradeValue_Real = m_arPlayerUpgradeData[(int)m_ePlayerUpgrade].fValue_Upgrade;
		float fApplyValue_Real = C_UpgradeManager.Instance.GetApplyValue(m_ePlayerUpgrade);
		if(m_ePlayerUpgrade == E_PlayerUpgrade.vehicle)
		{
			fUpgradeValue_Real = (fUpgradeValue_Real - 1.0f) * 100.0f;
		}
		else if(m_ePlayerUpgrade == E_PlayerUpgrade.starLightCritical_Percentage)
		{
			fUpgradeValue_Real *= 100.0f;
			fApplyValue_Real *= 100.0f;
		}

		double dUpgradeValue_Expression = System.Math.Round(fUpgradeValue_Real, C_BaseDataController.nDecimalPlace_Expression);
		double dApplyValue_Expression = System.Math.Round(fApplyValue_Real, C_BaseDataController.nDecimalPlace_Expression);

		C_DialogueController.Instance.Active
		(string.Format(m_arPlayerUpgradeData[(int)m_ePlayerUpgrade].arExplanation[(int)m_cUserData.eLanguage],
		C_StringHandler.Instance.GetUnitText(dUpgradeValue_Expression),
		C_StringHandler.Instance.GetUnitText(dApplyValue_Expression)),
		C_BaseDataController.fWaitTimeDialogue_Explain);
	}
}
