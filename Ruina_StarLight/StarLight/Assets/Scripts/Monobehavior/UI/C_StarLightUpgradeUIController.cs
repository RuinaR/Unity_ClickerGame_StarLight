using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StarLightUpgradeUIController : C_SubUIController
{
	private C_UserData m_cUserData;
	private C_UpgradeData[] m_arStarLightUpgradeData;

	[SerializeField]
	private C_ElementController_StarLightUpgrade[] m_arElement;

	public override void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arStarLightUpgradeData = C_BaseDataController.Instance.GetStarLightUpgradeData();

		for (int i = 0; i < m_arElement.Length; i++)
		{
			m_arElement[i].Init((E_StarLightType)i);
			UpdateUpgradeElement_Btn((E_StarLightType)i);
			UpdateUpgradeElement_Text((E_StarLightType)i);
		}
	}

	public override void UpdateUI_ChangeResource()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Btn((E_StarLightType)i);
		}
	}

	public override void UpdateUIALL()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Text((E_StarLightType)i);
			UpdateUpgradeElement_Btn((E_StarLightType)i);
		}
	}

	public void UpdateUpgradeElement_Btn(E_StarLightType eType)
	{
		double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(eType);

		if ((m_cUserData.dCurrentStarLight < dCost_Real) ||
			(m_cUserData.arStarLightUpgradeLevel[(int)eType] >= m_arStarLightUpgradeData[(int)eType].nLevelMax))
		{
			m_arElement[(int)eType].SetUpgradeBtnInteractable(false);
		}
		else
		{
			m_arElement[(int)eType].SetUpgradeBtnInteractable(true);
		}
	}

	public void UpdateUpgradeElement_Text(E_StarLightType eType)
	{
		double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(eType);


		if (m_cUserData.arStarLightUpgradeLevel[(int)eType] >= m_arStarLightUpgradeData[(int)eType].nLevelMax)
		{
			m_arElement[(int)eType].SetTextContent("Level Max");
		}
		else
		{
			m_arElement[(int)eType].SetTextContent(string.Format("{0}\nLv : {1}\nCost : {2}",
									m_arStarLightUpgradeData[(int)eType].arName[(int)m_cUserData.eLanguage],
									m_cUserData.arStarLightUpgradeLevel[(int)eType].ToString(),
									C_StringHandler.Instance.GetUnitText(dCost_Real)));
		}
	}
}

