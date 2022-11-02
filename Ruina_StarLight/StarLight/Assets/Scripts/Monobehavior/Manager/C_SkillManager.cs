using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class C_SkillManager : MonoBehaviour {

	public static C_SkillManager Instance { get; private set; }

	public static float fSkill_StarLight_Buff;

	private E_Skill m_eSkill;

	private C_SkillData[] m_arSkillData;
	private C_UserData m_cUserData;

	[SerializeField]
	private C_SkillUIController m_cSkillUIController;
	[SerializeField]
	private C_ChangeMode m_cChangeMode;

	[SerializeField]
	private GameObject m_AutoTapTimeBar;
	[SerializeField]
	private Image m_IMGAutoTapTimeBar;

	[SerializeField]
	private GameObject m_BuffTimeBar;
	[SerializeField]
	private Image m_IMGBuffTimeBar;

	[SerializeField]
	private GameObject m_SkillEffect_AutoTap;
	[SerializeField]
	private GameObject m_SkillEffect_Buff;

	[SerializeField]
	private GameObject m_SkillEffect_PowerTap;


	private delegate void delActiveSkill();
	private delActiveSkill[] m_arDelActiveSkill;

	private bool m_bIsActive_AutoTap;
	private bool m_bIsActive_Buff;
	
	public void Init()
	{
		m_bIsActive_AutoTap = false;
		m_bIsActive_Buff = false;
		fSkill_StarLight_Buff = 1.0f;

		m_arSkillData = C_BaseDataController.Instance.GetSkillData();
		m_cUserData = C_UserDataController.Instance.GetUserData();

		m_arDelActiveSkill = new delActiveSkill[(int)E_Skill.max];
		m_arDelActiveSkill[(int)E_Skill.initCooldown] = Skill_InitCooldown;
		m_arDelActiveSkill[(int)E_Skill.changeMode] = Skill_ChangeMode;
		m_arDelActiveSkill[(int)E_Skill.powerTap] = Skill_PowerTap;
		m_arDelActiveSkill[(int)E_Skill.autoTap] = Skill_AutoTap;
		m_arDelActiveSkill[(int)E_Skill.buff] = Skill_Buff;


		m_AutoTapTimeBar.gameObject.SetActive(false);

		m_SkillEffect_AutoTap.SetActive(false);
		m_SkillEffect_Buff.SetActive(false);


		for (int i = 0; i < (int)E_Skill.max; i++)
		{
			if (m_cUserData.arSkillCooldown[i] > 0)
			{
				StartCoroutine(CoroutineCooldown_Init((E_Skill)i));
			}
		}

		m_AutoTapTimeBar.SetActive(false);
		m_BuffTimeBar.SetActive(false);
	}

	public bool IsAbleToActive(E_Skill eSkill)
	{
		if (m_cUserData.arSkillCooldown[(int)eSkill] == 0)
		{
			return true;
		}
		return false;
	}

	public void ActiveSkill()
	{
		m_arDelActiveSkill[(int)m_eSkill]();
		StartCoroutine(CoroutineCooldown_ActiveSkill(m_eSkill));
	}

	public void ActiveSkillPopUp(E_Skill eSkill)
	{
		m_eSkill = eSkill;
		if (m_eSkill == E_Skill.initCooldown)
		{
			bool bIsAbleInitCooldown = false;

			for(int i = 0; i < (int)E_Skill.max; i++)
			{
				if (m_cUserData.arSkillCooldown[i] > 0)
				{
					bIsAbleInitCooldown = true;
				}
			}

			if (bIsAbleInitCooldown)
			{
				if (m_cUserData.eLanguage == E_Language.korean)
				{
					//C_PopUpController.Instance.ActivePopUp_YESNO("이 스킬은 보상형 광고 시청 후 사용할 수 있어요.\n광고를 보고 스킬을 사용할래요?", C_AdmobManager.Instance.UserChoseToWatchAd);
				}
				else
				{
					//C_PopUpController.Instance.ActivePopUp_YESNO("You can use this skill after watching a reward-type advertisement.\nDo you want to use skill after watching the ad?", C_AdmobManager.Instance.UserChoseToWatchAd);
				}
			}
			else
			{
				C_PopUpController.Instance.ActivePopUp_OK(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].PopUp_NoSkillCanCooldownInit);
			}
		}
		else
		{
			if ((m_eSkill == E_Skill.autoTap && m_bIsActive_AutoTap) ||
				(m_eSkill == E_Skill.buff && m_bIsActive_Buff))
			{
				C_PopUpController.Instance.ActivePopUp_OK(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].PopUp_CantActiveSkillInDuration);
			}
			else
			{
				C_PopUpController.Instance.ActivePopUp_YESNO(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].PopUp_skillActive, ActiveSkill);
			}
		}
	}

	IEnumerator CoroutineSkillEffect_Active(GameObject Effect, float fTime)
	{
		SpriteRenderer sr = Effect.GetComponent<SpriteRenderer>();
		Effect.SetActive(true);

		Color color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		sr.color = color;
		float fElapsedTime = 0.0f;
		while(fElapsedTime <= fTime)
		{
			color.a = fElapsedTime / fTime;
			sr.color = color;
			yield return null;
			fElapsedTime += Time.deltaTime;
		}
	}

	IEnumerator CoroutineSkillEffect_Disable(GameObject Effect, float fTime)
	{
		SpriteRenderer sr = Effect.GetComponent<SpriteRenderer>();

		Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		sr.color = color;
		float fElapsedTime = 0.0f;
		while (fElapsedTime <= fTime)
		{
			color.a = 1.0f - (fElapsedTime / fTime);
			sr.color = color;
			yield return null;
			fElapsedTime += Time.deltaTime;
		}
		Effect.SetActive(false);
	}

	IEnumerator CoroutineCooldown_ActiveSkill(E_Skill eSkill)
	{
		m_cSkillUIController.SetActiveBtnInteractable(eSkill, false);
		m_cUserData.arSkillCooldown[(int)eSkill] = m_arSkillData[(int)eSkill].nCooldownTime;
		float fWaitTime = 1.0f;
		WaitForSeconds waitTime = new WaitForSeconds(fWaitTime);
		while(m_cUserData.arSkillCooldown[(int)eSkill] > 0)
		{
			m_cUserData.arSkillCooldown[(int)eSkill] -= (int)fWaitTime;
			if (C_MainUIController.Instance.IsUIActive() &&
				C_MainUIController.Instance.GetCurrentActiveSubUI() == E_SubUI.skill)
			{
				m_cSkillUIController.UpdateCooldownText(eSkill);
				m_cSkillUIController.SetCoolDownIMGFillAmount(eSkill, 
					(float)m_cUserData.arSkillCooldown[(int)eSkill] / (float)m_arSkillData[(int)eSkill].nCooldownTime);
			}
			yield return waitTime;
		}
		m_cUserData.arSkillCooldown[(int)eSkill] = 0;
		if (C_MainUIController.Instance.IsUIActive() &&
				C_MainUIController.Instance.GetCurrentActiveSubUI() == E_SubUI.skill)
		{
			m_cSkillUIController.UpdateSkillElement(eSkill);
			m_cSkillUIController.SetActiveBtnInteractable(eSkill, true);
		}
	}

	IEnumerator CoroutineCooldown_Init(E_Skill eSkill)
	{
		WaitForSeconds loadUpdateTime = new WaitForSeconds(0.1f);

		while (!C_GameSceneController.IsInit)
		{
			yield return loadUpdateTime;
		}
		m_cSkillUIController.SetActiveBtnInteractable(eSkill, false);
		float fWaitTime = 1.0f;
		WaitForSeconds waitTime = new WaitForSeconds(fWaitTime);
		while (m_cUserData.arSkillCooldown[(int)eSkill] > 0)
		{
			m_cUserData.arSkillCooldown[(int)eSkill] -= (int)fWaitTime;

			if (C_MainUIController.Instance.IsUIActive() &&
				C_MainUIController.Instance.GetCurrentActiveSubUI() == E_SubUI.skill)
			{
				m_cSkillUIController.UpdateCooldownText(eSkill);
				m_cSkillUIController.SetCoolDownIMGFillAmount(eSkill,
					(float)m_cUserData.arSkillCooldown[(int)eSkill] / (float)m_arSkillData[(int)eSkill].nCooldownTime);
			}
			yield return waitTime;
		}
		m_cUserData.arSkillCooldown[(int)eSkill] = 0;

		if (C_MainUIController.Instance.IsUIActive() &&
				C_MainUIController.Instance.GetCurrentActiveSubUI() == E_SubUI.skill)
		{
			m_cSkillUIController.UpdateSkillElement(eSkill);
			m_cSkillUIController.SetActiveBtnInteractable(eSkill, true);
		}
	}

	IEnumerator CoroutineAutoTap(float fTime, int nTapPerSec)
	{
		m_bIsActive_AutoTap = true;

		float fElapsedTime = 0.0f;
		float fWaitTime = 1.0f / (float)nTapPerSec;
		WaitForSeconds waitTime = new WaitForSeconds(fWaitTime);

		m_AutoTapTimeBar.SetActive(true);
		m_IMGAutoTapTimeBar.fillAmount = 1.0f;

		StartCoroutine(CoroutineSkillEffect_Active(m_SkillEffect_AutoTap, 2.0f));

		while (fElapsedTime <= fTime)
		{
			C_StarLightManager.Instance.GetStarLightBySkill_AutoTap();
			m_IMGAutoTapTimeBar.fillAmount = Mathf.Clamp01(1.0f - (fElapsedTime / fTime));
			yield return waitTime;
			fElapsedTime += fWaitTime;
		}

		StartCoroutine(CoroutineSkillEffect_Disable(m_SkillEffect_AutoTap, 2.0f));

		m_AutoTapTimeBar.SetActive(false);

		m_bIsActive_AutoTap = false;
	}

	IEnumerator CoroutineBuff(float fTime, float fValue)
	{
		m_bIsActive_Buff = true;

		float fElapsedTime = 0.0f;
		float fWaitTime = 0.1f;
		WaitForSeconds waitTime = new WaitForSeconds(fWaitTime);

		m_BuffTimeBar.SetActive(true);
		m_IMGBuffTimeBar.fillAmount = 1.0f;
		StartCoroutine(CoroutineSkillEffect_Active(m_SkillEffect_Buff, 2.0f));

		fSkill_StarLight_Buff = fValue;

		while (fElapsedTime <= fTime)
		{
			m_IMGBuffTimeBar.fillAmount = Mathf.Clamp01(1.0f - (fElapsedTime / fTime));
			yield return waitTime;
			fElapsedTime += fWaitTime;
		}

		StartCoroutine(CoroutineSkillEffect_Disable(m_SkillEffect_Buff, 2.0f));

		fSkill_StarLight_Buff = 1.0f;
		m_BuffTimeBar.SetActive(false);

		m_bIsActive_Buff = false;
	}

	private void Skill_InitCooldown()
	{
		bool bInit = false;

		for (int i = 0; i < (int)E_Skill.max; i++)
		{

			if (m_cUserData.arSkillCooldown[i] > 0)
			{
				bInit = true;
			}
			else
			{
				bInit = false;
			}

			if (bInit)
			{
				m_cUserData.arSkillCooldown[i] = 0;

				if (C_MainUIController.Instance.IsUIActive() &&
				C_MainUIController.Instance.GetCurrentActiveSubUI() == E_SubUI.skill)
				{
					m_cSkillUIController.UpdateCooldownText((E_Skill)i);
					m_cSkillUIController.SetCoolDownIMGFillAmount((E_Skill)i,
						(float)m_cUserData.arSkillCooldown[i] / (float)m_arSkillData[i].nCooldownTime);

					m_cSkillUIController.UpdateSkillElement((E_Skill)i);
					m_cSkillUIController.SetActiveBtnInteractable((E_Skill)i, true);
				}
			}
		}
		C_PopUpController.Instance.ActivePopUp_OK(C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].Skill_InitCooldown);

	}

	private void Skill_ChangeMode()
	{
		m_cChangeMode.ActiveWindow();
	}

	private void Skill_PowerTap()
	{
		m_SkillEffect_PowerTap.SetActive(true);
		C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.skill_PowerTap);

		double dGetValue = C_StarLightManager.Instance.GetStarLightValuePerTap(E_StarLightType.starLightDefault) * (double)100;
		m_cUserData.dCurrentStarLight += dGetValue;

		C_MainUIController.Instance.ActiveStarLightGainUI(E_StarLightType.starLightDefault, dGetValue);
		C_MainUIController.Instance.UpdateUI_ChangeResource();
	}

	private void Skill_AutoTap()
	{
		StartCoroutine(CoroutineAutoTap(60.0f, 10));
	}

	private void Skill_Buff()
	{
		StartCoroutine(CoroutineBuff(60.0f, 2.0f));
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
}
