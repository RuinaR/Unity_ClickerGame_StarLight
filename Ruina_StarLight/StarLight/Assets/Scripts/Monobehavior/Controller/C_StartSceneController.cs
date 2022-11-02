using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StartSceneController : MonoBehaviour {
    
	private static bool bIsLoaded = false;

	[SerializeField]
	private C_StartUIController m_cStartUIController;
	[SerializeField]
	private int m_nFairyCount;

	private void LoadData()
	{
		C_FairyManager.nFairyCount = m_nFairyCount; 
		C_BaseDataController.Instance.Load();
		C_UserDataController.Instance.Load();
	}
	private void LoadResource()
	{
		C_SpriteManager.Instance.Load();
		C_SoundManager.Instance.Load();
		C_EffectManager.Instance.Load();
	}

	// Use this for initialization
	void Start () {
		if (!bIsLoaded)
		{
			LoadData();
			LoadResource();

			C_AnimationHashList.Init();
			C_TapController.Instance.Init();

			StartCoroutine(C_UserDataController.Instance.WaitIAPInitToCheck());
			bIsLoaded = true;
			Debug.Log("로딩 완료");
		}

		m_cStartUIController.Init();
		C_PopUpController.Instance.Init();

        StartCoroutine(CoroutineWait_Start(2.5f));
	}

    private IEnumerator CoroutineWait_Start(float fWaitTime)
    {
        float fUpdateTime = 0.1f;
        WaitForSeconds wait = new WaitForSeconds(fUpdateTime);
        float fElapsedTime = 0.0f;
        while (fElapsedTime <= fWaitTime)
        {
            yield return wait;
            fElapsedTime += fUpdateTime;
        }
        
        Debug.Log("Game_Start_Active");
        m_cStartUIController.StartActive();
    }
	//////////////////////////////TEST//////////////////////////

	public void InitUserData_For_Btn()
	{
		C_UserDataController.Instance.InitUserData();
		C_UserDataController.Instance.Save();
	}

}
