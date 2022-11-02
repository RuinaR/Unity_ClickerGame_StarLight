using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BonusObjController : MonoBehaviour {

	[SerializeField]
	private GameObject m_BonusObj;
	[SerializeField]
	private Collider m_col;

	private C_StarLightManager m_cStarManager;
	private C_UserData m_cUserData;

	private Rigidbody2D m_rb;
	private SpriteRenderer m_sr;
	private Sprite[] m_arSprite;

	[SerializeField]
	private GameObject m_Effect_Active;
	[SerializeField]
	private GameObject m_Effect_Get;


	public void Init()
	{
		m_arSprite = C_SpriteManager.Instance.GetSpriteArrayBonusObj();

		m_rb = m_BonusObj.GetComponent<Rigidbody2D>();
		m_sr = m_BonusObj.GetComponent<SpriteRenderer>();

		m_BonusObj.SetActive(false);

		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_cStarManager = FindObjectOfType<C_StarLightManager>();

		StartCoroutine(CoroutineObj());
	}

	private void Active()
	{
		int nRandomSpriteIndex = Random.Range(0, m_arSprite.Length);
		m_sr.sprite = m_arSprite[nRandomSpriteIndex];
		m_BonusObj.SetActive(true);

		m_Effect_Active.transform.position = m_BonusObj.transform.position;
		m_Effect_Active.SetActive(true);
		C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.Bonus_A);
	}

    private void GetBonus()
    {
		double dStarLightGain = m_cStarManager.GetStarLightValuePerTap(E_StarLightType.starLightDefault) * 25;

		if (m_cUserData.dCurrentStarLight + dStarLightGain < C_StringHandler.MAX)
		{
			m_cUserData.dCurrentStarLight += dStarLightGain;
			C_MainUIController.Instance.ActiveStarLightGainUI(E_StarLightType.starLightDefault, dStarLightGain);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}
		else
		{
			m_cUserData.dCurrentStarLight = C_StringHandler.MAX;
			C_MainUIController.Instance.ActiveStarLightGainUI(E_StarLightType.starLightDefault, 0.0);
			C_MainUIController.Instance.UpdateUI_ChangeResource();
		}

		m_Effect_Get.transform.position = m_BonusObj.transform.position;
		m_Effect_Get.SetActive(true);
		C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.Bonus_Get);

	}

	private IEnumerator CoroutineObj()
	{
		float fWaitTime = 0;
		float fElapsedTime = 0.0f;
		float fUpdateTime = 0.1f;
		WaitForSeconds UpdateTime = new WaitForSeconds(fUpdateTime);
		while(true)
		{
			if (!m_BonusObj.activeInHierarchy)
			{
				fElapsedTime = 0.0f;
				fWaitTime = Random.Range(180.0f, 300.0f);
				while (fElapsedTime < fWaitTime)
				{
					//Debug.Log(fElapsedTime);

					yield return UpdateTime;
					fElapsedTime += fUpdateTime;
				}
				Active();
			}
			yield return UpdateTime;
		}
	}


	private void Update()
	{
		if (C_TapController.Instance.Tap(m_col))
		{
			if(m_BonusObj.activeInHierarchy)
			{
				m_BonusObj.SetActive(false);
				GetBonus();
			}
		}
	}
}
