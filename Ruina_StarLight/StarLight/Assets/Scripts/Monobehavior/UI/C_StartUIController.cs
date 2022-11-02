using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_StartUIController : MonoBehaviour {

	[SerializeField]
	private Collider m_colStart;
    [SerializeField]
    private Text m_textStart;
    [SerializeField]
    private Text m_textLoading;

    private C_UserData m_cUserData;
    private RaycastHit m_hitInfo;
    private Coroutine m_CoroutineLoading;
    private string[] m_arLoadingText;
	
	public void Init()
	{
        m_arLoadingText = new string[4];
        m_arLoadingText[0] = "Loading";
        m_arLoadingText[1] = "Loading.";
        m_arLoadingText[2] = "Loading..";
        m_arLoadingText[3] = "Loading...";

        m_colStart.gameObject.SetActive(false);
        m_textStart.gameObject.SetActive(false);
        m_textLoading.text = m_arLoadingText[0];
        m_textLoading.gameObject.SetActive(true);
        m_cUserData = C_UserDataController.Instance.GetUserData();
        m_textStart.text = C_BaseDataController.arCSTR[(int)m_cUserData.eLanguage].start;

        m_CoroutineLoading = StartCoroutine(CoroutineLoadingText());
    }

	public void StartActive()
	{
        StopCoroutine(m_CoroutineLoading);
        m_textLoading.gameObject.SetActive(false);
        m_colStart.gameObject.SetActive(true);
        m_textStart.gameObject.SetActive(true);
    }

    private IEnumerator CoroutineLoadingText()
    {
        float fUpdateTime = 0.3f;
        WaitForSeconds fWait = new WaitForSeconds(fUpdateTime);
        int nCount = 0;
        while(true)
        {
            yield return fWait;
            m_textLoading.text = m_arLoadingText[nCount % m_arLoadingText.Length];
            nCount++;
        }
    }

    private void Update()
    {
        if (C_TapController.Instance.Tap(m_colStart))
        {
            C_SceneManager.Instance.ChangeScene(E_Scene.main);
            C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.gameStart);
        }
    }

}
