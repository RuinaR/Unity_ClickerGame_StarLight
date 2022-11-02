using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class C_DialogueController : MonoBehaviour {

	public static C_DialogueController Instance { get; private set; }
	[SerializeField]
	private C_PlayerController m_cPlayer;
	[SerializeField]
	private GameObject m_DialogueBox;
	[SerializeField]
	private GameObject m_dialogueEndIMG;
	[SerializeField]
	private Text m_text;
	[SerializeField]
	private BoxCollider m_tapCollider;
    [SerializeField]
    private Image m_CharacterImageOrigin;

	[SerializeField]
	private GameObject m_ScreenObj;
	[SerializeField]
	private C_SpeechBubbleController m_Bubble;
	[SerializeField]
	private GameObject m_cEnding;
	private Animator m_EndingAnimator;
    private C_UserData m_cUserData;
	private string[] m_arDialogue;

	private bool m_bIsCoroutineActive;
	private bool m_bIsBubbleActive;
	private bool isAboutEnd;

	private Coroutine m_coroutineDialogue;

	private IEnumerator CoroutineDialogue(string strDialogue, float fWaitTime)
	{
		int nActionValue = 3;
		m_bIsCoroutineActive = true;

		E_CharacterFaceType eFaceType;
		E_CharacterTalkType eTalkType;

		E_SpeechBubble eBubble;
		int nIsViewScreen;
		
		m_arDialogue = strDialogue.Split(C_BaseDataController.cParagraphDivision);

		bool bIsSkip;
		float fElapsedTime;

        m_DialogueBox.SetActive(true);
        
        for (int i = 0; i < m_arDialogue.Length; i++)
		{
			bIsSkip = false;
			isAboutEnd = false;
			fElapsedTime = 0.0f;

			m_text.text = "";
			m_dialogueEndIMG.SetActive(false);
			

			eFaceType = (E_CharacterFaceType)(m_arDialogue[i][0] - '0');
			eBubble = (E_SpeechBubble)(m_arDialogue[i][1] - '0');
			nIsViewScreen = (int)(m_arDialogue[i][2] - '0');
			if(m_arDialogue[i][3] == '#' ||
				m_arDialogue[i][3] == '$')
			{
				isAboutEnd = true;
			}
			//Debug.Log(string.Format("eFaceType : {0}, eBubble : {1}, nScreen : {2}", (int)eFaceType, (int)eBubble, nIsViewScreen));

			eTalkType = E_CharacterTalkType.talk_no;
			m_cPlayer.SetCharacterFaceType(eFaceType);
			m_cPlayer.SetCharacterTalkType(eTalkType);
			m_cPlayer.UpdateCharacter();

            m_CharacterImageOrigin.sprite =
            C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_no);

			m_Bubble.StartBubbleCoroutine(eBubble);
			m_bIsBubbleActive = true;

			if (nIsViewScreen == 1 && !m_ScreenObj.activeInHierarchy)
			{
				m_ScreenObj.SetActive(true);
			}
			if(nIsViewScreen == 0)
			{
				m_ScreenObj.SetActive(false);
			}

            for (int j = nActionValue; j < m_arDialogue[i].Length; j++)
			{
				if(m_arDialogue[i][j] == '#')
				{
					m_cEnding.SetActive(true);
					C_MainUIController.Instance.MainUIActiveFalse();
					C_SoundManager.Instance.ChangeBGM(E_BGM.game_ed);
				}
				if (m_arDialogue[i][j] == '$')
				{
					m_EndingAnimator.SetBool("End", true);
				}
				while (fElapsedTime < fWaitTime && !bIsSkip)
				{
					if (j > nActionValue)
					{
						if (C_TapController.Instance.Tap(m_tapCollider))
						{
							bIsSkip = true;
						}
					}
					yield return null;
					fElapsedTime += Time.deltaTime;
				}
				if (bIsSkip)
				{
					if (isAboutEnd)
					{
						m_text.text = m_arDialogue[i].Substring(nActionValue + 1);
					}
					else
					{
						m_text.text = m_arDialogue[i].Substring(nActionValue);
					}

					break;
				}
				else
				{
					if (m_arDialogue[i][j] != ' ' && m_arDialogue[i][j] != '\n' &&
						m_arDialogue[i][j] != '\t' && m_arDialogue[i][j] != '\b' &&
						m_arDialogue[i][j] != '?' && m_arDialogue[i][j] != '!' &&
						m_arDialogue[i][j] != '.' && m_arDialogue[i][j] != ',' &&
						m_arDialogue[i][j] != '~' && m_arDialogue[i][j] != '-' &&
						m_arDialogue[i][j] != '|')
					{
						if (j % 2 == 0)
						{
							if (eTalkType == E_CharacterTalkType.talk_no)
							{
								eTalkType = E_CharacterTalkType.talk_yes;
								m_CharacterImageOrigin.sprite =
								C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_yes);
							}
							else
							{
								eTalkType = E_CharacterTalkType.talk_no;
								m_CharacterImageOrigin.sprite =
								C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_no);
							}
							m_cPlayer.SetCharacterTalkType(eTalkType);
							m_cPlayer.UpdateCharacter();
						}
						C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.character_Voice);
					}
					else
					{
						eTalkType = E_CharacterTalkType.talk_no;
						m_cPlayer.SetCharacterTalkType(eTalkType);
						m_cPlayer.UpdateCharacter();

						m_CharacterImageOrigin.sprite =
						C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_no);
					}
					if (m_arDialogue[i][j] != '#' &&
						m_arDialogue[i][j] != '$')
					{
						m_text.text += m_arDialogue[i][j];
					}
					fElapsedTime = 0.0f;
				}

			}
			m_cPlayer.SetCharacterTalkType(E_CharacterTalkType.talk_no);
			m_cPlayer.UpdateCharacter();

			m_CharacterImageOrigin.sprite =
			C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_no);

			m_dialogueEndIMG.SetActive(true);
			while (true)
			{
				if (C_TapController.Instance.Tap(m_tapCollider))
				{
					m_Bubble.StopBubbleCoroutine();
					m_bIsBubbleActive = false;
					//m_ScreenObj.SetActive(false);
					break;
				}
				yield return null;
			}
		}
		m_ScreenObj.SetActive(false);
		eFaceType = E_CharacterFaceType.faceDefault;
		eTalkType = E_CharacterTalkType.talk_no;
		m_cPlayer.SetCharacterFaceType(eFaceType);
		m_cPlayer.SetCharacterTalkType(eTalkType);
		m_cPlayer.UpdateCharacter();

		m_CharacterImageOrigin.sprite =
		C_SpriteManager.Instance.GetSpriteCharacter_Origin(m_cUserData.eCharacterMode, eFaceType, E_CharacterTalkType.talk_no);

		Disable();
		m_bIsCoroutineActive = false;
	}

	public void Init()
	{
        m_cUserData = C_UserDataController.Instance.GetUserData();
        m_bIsCoroutineActive = false;
		m_text.text = "";
		m_DialogueBox.SetActive(false);
		m_dialogueEndIMG.SetActive(false);
		m_EndingAnimator = m_cEnding.GetComponent<Animator>();
	}

	public void Active(string strDialogue, float fWaitTime)
	{
		if (!m_DialogueBox.activeInHierarchy)
		{
			m_text.text = "";
			if (m_bIsCoroutineActive)
			{
				StopCoroutine(m_coroutineDialogue);
			}
			if(m_bIsBubbleActive)
			{
				m_Bubble.StopBubbleCoroutine();
			}
			m_ScreenObj.SetActive(false);
			m_coroutineDialogue = StartCoroutine(CoroutineDialogue(strDialogue, fWaitTime));
		}
	}

	public void Active_Force(string strDialogue, float fWaitTime)
	{
		m_text.text = "";
		if (m_bIsCoroutineActive)
		{
			StopCoroutine(m_coroutineDialogue);
		}
		if (m_bIsBubbleActive)
		{
			m_Bubble.StopBubbleCoroutine();
		}
		m_ScreenObj.SetActive(false);
		m_coroutineDialogue = StartCoroutine(CoroutineDialogue(strDialogue, fWaitTime));
	}

	public bool IsTalking()
	{
		return m_bIsCoroutineActive;
	}

	private void Disable()
	{
		m_text.text = "";
		m_DialogueBox.SetActive(false);
		m_dialogueEndIMG.SetActive(false);
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
