using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_UpgradeManager : MonoBehaviour
{
	public static C_UpgradeManager Instance { get; private set; }

	private C_UserData m_cUserData;
	private C_UpgradeData[] m_arPlayerUpgradeData;
	private C_UpgradeData[] m_arStarLightUpgradeData;
	private C_FairyUpgradeData[] m_arFairyUpgradeData;

	[SerializeField]
	private C_PlayerController m_cPlayerController;
    [SerializeField]
    private C_StateUIController m_cStateUI;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_arPlayerUpgradeData = C_BaseDataController.Instance.GetPlayerUpgradeData();
		m_arStarLightUpgradeData = C_BaseDataController.Instance.GetStarLightUpgradeData();
		m_arFairyUpgradeData = C_BaseDataController.Instance.GetFairyUpgradeData();
	}

	public void Upgrade(E_PlayerUpgrade eUpgrade, Vector3 vec3EffectPos)
	{
		double dCost = GetUpgradeCost(eUpgrade);
		if ((m_cUserData.dCurrentStarLight >= dCost) &&
			(m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade] < m_arPlayerUpgradeData[(int)eUpgrade].nLevelMax))
		{
			m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade]++;
			m_cUserData.dCurrentStarLight -= dCost;

			GameObject UpgradeEffect = C_EffectManager.Instance.GetEffect(E_Effect.upgrade).gameObject;
			UpgradeEffect.transform.position = vec3EffectPos;
			UpgradeEffect.SetActive(true);

			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.upgrade);
		}
		C_MainUIController.Instance.UpdateUI_ChangeResource();
		C_MainUIController.Instance.UpdateElementText(eUpgrade);
        m_cStateUI.ActiveStarLightGainUI(E_StarLightType.starLightDefault, -dCost);

        if (eUpgrade == E_PlayerUpgrade.vehicle)
		{
            if (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] % C_BaseDataController.nVehicleChangeLV == 0)
            {
                m_cPlayerController.UpdateVehicle();

                if (m_cUserData.nTextCount < C_BaseDataController.arCharacterText[(int)m_cUserData.eLanguage].Length)
                { 
                    C_DialogueController.Instance.Active_Force(C_BaseDataController.arCharacterText[(int)m_cUserData.eLanguage][m_cUserData.nTextCount],
                            C_BaseDataController.fWaitTimeDialogue_Talk);        
                }
                m_cUserData.nTextCount++;
            }
		}
		C_UserDataController.Instance.Save();
	}

	public void Upgrade(E_StarLightType eType, Vector3 vec3EffectPos)
	{
		double dCost = GetUpgradeCost(eType);

		if ((m_cUserData.dCurrentStarLight >= dCost) &&
			(m_cUserData.arStarLightUpgradeLevel[(int)eType] < m_arStarLightUpgradeData[(int)eType].nLevelMax))
		{
			m_cUserData.arStarLightUpgradeLevel[(int)eType]++;
			m_cUserData.dCurrentStarLight -= dCost;

			GameObject UpgradeEffect = C_EffectManager.Instance.GetEffect(E_Effect.upgrade).gameObject;
			UpgradeEffect.transform.position = vec3EffectPos;
			UpgradeEffect.SetActive(true);

			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.upgrade);
		}
		C_MainUIController.Instance.UpdateUI_ChangeResource();
		C_MainUIController.Instance.UpdateElementText(eType);
        m_cStateUI.ActiveStarLightGainUI(E_StarLightType.starLightDefault, -dCost);
		C_UserDataController.Instance.Save();
	}

	public void Upgrade(int nFairyNum, Vector3 vec3EffectPos)
	{
		double dCost = GetUpgradeCost(nFairyNum);

		if ((m_cUserData.dCurrentStarLight >= dCost) &&
			(m_cUserData.arFairyUpgradeLevel[nFairyNum] < m_arFairyUpgradeData[nFairyNum].nLevelMax))
		{
			m_cUserData.arFairyUpgradeLevel[nFairyNum]++;
			m_cUserData.dCurrentStarLight -= dCost;

			GameObject UpgradeEffect = C_EffectManager.Instance.GetEffect(E_Effect.upgrade).gameObject;
			UpgradeEffect.transform.position = vec3EffectPos;
			UpgradeEffect.SetActive(true);

			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.upgrade);
		}

		C_MainUIController.Instance.UpdateUI_ChangeResource();
		C_MainUIController.Instance.UpdateElementText(nFairyNum);
        m_cStateUI.ActiveStarLightGainUI(E_StarLightType.starLightDefault, -dCost);
		C_UserDataController.Instance.Save();
	}

	public double GetUpgradeCost(E_PlayerUpgrade eUpgrade)
	{
		return (double)(m_arPlayerUpgradeData[(int)eUpgrade].fUpgradeCost_Default *
			   Mathf.Pow(m_arPlayerUpgradeData[(int)eUpgrade].fUpgradeCost_Coefficient, (float)m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade]));
	}

	public double GetUpgradeCost(E_StarLightType eType)
	{
		return (double)(m_arStarLightUpgradeData[(int)eType].fUpgradeCost_Default *
			   Mathf.Pow(m_arStarLightUpgradeData[(int)eType].fUpgradeCost_Coefficient, (float)m_cUserData.arStarLightUpgradeLevel[(int)eType]));
	}

	public double GetUpgradeCost(int nFairyNum)
	{
		return (double)(m_arFairyUpgradeData[nFairyNum].fUpgradeCost_Default *
			   Mathf.Pow(m_arFairyUpgradeData[nFairyNum].fUpgradeCost_Coefficient, (float)m_cUserData.arFairyUpgradeLevel[nFairyNum]));
	}

	public float GetApplyValue(E_PlayerUpgrade eUpgrade)
	{
		if (eUpgrade == E_PlayerUpgrade.vehicle)
		{
			return
			m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].fValue_Default *
			Mathf.Pow(m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].fValue_Upgrade,
			(float)(m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle]));
		}
		else
		{
			return (m_arPlayerUpgradeData[(int)eUpgrade].fValue_Default +
			((float)(m_cUserData.arPlayerUpgradeLevel[(int)eUpgrade]) *
			m_arPlayerUpgradeData[(int)eUpgrade].fValue_Upgrade));
		}
	}

	public float GetApplyValue(E_StarLightType eType)
	{
		return (m_arStarLightUpgradeData[(int)eType].fValue_Default +
		((float)(m_cUserData.arStarLightUpgradeLevel[(int)eType]) *
		m_arStarLightUpgradeData[(int)eType].fValue_Upgrade));
	}

	public float GetApplyValue(int nFiaryNum)
	{
		return (m_arFairyUpgradeData[nFiaryNum].fValue_Default *
				Mathf.Pow(m_arFairyUpgradeData[nFiaryNum].fValue_Upgrade, (float)(m_cUserData.arFairyUpgradeLevel[nFiaryNum])));
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
