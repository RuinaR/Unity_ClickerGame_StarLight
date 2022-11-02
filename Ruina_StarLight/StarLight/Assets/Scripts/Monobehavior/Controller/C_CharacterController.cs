using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_CharacterController : MonoBehaviour
{
	private C_UserData m_cUserData;

	private SpriteRenderer m_sr;

	[SerializeField]
	private GameObject m_Blink_Default_Confusion;
	[SerializeField]
	private GameObject m_Blink_Cry_Angry;
	[SerializeField]
	private GameObject m_Blink_Disappointment;
	[SerializeField]
	private GameObject m_Blink_Shameful;
	[SerializeField]
	private GameObject m_Blink_Smile_Joke;

	private E_CharacterFaceType m_eCharacterFaceType;
	private E_CharacterTalkType m_eCharacterTalkType;
	private GameObject m_Blink_Select;
	private GameObject[] m_arBlink;
	private Coroutine m_coroutineBlink;

	public void Init(E_CharacterFaceType eFaceType)
	{
		m_sr = GetComponent<SpriteRenderer>();
		m_cUserData = C_UserDataController.Instance.GetUserData();
		SetCharacterFaceType(eFaceType);
		SetCharacterTalkType(E_CharacterTalkType.talk_no);

		m_arBlink = new GameObject[(int)E_CharacterFaceType.max];
		m_arBlink[(int)E_CharacterFaceType.faceDefault] = m_Blink_Default_Confusion;
		m_arBlink[(int)E_CharacterFaceType.confusion] = m_Blink_Default_Confusion;
		m_arBlink[(int)E_CharacterFaceType.cry] = m_Blink_Cry_Angry;
		m_arBlink[(int)E_CharacterFaceType.angry] = m_Blink_Cry_Angry;
		m_arBlink[(int)E_CharacterFaceType.disappointment] = m_Blink_Disappointment;
		m_arBlink[(int)E_CharacterFaceType.shameful] = m_Blink_Shameful;
		m_arBlink[(int)E_CharacterFaceType.smile] = m_Blink_Smile_Joke;
		m_arBlink[(int)E_CharacterFaceType.joke] = m_Blink_Smile_Joke;

		m_Blink_Select = m_arBlink[(int)eFaceType];
	}

	public void StartCoroutineAction()
	{
		m_coroutineBlink = StartCoroutine(CoroutineBlink());
	}

	public void StopCoroutineAction()
	{
		StopCoroutine(m_coroutineBlink);
	}


	private IEnumerator CoroutineBlink()
	{
		float fUpdateTime = 0.1f;
		WaitForSeconds UpdateTime = new WaitForSeconds(fUpdateTime);
		WaitForSeconds blinkTime = new WaitForSeconds(0.2f);
		while (true)
		{

			float fWaitTime = Random.Range(0.5f, 2.0f);
			float fElapsedTime = 0.0f;
			while (fElapsedTime <= fWaitTime)
			{
				yield return UpdateTime;
				fElapsedTime += fUpdateTime;
			}

			if (m_eCharacterFaceType != E_CharacterFaceType.confusion)
			{
				m_Blink_Select.SetActive(true);

				yield return blinkTime;

				m_Blink_Select.SetActive(false);
			}
		}
	}

	public void SetCharacterFaceType(E_CharacterFaceType eFaceType)
	{
		m_eCharacterFaceType = eFaceType;
	}

	public void SetCharacterTalkType(E_CharacterTalkType eTalkType)
	{
		m_eCharacterTalkType = eTalkType;
	}

	public void UpdateCharacter()
	{
		m_sr.sprite = C_SpriteManager.Instance.GetSpriteCharacter(m_cUserData.eCharacterMode, m_eCharacterFaceType, m_eCharacterTalkType);

		if (m_Blink_Select != m_arBlink[(int)m_eCharacterFaceType])
		{
			m_Blink_Select.SetActive(false);
			m_Blink_Select = m_arBlink[(int)m_eCharacterFaceType];
		}
	}
}
