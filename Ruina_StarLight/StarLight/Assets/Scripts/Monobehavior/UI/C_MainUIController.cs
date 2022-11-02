using UnityEngine;
using UnityEngine.UI;

public class C_MainUIController : MonoBehaviour {

	public static C_MainUIController Instance;

    private C_UserData m_cUserData;

	private Animator m_animator;
	private bool m_bIsActive;

    [SerializeField]
    private Text m_textAutoSave;

	[SerializeField]
	private Button m_btnUIActive;

	[SerializeField]
	private C_StateUIController m_cStateUI;

	[SerializeField]
	private C_OptionController m_cOption;
	[SerializeField]
	private C_ShopController m_cShop;
	
	[SerializeField]
	private C_ChangeMode m_cChangeMode;

	[SerializeField]
	private C_SubUIController[] m_arSubUI;
	private int m_nCurrentActiveSubUIIndex;

	public void Init()
	{
        m_cUserData = C_UserDataController.Instance.GetUserData();
        m_textAutoSave.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].AutoSave;

        m_btnUIActive.onClick.AddListener(MainUIActiveTrigger_ForBtn);
		m_animator = GetComponent<Animator>();
		m_cStateUI.Init();
		m_cOption.Init();
		m_cShop.Init();
		m_cChangeMode.Init();
		for (int i = 0; i < m_arSubUI.Length; i++)
		{
			m_arSubUI[i].Init();
		}
		AllSubUIActiveFalse();
		m_nCurrentActiveSubUIIndex = 0;
		m_bIsActive = false;
		UpdateStateUI();
	}

	public bool IsUIActive()
	{
		return m_bIsActive;
	}

	public E_SubUI GetCurrentActiveSubUI()
	{
		return (E_SubUI)m_nCurrentActiveSubUIIndex;
	}

	public void UpdateUI_ChangeResource()
	{
		UpdateStateUI();
		if (IsUIActive())
		{
			m_arSubUI[m_nCurrentActiveSubUIIndex].UpdateUI_ChangeResource();
		}
	}

	public void UpdateUIAll()
	{
		UpdateStateUI();
		C_PopUpController.Instance.UpdateBtnText();
		m_cOption.UpdateText();
		m_cShop.UpdateUI();
		m_cChangeMode.UpdateText();
		for (int i = 0; i < m_arSubUI.Length; i++)
		{
			m_arSubUI[i].UpdateUIALL();
		}
        m_textAutoSave.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].AutoSave;
    }

	public void UpdateStateUI()
	{
		m_cStateUI.UpdateUI();
	}

	public void ActiveStarLightGainUI(E_StarLightType eType, double StarLightGain)
	{
		m_cStateUI.ActiveStarLightGainUI(eType, StarLightGain);
	}

	public void UpdateElementBtn(E_PlayerUpgrade eUpgrade)
	{
		((C_PlayerUpgradeUIController)m_arSubUI[(int)E_SubUI.playerUpgrade]).UpdateUpgradeElement_Btn(eUpgrade);
	}
	public void UpdateElementBtn(E_StarLightType eType)
	{
		((C_StarLightUpgradeUIController)m_arSubUI[(int)E_SubUI.starLightUpgrade]).UpdateUpgradeElement_Btn(eType);
	}
	public void UpdateElementBtn(int nFairyNum)
	{
		((C_FairyUpgradeUIController)m_arSubUI[(int)E_SubUI.FairyUpgrade]).UpdateUpgradeElement_Btn(nFairyNum);
	}

	public void UpdateElementText(E_PlayerUpgrade eUpgrade)
	{
		((C_PlayerUpgradeUIController)m_arSubUI[(int)E_SubUI.playerUpgrade]).UpdateUpgradeElement_Text(eUpgrade);
	}
	public void UpdateElementText(E_StarLightType eType)
	{
		((C_StarLightUpgradeUIController)m_arSubUI[(int)E_SubUI.starLightUpgrade]).UpdateUpgradeElement_Text(eType);
	}
	public void UpdateElementText(int nFairyNum)
	{
		((C_FairyUpgradeUIController)m_arSubUI[(int)E_SubUI.FairyUpgrade]).UpdateUpgradeElement_Text(nFairyNum);
	}

	public void UnLock(int nFairyNum)
	{
		((C_FairyUpgradeUIController)m_arSubUI[(int)E_SubUI.FairyUpgrade]).UnLockElement(nFairyNum);
	}

	public void SetFeverBarLength(float fFillAmount)
	{
		m_cStateUI.SetFeverBarLength(fFillAmount);
	}

	public void SetFeverBarColor_Default()
	{
		m_cStateUI.SetFeverBarColor_Default();
	}

	public void SetFeverBarColor_Fever()
	{
		m_cStateUI.SetFeverBarColor_Fever();
	}

	private void AllSubUIActiveFalse()
	{
		for (int i = 0; i < m_arSubUI.Length; i++)
		{
			m_arSubUI[i].gameObject.SetActive(false);
		}
	}

	public void ActiveSubUI(int nIndex)
	{
		m_nCurrentActiveSubUIIndex = nIndex;
		m_arSubUI[m_nCurrentActiveSubUIIndex].gameObject.SetActive(true);
		for (int i = 0; i < m_arSubUI.Length; i++)
		{
			if(m_nCurrentActiveSubUIIndex != i)
			{
				m_arSubUI[i].gameObject.SetActive(false);
			}
		}
		UpdateUI_ChangeResource();
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}
	
	public void MainUIActiveTrigger_ForBtn()
	{
		if(IsUIActive())
		{
			m_animator.SetTrigger(C_AnimationHashList.nMainUIDisable);
			m_bIsActive = false;
			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.UI_D);
		}
		else
		{
			m_animator.SetTrigger(C_AnimationHashList.nMainUIActive);
			m_bIsActive = true;
			ActiveSubUI(m_nCurrentActiveSubUIIndex);
			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.UI_A);
		}
	}

	public void MainUIActiveFalse()
	{
		if (IsUIActive())
		{
			m_animator.SetTrigger(C_AnimationHashList.nMainUIDisable);
			m_bIsActive = false;
		}
	}

	private void SetActiveButtonInteractable_False_forAnim()
	{
		m_btnUIActive.interactable = false;
	}
	private void SetActiveButtonInteractable_True_forAnim()
	{
		m_btnUIActive.interactable = true;
	}

}
