using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ElementController_FairyUpgrade : MonoBehaviour {

    private C_StateUIController m_cStateUI;

    [SerializeField]
	private GameObject m_Lock;
	[SerializeField]
	private Text m_textLock;
	[SerializeField]
	private Text m_textContent;
	[SerializeField]
	private Button m_upgradeBtn;
	[SerializeField]
	private Button m_purchaseBtn;
	[SerializeField]
	private Image m_iconIMG;
	[SerializeField]
	private Button m_iconBtn;

	private C_UserData m_cUserData;
	private C_FairyUpgradeData[] m_arFairyUpgradeData;

	private int m_nFairyNum;

	public void Init(int nFairyNum, C_StateUIController cStateUI)
	{
		m_nFairyNum = nFairyNum;
		m_cStateUI = cStateUI;

        m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arFairyUpgradeData = C_BaseDataController.Instance.GetFairyUpgradeData();
		m_upgradeBtn.onClick.AddListener(Upgrade);
		m_purchaseBtn.onClick.AddListener(Purchase);
		m_iconBtn.onClick.AddListener(explain);
		m_iconIMG.sprite = C_SpriteManager.Instance.GetSpriteIcon_Fairy(m_nFairyNum);

		if (m_nFairyNum != 0)
		{

			m_textLock.text = string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].need_to_buy_fairyUpgrade, 
											m_arFairyUpgradeData[m_nFairyNum - 1].arName[(int)m_cUserData.eLanguage]);

			if (!m_cUserData.arIsPurchaseFairy[m_nFairyNum - 1])
			{
				SetActiveLock(true);
			}
			else
			{
				SetActiveLock(false);
			}
		}
		else
		{
			SetActiveLock(false);
		}
	}
	public void UpdateTextLock_ChangeLanguage()
	{
		if (m_nFairyNum != 0)
		{
			m_textLock.text = string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].need_to_buy_fairyUpgrade,
												m_arFairyUpgradeData[m_nFairyNum - 1].arName[(int)m_cUserData.eLanguage]);
		}
	}


	public void SetTextContent(string strContent)
	{
		m_textContent.text = strContent;
	}
	public void SetUpgradeBtnInteractable(bool bIsInteractable)
	{
		m_upgradeBtn.interactable = bIsInteractable;	
	}
	public void SetPurchaseBtnInteractable(bool bIsInteractable)
	{
		m_purchaseBtn.interactable = bIsInteractable;
	}
	public void SetActiveLock(bool bIsLock)
	{
		m_Lock.SetActive(bIsLock);
		if(bIsLock)
		{
			m_iconBtn.gameObject.SetActive(false);
			m_upgradeBtn.gameObject.SetActive(false);
			m_purchaseBtn.gameObject.SetActive(false);
		}
		else
		{
			m_iconBtn.gameObject.SetActive(true);
			if (m_cUserData.arIsPurchaseFairy[m_nFairyNum])
			{
				m_upgradeBtn.gameObject.SetActive(true);
				m_purchaseBtn.gameObject.SetActive(false);
			}
			else
			{
				m_purchaseBtn.gameObject.SetActive(true);
				m_upgradeBtn.gameObject.SetActive(false);
			}
		}
	}

	private void Upgrade()
	{
		C_UpgradeManager.Instance.Upgrade(m_nFairyNum, m_iconBtn.gameObject.transform.position);
	}

	private void Purchase()
	{
		double dCost = m_arFairyUpgradeData[m_nFairyNum].fPurchaseCost;

		if (!m_cUserData.arIsPurchaseFairy[m_nFairyNum] &&
			m_cUserData.dCurrentStarLight >= dCost)
		{
			m_cUserData.dCurrentStarLight -= dCost;
			C_FairyManager.Instance.ActiveFairy(m_nFairyNum);
			if (m_cUserData.eLanguage == E_Language.korean)
			{
				C_DialogueController.Instance.Active(
					 string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_PurchaseDialogue,
			         C_STR.GetComleteWord(m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage], "을", "를"),
					 C_STR.GetComleteWord(m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage], "이", "가")),
					 C_BaseDataController.fWaitTimeDialogue_Explain);
			}
			else
			{
				C_DialogueController.Instance.Active(
				string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_PurchaseDialogue, m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage]),
				C_BaseDataController.fWaitTimeDialogue_Explain);
			}
			m_cUserData.arIsPurchaseFairy[m_nFairyNum] = true;

			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.purchase);

			if (m_nFairyNum + 1 != C_FairyManager.nFairyCount)
			{
				C_MainUIController.Instance.UnLock(m_nFairyNum + 1);
				C_MainUIController.Instance.UpdateElementText(m_nFairyNum + 1);
			}
			C_MainUIController.Instance.UpdateElementText(m_nFairyNum);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
            m_cStateUI.ActiveStarLightGainUI(E_StarLightType.starLightDefault, -dCost);

            m_upgradeBtn.gameObject.SetActive(true);
			m_purchaseBtn.gameObject.SetActive(false);
		}
	}

	private void explain()
	{
		if (!m_cUserData.arIsPurchaseFairy[m_nFairyNum])
		{
			if (m_cUserData.eLanguage == E_Language.korean)
			{
				C_DialogueController.Instance.Active(
				string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_PurchaseExplanation, 
				C_STR.GetComleteWord( m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage],"이","")),
				C_BaseDataController.fWaitTimeDialogue_Explain);
			}
			else
			{
				C_DialogueController.Instance.Active(
				string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_PurchaseExplanation, 
				m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage]),
				C_BaseDataController.fWaitTimeDialogue_Explain);
			}
		}
		else
		{
			float fUpgradeValue_Real = m_arFairyUpgradeData[m_nFairyNum].fValue_Upgrade;
			float fApplyValue_Real = C_UpgradeManager.Instance.GetApplyValue(m_nFairyNum);

			fUpgradeValue_Real = (fUpgradeValue_Real - 1.0f) * 100.0f;

			double dUpgradeValue_Expression = System.Math.Round(fUpgradeValue_Real, C_BaseDataController.nDecimalPlace_Expression);
			double dApplyValue_Expression = System.Math.Round(fApplyValue_Real, C_BaseDataController.nDecimalPlace_Expression);

			if (m_cUserData.eLanguage == E_Language.korean)
			{
				C_DialogueController.Instance.Active(
				string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_Explanation,
				C_STR.GetComleteWord( m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage],"이",""),
				C_STR.GetComleteWord(m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage], "이", "가"),
				C_StringHandler.Instance.GetUnitText(dUpgradeValue_Expression),
				C_StringHandler.Instance.GetUnitText(dApplyValue_Expression)),
				C_BaseDataController.fWaitTimeDialogue_Explain);
			}
			else
			{
				C_DialogueController.Instance.Active(
				string.Format(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Fairy_Explanation,
				m_arFairyUpgradeData[m_nFairyNum].arName[(int)m_cUserData.eLanguage],
				C_StringHandler.Instance.GetUnitText(dUpgradeValue_Expression),
				C_StringHandler.Instance.GetUnitText(dApplyValue_Expression)),
				C_BaseDataController.fWaitTimeDialogue_Explain);
			}
		}
	}
}
