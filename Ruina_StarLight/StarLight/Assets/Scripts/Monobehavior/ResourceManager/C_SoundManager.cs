using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SoundManager : MonoBehaviour
{
	private delegate void delActiveSoundEffect_StarLightTap();

	public static C_SoundManager Instance { get; private set; }

	[SerializeField]
	private AudioSource m_BGM;
	[SerializeField]
	private AudioSource m_Effect;

	private C_UserData m_cUserData;

	private AudioClip[] m_arBGM;

	private AudioClip[] m_arEffect;

	private delActiveSoundEffect_StarLightTap[] m_arDelActiveSoundEffect_StarLightTap;

	public void Load()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();

		m_arBGM = new AudioClip[(int)E_BGM.max];
		m_arBGM[(int)E_BGM.start] = Resources.LoadAll<AudioClip>(C_ResourcesPath.SoundBGM_Start)[0];
		m_arBGM[(int)E_BGM.game] = Resources.LoadAll<AudioClip>(C_ResourcesPath.SoundBGM_Game)[0];
		m_arBGM[(int)E_BGM.fever] = Resources.LoadAll<AudioClip>(C_ResourcesPath.SoundBGM_Fever)[0];
		m_arBGM[(int)E_BGM.game_ed] = Resources.LoadAll<AudioClip>(C_ResourcesPath.SoundBGM_Game_Ed)[0];


		m_arEffect = Resources.LoadAll<AudioClip>(C_ResourcesPath.SoundEffect);


		m_arDelActiveSoundEffect_StarLightTap = new delActiveSoundEffect_StarLightTap[3];
		m_arDelActiveSoundEffect_StarLightTap[(int)E_StarLightType.starLightDefault] = ActiveSoundEffect_StarLight_Default;
		m_arDelActiveSoundEffect_StarLightTap[(int)E_StarLightType.starLightBlue] = ActiveSoundEffect_StarLight_Blue;
		m_arDelActiveSoundEffect_StarLightTap[(int)E_StarLightType.starLightRed] = ActiveSoundEffect_StarLight_Red;

		m_BGM.volume = m_cUserData.fSound_BGM_Volume;
		m_BGM.clip = m_arBGM[(int)E_BGM.start];
		m_BGM.Play();
	}

	public void ChangeBGM(E_BGM eBGM)
	{
		m_BGM.Stop();
		m_BGM.clip = m_arBGM[(int)eBGM];
		m_BGM.Play();
	}
	public void ChangeBGMVolume(float fVolume)
	{
		m_BGM.volume = fVolume;
	}

	public void ActiveSoundEffect(E_Sound_Effect eSoundEffect)
	{
		m_Effect.PlayOneShot(m_arEffect[(int)eSoundEffect], m_cUserData.fSound_Effect_Volume);
	}

	public void ActiveSoundEffect_StarLightTap(E_StarLightType eStarLightType)
	{
		if(eStarLightType == E_StarLightType.starLightFairy)
		{
			return;
		}
		m_arDelActiveSoundEffect_StarLightTap[(int)eStarLightType]();
	}


	private void ActiveSoundEffect_StarLight_Default()
	{
		m_Effect.PlayOneShot(m_arEffect[(int)E_Sound_Effect.starLight_Default], m_cUserData.fSound_Effect_Volume);
	}
	private void ActiveSoundEffect_StarLight_Blue()
	{
		m_Effect.PlayOneShot(m_arEffect[(int)E_Sound_Effect.starLight_Blue], m_cUserData.fSound_Effect_Volume);
	}
	private void ActiveSoundEffect_StarLight_Red()
	{
		m_Effect.PlayOneShot(m_arEffect[(int)E_Sound_Effect.starLight_Red], m_cUserData.fSound_Effect_Volume);
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
}
