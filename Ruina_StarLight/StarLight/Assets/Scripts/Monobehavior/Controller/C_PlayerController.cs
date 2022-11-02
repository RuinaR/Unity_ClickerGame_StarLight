using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_PlayerController : MonoBehaviour {

	[SerializeField]
	private C_VehicleController m_cVehicle;
	[SerializeField]
	private C_CharacterController m_cCharacter;
	[SerializeField]
	private Transform m_tfPlayerRender;
	[SerializeField]
	private Collider m_collider;
	[SerializeField]
	private GameObject m_DialogueBox;

	[SerializeField]
	private Transform m_tfCat;



	private Vector2 m_vec2DefaultPos;
	private Vector3 m_vec3DefaultPos;


	private Rigidbody2D m_rb;
	private Animator m_animator;
	private Coroutine m_CoroutineMove;
	private Coroutine m_CoroutineMovePrivate;
	private C_UserData m_cUserData;

	private int nPlayerTapCount;

	private IEnumerator CoroutineMove_Normal()
	{
		WaitForSeconds wait = new WaitForSeconds(5.0f);
		float fSpeed = 0.15f;
		Vector2 vec2UpVelocity = Vector2.up * fSpeed;
		Vector2 vec2DownVelocity = Vector2.down * fSpeed;

		while (true)
		{
			m_rb.MovePosition(m_vec2DefaultPos);
			m_rb.velocity = vec2UpVelocity;
			yield return wait;
			m_rb.velocity = vec2DownVelocity;
			yield return wait;		
		}
	}

	private IEnumerator CoroutineMove_ToCat()
	{
		float fRatio = 1.0f;
		
		Vector3 vec3Set = new Vector3(0.0f, 0.0f, 0.0f);
		while(true)
		{
			vec3Set = Vector3.Lerp(transform.position, m_tfCat.position, fRatio * Time.deltaTime);
			transform.position = vec3Set;
			yield return null;
		}
	}

	private IEnumerator CoroutineMove()
	{
		m_CoroutineMovePrivate = StartCoroutine(CoroutineMove_Normal());

		float fUpdate = 0.1f;
		float fTotalTime = 0.0f;
		WaitForSeconds update = new WaitForSeconds(fUpdate);

		Quaternion quaternion = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
		while(true)
		{
			if (m_cUserData.bIsPurchaseNo_Ads)
			{
				Debug.Log("Move_Normal");
				float fWait = 0.0f;
				float fWaitUnitTime = Random.Range(15.0f, 20.0f);
				while (fWait <= fWaitUnitTime)
				{
					yield return update;
					fWait += fUpdate;
				}
				StopCoroutine(m_CoroutineMovePrivate);
				m_CoroutineMovePrivate = StartCoroutine(CoroutineMove_ToCat());

				Debug.Log("Move_ToCat");
				fWait = 0.0f;
				fWaitUnitTime = Random.Range(10.0f, 15.0f);
				while (fWait <= fWaitUnitTime)
				{
					if(m_tfCat.transform.position.x < transform.position.x)
					{
						quaternion.y = 180.0f;
						m_tfPlayerRender.rotation = quaternion;
					}
					else
					{
						quaternion.y = 0.0f;
						m_tfPlayerRender.rotation = quaternion;
					}
					yield return update;
					fWait += fUpdate;
				}
				StopCoroutine(m_CoroutineMovePrivate);

				Debug.Log("Move_ToOrigin");
				fWait = 0.0f;
				fWaitUnitTime = 3.0f;
				if (m_vec3DefaultPos.x < transform.position.x)
				{
					quaternion.y = 180.0f;
					m_tfPlayerRender.rotation = quaternion;
				}
				else
				{
					quaternion.y = 0.0f;
					m_tfPlayerRender.rotation = quaternion;
				}
				Vector3 vec3Start = transform.position;
				while (fWait <= fWaitUnitTime)
				{
					transform.position = Vector3.Lerp(vec3Start, m_vec3DefaultPos, fWait / fWaitUnitTime);
					yield return null;
					fWait += Time.deltaTime;
				}
				m_CoroutineMovePrivate = StartCoroutine(CoroutineMove_Normal());

				yield return update;
				fTotalTime += fUpdate;
			}
			yield return update;
		}
	}

	public void Init(E_CharacterFaceType eCharacterFaceType)
	{
		nPlayerTapCount = 0;
		m_cUserData = C_UserDataController.Instance.GetUserData();
		m_vec2DefaultPos = transform.position;
		m_vec3DefaultPos = transform.position;
		m_rb = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
		m_cCharacter.Init(eCharacterFaceType);
		m_cVehicle.Init();
	}

	public Animator GetAnimator()
	{
		return m_animator;
	}

	public void SetCharacterFaceType(E_CharacterFaceType eCharacterFaceType)
	{
		m_cCharacter.SetCharacterFaceType(eCharacterFaceType);
	}
	public void SetCharacterTalkType(E_CharacterTalkType eTalkType)
	{
		m_cCharacter.SetCharacterTalkType(eTalkType);
	}

	public void UpdateAll()
	{
		m_cCharacter.UpdateCharacter();
		m_cVehicle.UpdateVehicle();
	}

	public void UpdateCharacter()
	{
		m_cCharacter.UpdateCharacter();
	}

	public void UpdateVehicle()
	{
		m_cVehicle.UpdateVehicle();
	}

	public void StartCoroutineAction()
	{
		m_CoroutineMove = StartCoroutine(CoroutineMove());
		m_cCharacter.StartCoroutineAction();
	}

	//이중 참조 주의
	//public void StopCoroutineAction()
	//{
	//	StopCoroutine(m_CoroutineMove);
	//	m_cCharacter.StopCoroutineAction();
	//}

	private void CharacterFaceTypeSettingUpdateForAnim(E_CharacterFaceType eCharacterFaceType)
	{
		if (!C_DialogueController.Instance.IsTalking())
		{
			SetCharacterFaceType(eCharacterFaceType);
			SetCharacterTalkType(E_CharacterTalkType.talk_no);
			UpdateCharacter();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (C_TapController.Instance.Tap(m_collider))
		{
			GetAnimator().SetTrigger(C_AnimationHashList.nTap);
			C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.character_Tap);

			if (!m_DialogueBox.activeInHierarchy)
			{
				nPlayerTapCount++;

				if (nPlayerTapCount >= 3)
				{
					if (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] >=
						C_BaseDataController.Instance.m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].nLevelMax)
					{
						int nRandom = Random.Range(0, C_BaseDataController.arCharacterText_Random_Ed[(int)m_cUserData.eLanguage].Length);
						C_DialogueController.Instance.Active(C_BaseDataController.arCharacterText_Random_Ed[(int)m_cUserData.eLanguage][nRandom],
						C_BaseDataController.fWaitTimeDialogue_Talk);
					}
					else
					{
						int nRandom = Random.Range(0, C_BaseDataController.arCharacterText_Random[(int)m_cUserData.eLanguage].Length);
						C_DialogueController.Instance.Active(C_BaseDataController.arCharacterText_Random[(int)m_cUserData.eLanguage][nRandom],
						C_BaseDataController.fWaitTimeDialogue_Talk);
					}
					nPlayerTapCount = 0;
				}
			}


		}
	}
}
