using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FairyManager : MonoBehaviour {

	public static C_FairyManager Instance { get; private set; }
	public static int nFairyCount;

	private C_UserData m_cUserData;
	
	private C_FairyController[] arFairy;

	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();
		arFairy = GetComponentsInChildren<C_FairyController>(true);
		for (int i = 0; i < nFairyCount; i++)
		{
			arFairy[i].Init(i);
			if(m_cUserData.arIsPurchaseFairy[i])
			{
				ActiveFairy(i);
			}
		}
	}

	public void ActiveFairy(int nFairyNum)
	{
		arFairy[nFairyNum].gameObject.SetActive(true);
		arFairy[nFairyNum].StartCoroutine();
	}

	public void DisableFairy(int nFairyNum)
	{
		arFairy[nFairyNum].StopCoroutine();
		arFairy[nFairyNum].gameObject.transform.position = Vector3.zero;
		arFairy[nFairyNum].gameObject.SetActive(false);
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
