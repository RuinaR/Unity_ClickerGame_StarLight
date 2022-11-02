using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GameSceneController : MonoBehaviour {

	public static C_GameSceneController Instance { get; private set; }
	public static bool IsInit = false;

	private C_UserData m_cUserData;
	private C_CharacterModeData[] m_arCharacterModeData;

	[SerializeField]
	private C_BackgroundNormalController m_cBackgroundController;
	[SerializeField]
	private C_PlayerController m_cPlayerController;
	[SerializeField]
	private C_SpeechBubbleController m_cBubbleController;

    [SerializeField]
    private GameObject m_textAutoSave;
	[SerializeField]
	private C_BonusObjController m_cBonus;
	[SerializeField]
	private C_CatController m_cCat;
	[SerializeField]
	private C_FeverBGController m_cFBG;

	
	/////
	[SerializeField]
	private string m_strTestDialogue;
	

	private void Init()
	{
		IsInit = false;
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arCharacterModeData = C_BaseDataController.Instance.GetCharacterModeData();

		m_cBubbleController.Init();
		C_UpgradeManager.Instance.Init();
		C_StarLightManager.Instance.Init(m_cFBG);
		C_FairyManager.Instance.Init();

		C_DialogueController.Instance.Init();
		C_FeverController.Instance.Init();
		C_SkillManager.Instance.Init();
		C_MainUIController.Instance.Init();
		C_PopUpController.Instance.Init();
		m_cFBG.Init();
		m_cBonus.Init();
		m_cCat.Init();

		m_cBackgroundController.Init();
		m_cPlayerController.Init(E_CharacterFaceType.faceDefault);


		m_cPlayerController.UpdateAll();
		IsInit = true;


		if (m_cUserData.bIsTutorialCompleted)
		{
			int nRandomGreetingIndex = 0;

			if (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] == C_BaseDataController.Instance.GetPlayerUpgradeData()[(int)E_PlayerUpgrade.vehicle].nLevelMax)
			{
				nRandomGreetingIndex = Random.Range(0, C_BaseDataController.arCharacterText_Greeting_End[(int)m_cUserData.eLanguage].Length);
				C_DialogueController.Instance.Active(C_BaseDataController.arCharacterText_Greeting_End[(int)m_cUserData.eLanguage][nRandomGreetingIndex],
													 C_BaseDataController.fWaitTimeDialogue_Talk);
			}
			else
			{
				nRandomGreetingIndex = Random.Range(0, C_BaseDataController.arCharacterText_Greeting[(int)m_cUserData.eLanguage].Length);
				C_DialogueController.Instance.Active(C_BaseDataController.arCharacterText_Greeting[(int)m_cUserData.eLanguage][nRandomGreetingIndex],
													 C_BaseDataController.fWaitTimeDialogue_Talk);
			}
		}
		else
		{
			C_DialogueController.Instance.Active_Force(C_BaseDataController.arTutorialText[(int)m_cUserData.eLanguage], C_BaseDataController.fWaitTimeDialogue_Talk);
			m_cUserData.bIsTutorialCompleted = true;
			C_UserDataController.Instance.Save();
		}

		if(!m_cUserData.bIsPurchaseNo_Ads)
		{
			//C_AdmobManager.Instance.ShowBanner();
		}

		Debug.Log("GC 초기화");
	}

    public IEnumerator CoroutineAutoSave()
    {
        WaitForSeconds waitTime = new WaitForSeconds(30.0f);
        while (true)
        {
            yield return waitTime;
            C_UserDataController.Instance.Save();
            m_textAutoSave.SetActive(true);
        }
    }
    ///////////////////////////////////////////////////////////////////////////////////////////


    private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Init();
		m_cPlayerController.StartCoroutineAction();
		m_cBackgroundController.MoveStart();

        StartCoroutine(CoroutineAutoSave());
	}

	////////////////////////////// TestBtn Func //////////////////////////////

	

	public void ChangeScene_For_Btn()
	{
		C_SceneManager.Instance.ChangeScene(E_Scene.start);
	}
	public void DialogueTest_For_Btn()
	{
		C_DialogueController.Instance.Active(m_strTestDialogue,
			C_BaseDataController.fWaitTimeDialogue_Talk);
	}
	public void Save_For_Btn()
	{
		C_UserDataController.Instance.Save();
    }
	public void GetStarLight_For_Btn()
	{
		m_cUserData.dCurrentStarLight = C_UpgradeManager.Instance.GetUpgradeCost(E_PlayerUpgrade.vehicle);
		C_MainUIController.Instance.UpdateUI_ChangeResource();
	}
	public void SetPurchaseTrue_For_Btn()
	{
		m_cUserData.bIsPurchaseNo_Ads = true;
		C_UserDataController.Instance.Save();
	}

}
