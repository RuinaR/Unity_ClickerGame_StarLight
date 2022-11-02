using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FairyUpgradeUIController : C_SubUIController
{
    [SerializeField]
    private C_StateUIController m_cStateUI;

	private C_UserData m_cUserData;
	private C_FairyUpgradeData[] m_arFairyUpgradeData;

	[SerializeField]
	private C_ElementController_FairyUpgrade m_cOriginE;
	[SerializeField]
	private Transform m_tfE;
	private C_ElementController_FairyUpgrade[] m_arElement;

	public override void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arFairyUpgradeData = C_BaseDataController.Instance.GetFairyUpgradeData();

		m_arElement = new C_ElementController_FairyUpgrade[C_FairyManager.nFairyCount];

		for (int i = 0; i < C_FairyManager.nFairyCount; i++)
		{
			m_arElement[i] = Instantiate(m_cOriginE, m_tfE);
			
			m_arElement[i].Init(i, m_cStateUI);
			UpdateUpgradeElement_Btn(i);
			UpdateUpgradeElement_Text(i);
		}
	}

	public override void UpdateUI_ChangeResource()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Btn(i);
		}
	}

	public override void UpdateUIALL()
	{
		for (int i = 0; i < m_arElement.Length; i++)
		{
			UpdateUpgradeElement_Text(i);
			UpdateUpgradeElement_Btn(i);
			m_arElement[i].UpdateTextLock_ChangeLanguage();
		}
	}

	public void UnLockElement(int nFairyNum)
	{
		m_arElement[nFairyNum].SetActiveLock(false);
	}

	public void UpdateUpgradeElement_Btn(int nFairyNum)
	{
		if (m_cUserData.arIsPurchaseFairy[nFairyNum])
		{
			double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(nFairyNum);

			if ((m_cUserData.dCurrentStarLight < dCost_Real) ||
				(m_cUserData.arFairyUpgradeLevel[nFairyNum] >= m_arFairyUpgradeData[nFairyNum].nLevelMax))
			{

				m_arElement[nFairyNum].SetUpgradeBtnInteractable(false);

			}
			else
			{
				m_arElement[nFairyNum].SetUpgradeBtnInteractable(true);
			}
		}
		else
		{
			double dCost_Real = m_arFairyUpgradeData[nFairyNum].fPurchaseCost;

			if (m_cUserData.dCurrentStarLight < dCost_Real)
			{
				m_arElement[nFairyNum].SetPurchaseBtnInteractable(false);
			}
			else
			{
				m_arElement[nFairyNum].SetPurchaseBtnInteractable(true);
			}

		}
	}
	public void UpdateUpgradeElement_Text(int nFairyNum)
	{
		if (m_cUserData.arIsPurchaseFairy[nFairyNum])
		{
			double dCost_Real = C_UpgradeManager.Instance.GetUpgradeCost(nFairyNum);

			if (m_cUserData.arFairyUpgradeLevel[nFairyNum] >= m_arFairyUpgradeData[nFairyNum].nLevelMax)
			{
				m_arElement[nFairyNum].SetTextContent("Level Max");
			}
			else
			{
				m_arElement[nFairyNum].SetTextContent(string.Format("{0}\nLv : {1}\nCost : {2}",
										m_arFairyUpgradeData[nFairyNum].arName[(int)m_cUserData.eLanguage],
										m_cUserData.arFairyUpgradeLevel[nFairyNum].ToString(),
										C_StringHandler.Instance.GetUnitText(dCost_Real)));
			}
		}
		else
		{
			double dCost_Real = m_arFairyUpgradeData[nFairyNum].fPurchaseCost;

			m_arElement[nFairyNum].SetTextContent(string.Format("{0}\nCost : {1}",
									m_arFairyUpgradeData[nFairyNum].arName[(int)m_cUserData.eLanguage],
									C_StringHandler.Instance.GetUnitText(dCost_Real)));
		}

	}

}
