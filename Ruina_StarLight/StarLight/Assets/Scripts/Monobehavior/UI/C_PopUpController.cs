using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class C_PopUpController : MonoBehaviour {

	public static C_PopUpController Instance { get; private set; }

	[SerializeField]
	private GameObject PopUp;

	[SerializeField]
	private Text m_text;
	[SerializeField]
	private Button m_Btn_OK;
	[SerializeField]
	private Button m_Btn_YES;
	[SerializeField]
	private Button m_Btn_NO;


	[SerializeField]
	private Text m_textOK;
	[SerializeField]
	private Text m_textYES;
	[SerializeField]
	private Text m_textNO;

	private C_UserData m_cUserData;

	public delegate void delYesFunc();
	private delYesFunc m_delYesFunc;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();

		UpdateBtnText();

		m_delYesFunc = null;
		m_text.text = "";

		m_Btn_OK.onClick.AddListener(SetActiveFalse);
		m_Btn_YES.onClick.AddListener(SetActiveFalse);
		m_Btn_NO.onClick.AddListener(SetActiveFalse);

		m_Btn_YES.onClick.AddListener(YesBtnFunc);
		SetActiveFalse();
	}

	private void SetActiveFalse()
	{
		PopUp.SetActive(false);
	}

	private void YesBtnFunc()
	{
		m_delYesFunc();
		m_delYesFunc = null;
	}

	public void ActivePopUp_OK(string strText)
	{
		m_text.text = strText;
		PopUp.SetActive(true);
		m_Btn_OK.gameObject.SetActive(true);
		m_Btn_YES.gameObject.SetActive(false);
		m_Btn_NO.gameObject.SetActive(false);
	}

	public void ActivePopUp_YESNO(string strText, delYesFunc BtnYesFunc)
	{
		m_text.text = strText;
		m_delYesFunc = BtnYesFunc;

		PopUp.SetActive(true);
		m_Btn_OK.gameObject.SetActive(false);
		m_Btn_YES.gameObject.SetActive(true);
		m_Btn_NO.gameObject.SetActive(true);
	}

	public void UpdateBtnText()
	{
		m_textOK.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].ok;
		m_textYES.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].yes;
		m_textNO.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].no;
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(PopUp.activeInHierarchy)
			{
				SetActiveFalse();
			}
			else
			{
				ActivePopUp_YESNO(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].PopUp_gameEnd, Application.Quit);
			}
		}
	}


}
