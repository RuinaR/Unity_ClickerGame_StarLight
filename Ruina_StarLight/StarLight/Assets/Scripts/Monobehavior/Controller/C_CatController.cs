using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CatController : MonoBehaviour {

	[SerializeField]
	private GameObject m_cat;

	[SerializeField]
	private Animator m_animator;

	private Rigidbody2D m_rb;
	private RectTransform m_rtf;
	[SerializeField]
	private SpriteRenderer m_sr;

	private C_UserData m_cUserData;

	public void Init()
	{
		m_rb = GetComponent<Rigidbody2D>();
		m_rtf = GetComponent<RectTransform>();
		m_cUserData = C_UserDataController.Instance.GetUserData();
		if (m_cUserData.bIsPurchaseNo_Ads)
		{
			ActiveCat();
		}
	}

	public void ActiveCat()
	{
		m_cat.SetActive(true);
		StartCoroutine(CoroutineCat());
	}

	private IEnumerator CoroutineCat()
	{
		float fElapsedTime = 0.0f;
		float fWaitTime = 0.0f;
		float fUpdateTime = 0.1f;
		WaitForSeconds UpdateTime = new WaitForSeconds(fUpdateTime);

		Vector2 vecMove = new Vector2(1.0f, 0.0f);
		int nMotionNum = 0;

		bool bChangeDirFlag = false;
		while (true)
		{
			//기본
			fElapsedTime = 0.0f;
			fWaitTime = Random.Range(1.0f, 3.0f);
			while (fElapsedTime <= fWaitTime)
			{
				yield return UpdateTime;
				fElapsedTime += fUpdateTime;
			}
			//이동
			float fMoveTime = Random.Range(1.5f, 4.0f);
			fElapsedTime = 0.0f;
			if(Random.Range(0.0f, 1.0f) >= 0.5f)
			{
				m_rb.velocity = vecMove;
				m_sr.flipX = true;
			}
			else
			{
				m_rb.velocity = -vecMove;
				m_sr.flipX = false;
			}
			m_animator.SetBool(C_AnimationHashList.nCat_Walk, true);
			bChangeDirFlag = false;
			while (fElapsedTime < fMoveTime)
			{
				
				if (!bChangeDirFlag)
				{
					if (m_rtf.anchoredPosition.x < C_ScreenArea.fCat_X_Left ||
						m_rtf.anchoredPosition.x > C_ScreenArea.fCat_X_Right)
					{
						Debug.Log(m_rtf.anchoredPosition.x);
						m_rb.velocity = -m_rb.velocity;
						if (m_sr.flipX)
						{
							m_sr.flipX = false;
						}
						else
						{
							m_sr.flipX = true;
						}
						bChangeDirFlag = true;
					}
				}
				yield return null;
				fElapsedTime += Time.deltaTime;
			}
			m_animator.SetBool(C_AnimationHashList.nCat_Walk, false);
			m_rb.velocity = Vector2.zero;
			//기본
			fElapsedTime = 0.0f;
			fWaitTime = Random.Range(3.0f, 6.0f);
			while (fElapsedTime <= fWaitTime)
			{
				yield return UpdateTime;
				fElapsedTime += fUpdateTime;
			}
			//모션	
			nMotionNum = Random.Range(0, (int)E_CatMotion.max);
			m_animator.SetBool(C_AnimationHashList.arCat_Motion[nMotionNum], true);
			fElapsedTime = 0.0f;
			fWaitTime = Random.Range(7.5f, 12.0f);
			while (fElapsedTime <= fWaitTime)
			{
				yield return UpdateTime;
				fElapsedTime += fUpdateTime;
			}
			m_animator.SetBool(C_AnimationHashList.arCat_Motion[nMotionNum], false);
		}
	}
}
