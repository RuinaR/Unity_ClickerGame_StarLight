using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StarLightManager : MonoBehaviour {

	public static C_StarLightManager Instance { get; private set; }
	public static Color[] arColorByStarLightType;

	[SerializeField]
	private C_StarLightController m_cOrigin;
	[SerializeField]
	private C_StarLightPool m_cStarLightPool;
	[SerializeField]
	private Collider2D m_col2dStarLightBoundary;
	[SerializeField]
	private Collider m_colStarLightCreate;
	private C_FeverBGController m_cFBG;

	private C_PlayerController m_cPlayer;
	private List<C_StarLightController> m_listStarLight;

	private C_UserData m_cUserData;
	private C_CharacterModeData[] m_arCharacterModeData;

	private RaycastHit m_hitInfo;
	private Vector3 m_vec3StarLightRandomPos;

	private double m_dStarLightGain_Tap;
	private double m_dStarLightGain_Fairy;

	private bool m_bIsInitialize = false;

	public void Init(C_FeverBGController c_FBG)
	{
		m_cPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<C_PlayerController>();
		m_cFBG = c_FBG;
		if (!m_bIsInitialize)
		{
			m_cStarLightPool.Init(m_cOrigin);
			m_cUserData = C_UserDataController.Instance.GetUserData();
			m_arCharacterModeData = C_BaseDataController.Instance.GetCharacterModeData();
			m_dStarLightGain_Tap = 0.0d;
			m_dStarLightGain_Fairy = 0.0d;
			m_listStarLight = m_cStarLightPool.GetList();

			arColorByStarLightType = new Color[(int)E_StarLightType.max];
			arColorByStarLightType[(int)E_StarLightType.starLightDefault] = Color.yellow;
			arColorByStarLightType[(int)E_StarLightType.starLightBlue] = Color.blue;
			arColorByStarLightType[(int)E_StarLightType.starLightRed] = Color.red;
			arColorByStarLightType[(int)E_StarLightType.starLightFairy] = Color.green;

			m_bIsInitialize = true;
		}
	}

	private bool GetRandom(float fPercentage)
	{
		if (Random.value <= fPercentage)
		{
			return true;
		}
		return false;
	}

	private double GetStarLightValueByFairy(int nFairyNum)
	{		
		return (double)C_UpgradeManager.Instance.GetApplyValue(nFairyNum) *
			   (double)C_SkillManager.fSkill_StarLight_Buff;
	}

	public double GetStarLightValuePerTap(E_StarLightType eType)
	{
		if(eType == E_StarLightType.starLightFairy)
		{
			return 0.0d;
		}

		return
		(double)C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.vehicle) *
		(double)C_UpgradeManager.Instance.GetApplyValue(eType) *
		(double)m_arCharacterModeData[(int)m_cUserData.eCharacterMode].arStarLightTypeCoefficient[(int)eType] *
		(double)C_SkillManager.fSkill_StarLight_Buff;
	}

    public double GetStarLightValuePerTap_NoBuff(E_StarLightType eType)
    {
        if (eType == E_StarLightType.starLightFairy)
        {
            return 0.0d;
        }

        return
        (double)C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.vehicle) *
        (double)C_UpgradeManager.Instance.GetApplyValue(eType) *
        (double)m_arCharacterModeData[(int)m_cUserData.eCharacterMode].arStarLightTypeCoefficient[(int)eType];
    }

    public void GetStarLightByFairy(int nFairyNum, Transform tf)
	{
		C_StarLightController newStarLight = m_cStarLightPool.GetFromPool();
		newStarLight.SetStarLightType(E_StarLightType.starLightFairy);
		newStarLight.SetTfPosition(tf.position);
		newStarLight.SetColorClear();
		newStarLight.SetParticleActive(false);
		newStarLight.gameObject.SetActive(true);

		C_EffectController effectCreate = C_EffectManager.Instance.GetEffect(E_Effect.starLight_Create);
		effectCreate.gameObject.transform.position = tf.position;

		effectCreate.SetStarLight(newStarLight);

		effectCreate.gameObject.SetActive(true);

		m_dStarLightGain_Fairy = GetStarLightValueByFairy(nFairyNum);

		if (m_cUserData.dCurrentStarLight + m_dStarLightGain_Fairy < C_StringHandler.MAX)
		{
			m_cUserData.dCurrentStarLight += m_dStarLightGain_Fairy;
			C_MainUIController.Instance.ActiveStarLightGainUI(E_StarLightType.starLightFairy, m_dStarLightGain_Fairy);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
		else
		{
			m_cUserData.dCurrentStarLight = C_StringHandler.MAX;
			C_MainUIController.Instance.ActiveStarLightGainUI(E_StarLightType.starLightFairy, 0.0);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
	}

	private E_StarLightType GetStarLightCreateType()
	{
		E_StarLightType eStarLightTypeCreate;
		if (C_FeverController.Instance.IsFever())
		{
			eStarLightTypeCreate = E_StarLightType.starLightRed;
		}
		else
		{
			eStarLightTypeCreate = E_StarLightType.starLightDefault;
			if (GetRandom(m_arCharacterModeData[(int)m_cUserData.eCharacterMode].fStarLightCriticalPercentage +
				C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.starLightCritical_Percentage)))
			{
				eStarLightTypeCreate = E_StarLightType.starLightBlue;
			}
		}
		return eStarLightTypeCreate;
	}

	private void GetStarLightByPlayerTap(Vector3 vec3EffectPos)
	{
		if (C_MainUIController.Instance.IsUIActive())
		{
			m_vec3StarLightRandomPos.y = Random.Range(C_ScreenArea.fY_Bottom_UI, C_ScreenArea.fY_Top);

		}
		else
		{
			m_vec3StarLightRandomPos.y = Random.Range(C_ScreenArea.fY_Bottom, C_ScreenArea.fY_Top);
		}
		m_vec3StarLightRandomPos.x = Random.Range(C_ScreenArea.fX_Left, C_ScreenArea.fX_Right);

	    E_StarLightType eStarLightTypeCreate = GetStarLightCreateType();
		
		C_StarLightController newStarLight = m_cStarLightPool.GetFromPool(); 
		newStarLight.SetStarLightType(eStarLightTypeCreate);
		newStarLight.SetTfPosition(m_vec3StarLightRandomPos);
		newStarLight.SetColorClear();
		newStarLight.SetParticleActive(false);
		newStarLight.gameObject.SetActive(true);

		GameObject effectTap = C_EffectManager.Instance.GetEffect_StarLightTap(eStarLightTypeCreate).gameObject;
		effectTap.transform.position = vec3EffectPos;
		effectTap.SetActive(true);

		C_EffectController effectCreate = C_EffectManager.Instance.GetEffect(E_Effect.starLight_Create);
		effectCreate.gameObject.transform.position = m_vec3StarLightRandomPos;

		effectCreate.SetStarLight(newStarLight);
		
		effectCreate.gameObject.SetActive(true);

		C_SoundManager.Instance.ActiveSoundEffect_StarLightTap(eStarLightTypeCreate);

		if (C_FeverController.Instance.IsFever())
		{
			m_cFBG.ShakeBG(0.5f, 0.2f);
			m_cPlayer.GetAnimator().SetTrigger(C_AnimationHashList.nTap);
		}
		else
		{
			m_cUserData.nCurrentFeverTapCount++;
			C_FeverController.Instance.UpdateFever();
		}

		m_dStarLightGain_Tap = GetStarLightValuePerTap(eStarLightTypeCreate);

		if (m_cUserData.dCurrentStarLight + m_dStarLightGain_Tap < C_StringHandler.MAX)
		{
			m_cUserData.dCurrentStarLight += m_dStarLightGain_Tap;
			C_MainUIController.Instance.ActiveStarLightGainUI(eStarLightTypeCreate, m_dStarLightGain_Tap);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
		else
		{
			m_cUserData.dCurrentStarLight = C_StringHandler.MAX;
			C_MainUIController.Instance.ActiveStarLightGainUI(eStarLightTypeCreate, 0.0);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
	}

	public void GetStarLightBySkill_AutoTap()
	{
		if (C_MainUIController.Instance.IsUIActive())
		{
			m_vec3StarLightRandomPos.y = Random.Range(C_ScreenArea.fY_Bottom_UI, C_ScreenArea.fY_Top);

		}
		else
		{
			m_vec3StarLightRandomPos.y = Random.Range(C_ScreenArea.fY_Bottom, C_ScreenArea.fY_Top);
		}
		m_vec3StarLightRandomPos.x = Random.Range(C_ScreenArea.fX_Left, C_ScreenArea.fX_Right);

		E_StarLightType eStarLightTypeCreate = GetStarLightCreateType();

		C_StarLightController newStarLight = m_cStarLightPool.GetFromPool();
		newStarLight.SetStarLightType(eStarLightTypeCreate);
		newStarLight.SetTfPosition(m_vec3StarLightRandomPos);
		newStarLight.SetColorClear();
		newStarLight.SetParticleActive(false);
		newStarLight.gameObject.SetActive(true);

		C_EffectController effectCreate = C_EffectManager.Instance.GetEffect(E_Effect.starLight_Create);
		effectCreate.gameObject.transform.position = m_vec3StarLightRandomPos;

		effectCreate.SetStarLight(newStarLight);

		effectCreate.gameObject.SetActive(true);

		C_SoundManager.Instance.ActiveSoundEffect_StarLightTap(eStarLightTypeCreate);

		if (C_FeverController.Instance.IsFever())
		{
			m_cFBG.ShakeBG(0.5f, 0.2f);
			m_cPlayer.GetAnimator().SetTrigger(C_AnimationHashList.nTap);
		}
		else
		{
			m_cUserData.nCurrentFeverTapCount++;
			C_FeverController.Instance.UpdateFever();
		}

		m_dStarLightGain_Tap = GetStarLightValuePerTap(eStarLightTypeCreate);

		if (m_cUserData.dCurrentStarLight + m_dStarLightGain_Tap < C_StringHandler.MAX)
		{
			m_cUserData.dCurrentStarLight += m_dStarLightGain_Tap;
			C_MainUIController.Instance.ActiveStarLightGainUI(eStarLightTypeCreate, m_dStarLightGain_Tap);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
		else
		{
			m_cUserData.dCurrentStarLight = C_StringHandler.MAX;
			C_MainUIController.Instance.ActiveStarLightGainUI(eStarLightTypeCreate, 0.0);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
	}

	public void AllStarLightSetActiveFalse()
	{
		if (m_bIsInitialize)
		{
			foreach(C_StarLightController starLight in m_listStarLight)
			{
				starLight.ClearRb();
				starLight.gameObject.SetActive(false);
			}
		}
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((C_SceneManager.Instance.GetScene() != E_Scene.main) ||
			!m_bIsInitialize)
		{
			return;
		}

		if (C_TapController.Instance.Tap(m_colStarLightCreate, ref m_hitInfo)) 
		{
			GetStarLightByPlayerTap(m_hitInfo.point);
		}
	}
}
