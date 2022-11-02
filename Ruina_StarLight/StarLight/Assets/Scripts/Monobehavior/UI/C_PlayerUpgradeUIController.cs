using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PlayerUpgradeUIController : C_SubUIController
{
	private C_UserData m_cUserData;
	private C_UpgradeData[] m_arPlayerUpgradeData;
	[SerializeField]
	private C_ElementController_PlayerUpgrade[] m_arElement;

	public override void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arPlayerUpgradeData = C_BaseDataController.Instance.GetPlayerUpgradeData();

		for (int i = 0; i < m_arElement.Length; i++)
		{
			m_arElement[i].Init((E_PlayerUpgrade)i);
			UpdateUpgradeElement_Btn((E_PlayerUpgrade)i);
			UpdateUpgradeElement_Text((E_PlayerUpgrade)i);
		}
	}
	public override void UpdateUI_ChangeResource()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Btn((E_PlayerUpgrade)i);
		}
	}

	public override void UpdateUIALL()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Text(((E_PlayerUpgrade)i));
			UpdateUpgradeElement_Btn((E_PlayerUpgrade)i);
		}
	}

	public void UpdateUpgradeElement_Btn(E_PlayerUpgrade eUpgrade)
	{
		double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(eUpgrade);

		if ((m_cUserData.dCurrentStarLight < dCost_Real) ||
			(m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade] >= m_arPlayerUpgradeData[(int)eUpgrade].nLevelMax))
		{
			m_arElement[(int)eUpgrade].SetUpgradeBtnInteractable(false);
		}
		else
		{
			m_arElement[(int)eUpgrade].SetUpgradeBtnInteractable(true);
		}
	}

	public void UpdateUpgradeElement_Text(E_PlayerUpgrade eUpgrade)
	{
		double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(eUpgrade);

		if (m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade] >= m_arPlayerUpgradeData[(int)eUpgrade].nLevelMax)
		{
			m_arElement[(int)eUpgrade].SetTextContent("Level Max");
		}
		else
		{
			m_arElement[(int)eUpgrade].SetTextContent(string.Format("{0}\nLv : {1}\nCost : {2}",
							  		   m_arPlayerUpgradeData[(int)eUpgrade].arName[(int)m_cUserData.eLanguage],
								       m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade].ToString(),
									  C_StringHandler.Instance.GetUnitText(dCost_Real)));
		}
	}

}



