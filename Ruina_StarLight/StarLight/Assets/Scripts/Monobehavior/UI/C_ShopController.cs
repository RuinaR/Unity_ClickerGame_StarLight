using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ShopController : MonoBehaviour {

    [SerializeField]
    private C_StateUIController m_cStateUI;

	[SerializeField]
	private C_ElementController_Shop m_Element_NoAds;

	[SerializeField]
	private C_CatController m_cCat;

	[SerializeField]
	private GameObject m_Shop;

	private C_UserData m_cUserData;
	private C_PopUpController.delYesFunc purchase;

	public void Purchase_NoAds()
	{
		Debug.Log("광고 제거!");
		//C_AdmobManager.Instance.HideBanner();
		m_cCat.ActiveCat();
        m_cUserData.bIsPurchaseNo_Ads = true;

		C_UserDataController.Instance.Save();
		UpdateUI();
		C_MainUIController.Instance.UpdateUI_ChangeResource();

		m_Shop.SetActive(false);
		C_DialogueController.Instance.Active(C_BaseDataController.arCharacterText_NoAd[(int)m_cUserData.eLanguage], C_BaseDataController.fWaitTimeDialogue_Talk);
	}

	private void ActivePopUp_Purchase_NoAds()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			if (C_UserDataController.Instance.GetUserData().eLanguage == E_Language.korean)
			{
				C_PopUpController.Instance.ActivePopUp_OK("인터넷에 연결되어 있지 않으면 결제를 진행할 수 없어요.");
			}
			else
			{
				C_PopUpController.Instance.ActivePopUp_OK("If you're not connected to the Internet,\nYou can't proceed with the payment.");
			}
			return;
		}

		C_PopUpController.Instance.ActivePopUp_YESNO(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].PopUp_purchase,
													 purchase);
	}
	private void _purchase()
	{
		//C_IAPManager.Instance.Purchase("RemoveBannerAd");
	}
	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();

		purchase = _purchase;

		m_Element_NoAds.InitBtnFunc(ActivePopUp_Purchase_NoAds);
		m_Element_NoAds.SetText(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Shop_NoAds);
		if(m_cUserData.bIsPurchaseNo_Ads)
		{
			m_Element_NoAds.SetBtnInteractable(false);
		}
		else
		{
			m_Element_NoAds.SetBtnInteractable(true);
		}

	}

	public void UpdateUI()
	{
		m_Element_NoAds.SetText(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Shop_NoAds);
		if (m_cUserData.bIsPurchaseNo_Ads)
		{
			m_Element_NoAds.SetBtnInteractable(false);
		}
		else
		{
			m_Element_NoAds.SetBtnInteractable(true);
		}
	}
}
