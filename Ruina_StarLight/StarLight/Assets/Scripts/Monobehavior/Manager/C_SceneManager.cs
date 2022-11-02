using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_SceneManager : MonoBehaviour {

	public static C_SceneManager Instance;

	private E_Scene m_eScene;

	public void ChangeScene(E_Scene eScene)
	{
		m_eScene = eScene;
		SceneManager.LoadScene((int)eScene);
		switch (eScene)
		{
			case E_Scene.start:
				{
					C_SoundManager.Instance.ChangeBGM(E_BGM.start);
					C_StarLightManager.Instance.AllStarLightSetActiveFalse();
					//C_AdmobManager.Instance.HideBanner();
				}
				break;
			case E_Scene.main:
				{
					if (C_UserDataController.Instance.GetUserData().arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] >=
						C_BaseDataController.Instance.m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].nLevelMax)
					{
						C_SoundManager.Instance.ChangeBGM(E_BGM.game_ed);
					}
					else
					{
						C_SoundManager.Instance.ChangeBGM(E_BGM.game);
					}
				}
				break;
			default:
				break;
		}

	}

	public E_Scene GetScene()
	{
		return m_eScene;
	}

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			m_eScene = E_Scene.start;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}


	/////////테스트용 나중에 밑에 다 삭제할 것/////////


	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			ChangeScene(E_Scene.start);
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			ChangeScene(E_Scene.main);
		}
	}

}
