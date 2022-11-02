using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FeverController : MonoBehaviour {

	public static C_FeverController Instance { get; private set; }

	private C_UserData m_cUserData;

	private bool m_bIsFever;

	[SerializeField]
	private GameObject m_cFeverBackground;
	private Animator m_FeverBackgroundAnimator;

	private Vector3 m_vec3OriginScale;
	private Vector3 m_vec3ApplyScale;

	IEnumerator CoroutineFever(float fFeverTime)
	{
		C_MainUIController.Instance.SetFeverBarLength(1.0f);
		C_MainUIController.Instance.SetFeverBarColor_Fever();
		m_cFeverBackground.transform.localScale = m_vec3OriginScale;

		float fElapsedTime = 0.0f;
		float fAddScaleValue = 5;
		
		m_bIsFever = true;
		C_SoundManager.Instance.ChangeBGM(E_BGM.fever);

		m_FeverBackgroundAnimator.SetTrigger(C_AnimationHashList.nFeverStart);
		while (fElapsedTime <= fFeverTime)
		{
			C_MainUIController.Instance.SetFeverBarLength(1.0f - (fElapsedTime / fFeverTime));
			m_vec3ApplyScale.x = m_vec3OriginScale.x + (fAddScaleValue * (fElapsedTime / fFeverTime));
			m_vec3ApplyScale.y = m_vec3OriginScale.y + (fAddScaleValue * (fElapsedTime / fFeverTime));
			m_vec3ApplyScale.z = 1;

			m_cFeverBackground.transform.localScale = m_vec3ApplyScale;
			
			yield return null;
			fElapsedTime += Time.deltaTime;
		}
		m_FeverBackgroundAnimator.SetTrigger(C_AnimationHashList.nFeverEnd);
		C_MainUIController.Instance.SetFeverBarColor_Default();
		m_bIsFever = false;

		if (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] >=
						C_BaseDataController.Instance.m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].nLevelMax)
		{
			C_SoundManager.Instance.ChangeBGM(E_BGM.game_ed);
		}
		else
		{
			C_SoundManager.Instance.ChangeBGM(E_BGM.game);
		}

		C_MainUIController.Instance.UpdateStateUI();
	}

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_FeverBackgroundAnimator = m_cFeverBackground.GetComponent<Animator>();
		m_vec3OriginScale = m_cFeverBackground.transform.localScale;
		m_bIsFever = false;
	}

	public bool IsFever()
	{
		return m_bIsFever;
	}

	public void UpdateFever()
	{
		C_MainUIController.Instance.SetFeverBarLength((float)m_cUserData.nCurrentFeverTapCount / C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.fever_TapCount));
		if (m_cUserData.nCurrentFeverTapCount >= (int)C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.fever_TapCount))
		{
			m_cUserData.nCurrentFeverTapCount = 0;
			StartCoroutine(CoroutineFever(C_UpgradeManager.Instance.GetApplyValue(E_PlayerUpgrade.fever_Time)));
		}
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			Init();
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
		
	}
}
