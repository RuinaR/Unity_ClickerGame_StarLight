using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class C_UserDataController : MonoBehaviour {

	public static C_UserDataController Instance { get; private set; }

	private C_UserData m_cUserData;

	private void OnApplicationQuit()
	{
		Save();
	}

	public void Load()
	{
		string strUserData = PlayerPrefs.GetString("UserData");
		if (string.IsNullOrEmpty(strUserData))
		{
			Make_InitUserData_And_Save();
		}
		else
		{
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream stream = new MemoryStream(Convert.FromBase64String(strUserData));
			m_cUserData = (C_UserData)bf.Deserialize(stream);
			stream.Close();


			if (m_cUserData.arPlayerUpgradeLevel == null)
			{
				m_cUserData.arPlayerUpgradeLevel = new int[(int)E_PlayerUpgrade.max];
			}
			if (m_cUserData.arStarLightUpgradeLevel == null)
			{
				m_cUserData.arStarLightUpgradeLevel = new int[(int)E_StarLightType.max];
			}
			if (m_cUserData.arIsPurchaseFairy == null)
			{
				m_cUserData.arIsPurchaseFairy = new bool[C_FairyManager.nFairyCount];
			}
			if (m_cUserData.arFairyUpgradeLevel == null)
			{
				m_cUserData.arFairyUpgradeLevel = new int[C_FairyManager.nFairyCount];
			}
			if (m_cUserData.arSkillCooldown == null)
			{
				m_cUserData.arSkillCooldown = new int[(int)E_Skill.max];
			}

			if (m_cUserData.arIsPurchaseFairy.Length < C_FairyManager.nFairyCount)
			{
				bool[] arNewData = new bool[C_FairyManager.nFairyCount];
				m_cUserData.arIsPurchaseFairy.CopyTo(arNewData, 0);

				for (int i = 0; i < C_FairyManager.nFairyCount - m_cUserData.arIsPurchaseFairy.Length; i++)
				{
					arNewData[C_FairyManager.nFairyCount - 1 - i] = false;
				}

				m_cUserData.arIsPurchaseFairy = arNewData;
			}
			if (m_cUserData.arFairyUpgradeLevel.Length < C_FairyManager.nFairyCount)
			{
				int[] arNewData = new int[C_FairyManager.nFairyCount];
				m_cUserData.arFairyUpgradeLevel.CopyTo(arNewData, 0);

				for (int i = 0; i < C_FairyManager.nFairyCount - m_cUserData.arFairyUpgradeLevel.Length; i++)
				{
					arNewData[C_FairyManager.nFairyCount - 1 - i] = 0;
				}

				m_cUserData.arFairyUpgradeLevel = arNewData;
			}
			if (m_cUserData.arSkillCooldown.Length < (int)E_Skill.max)
			{
				int[] arNewData = new int[(int)E_Skill.max];
				m_cUserData.arSkillCooldown.CopyTo(arNewData, 0);

				for (int i = 0; i < (int)E_Skill.max - m_cUserData.arSkillCooldown.Length; i++)
				{
					arNewData[C_FairyManager.nFairyCount - 1 - i] = 0;
				}

				m_cUserData.arSkillCooldown = arNewData;
			}

			Save();
		}

	}

	public C_UserData GetUserData()
	{
		return m_cUserData;
	}

	public void Save()
	{

		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream stream = new MemoryStream();

		bf.Serialize(stream, m_cUserData);
		PlayerPrefs.SetString("UserData", Convert.ToBase64String(stream.GetBuffer()));
		stream.Close();
	}

	public void Make_InitUserData_And_Save()
	{
		m_cUserData = new C_UserData();
		InitUserData();
		Save();
	}

	public void InitUserData()
	{
		m_cUserData.nTextCount = 0;
		m_cUserData.arPlayerUpgradeLevel = new int[(int)E_PlayerUpgrade.max];
		m_cUserData.arStarLightUpgradeLevel = new int[(int)E_StarLightType.max];
		m_cUserData.arIsPurchaseFairy = new bool[C_FairyManager.nFairyCount];
		m_cUserData.arFairyUpgradeLevel = new int[C_FairyManager.nFairyCount];
		m_cUserData.arSkillCooldown = new int[(int)E_Skill.max];

		m_cUserData.bIsTutorialCompleted = false;

		m_cUserData.dCurrentStarLight = 0.0d;
		m_cUserData.nCurrentFeverTapCount = 0;
		m_cUserData.eCharacterMode = E_CharacterMode.DefaultMode;

		for(int i = 0; i < (int)E_PlayerUpgrade.max; i++)
		{
			m_cUserData.arPlayerUpgradeLevel[i] = 0;
		}

		m_cUserData.arStarLightUpgradeLevel[(int)E_StarLightType.starLightDefault] = 0;
		m_cUserData.arStarLightUpgradeLevel[(int)E_StarLightType.starLightBlue] = 0;
		m_cUserData.arStarLightUpgradeLevel[(int)E_StarLightType.starLightRed] = 0;

		for(int i = 0; i < C_FairyManager.nFairyCount; i++)
		{
			m_cUserData.arIsPurchaseFairy[i] = false;
			m_cUserData.arFairyUpgradeLevel[i] = 0;
		}

		for(int i = 0; i < (int)E_Skill.max; i++)
		{
			m_cUserData.arSkillCooldown[i] = 0;
		}

		m_cUserData.bIsPurchaseNo_Ads = false;

		if (Application.systemLanguage == SystemLanguage.Korean)
		{
			m_cUserData.eLanguage = E_Language.korean;
		}
		else
		{
			m_cUserData.eLanguage = E_Language.english;
		}
		m_cUserData.fSound_BGM_Volume = 0.5f;
		m_cUserData.fSound_Effect_Volume = 0.5f;
	}

	private void purchaseCheck()
	{
		//if(C_IAPManager.Instance.HadPurchased("RemoveBannerAd"))
		//{
		//	m_cUserData.bIsPurchaseNo_Ads = true;
		//	Save();
		//}
	}

	public IEnumerator WaitIAPInitToCheck()
	{
		float fWaiTime = 0.0f;
		float fUpdateTime = 0.1f;
		WaitForSeconds Update = new WaitForSeconds(fUpdateTime);
        //while(fWaiTime <= 2.5f && !C_IAPManager.Instance.isInit)
        //{
        //	yield return Update;
        //	fWaiTime += fUpdateTime;
        //}
        yield return Update;
        //if(C_IAPManager.Instance.isInit)
        //{
        //	purchaseCheck();
        //	Debug.Log("IAP 초기화, 결재상품 체크 성공");

        //}
        //else
        //{
        //	Debug.Log("IAP 초기화 실패");
        //}
    }

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

}
