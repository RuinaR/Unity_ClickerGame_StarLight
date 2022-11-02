using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SpriteManager : MonoBehaviour
{
	public static C_SpriteManager Instance { get; private set; }

	private Sprite[][][] m_arSpriteCharacter;
    private Sprite[][][] m_arSpriteCharacter_Origin;
	
	private Sprite[][] m_arSpriteIcon_Fairy;

	private Sprite[] m_arSpriteVehicleBackward;
	private Sprite[] m_arSpriteVehicleForward;
	private Sprite[] m_arSpriteStarLight;
	private Sprite[] m_arSpriteBackgroundNormal;
	private Sprite[] m_arBonusObj;

	public void Load()
	{
		m_arSpriteCharacter = new Sprite[(int)E_CharacterMode.max][][];
		for(int i = 0; i < m_arSpriteCharacter.Length; i++)
		{
			m_arSpriteCharacter[i] = new Sprite[(int)E_CharacterFaceType.max][];
		}

		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.faceDefault] = 
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Default);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.angry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Angry);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.confusion] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Confusion);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.cry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Cry);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.disappointment] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Disappointment);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.joke] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Joke);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.shameful] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Shameful);
		m_arSpriteCharacter[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.smile] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Default_Smile);

		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.faceDefault] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Default);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.angry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Angry);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.confusion] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Confusion);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.cry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Cry);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.disappointment] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Disappointment);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.joke] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Joke);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.shameful] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Shameful);
		m_arSpriteCharacter[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.smile] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Blue_Smile);

		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.faceDefault] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Default);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.angry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Angry);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.confusion] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Confusion);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.cry] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Cry);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.disappointment] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Disappointment);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.joke] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Joke);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.shameful] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Shameful);
		m_arSpriteCharacter[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.smile] =
		Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Red_Smile);

        m_arSpriteCharacter_Origin = new Sprite[(int)E_CharacterMode.max][][];
        for (int i = 0; i < m_arSpriteCharacter_Origin.Length; i++)
        {
            m_arSpriteCharacter_Origin[i] = new Sprite[(int)E_CharacterFaceType.max][];
        }

        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.faceDefault] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Default);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.angry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Angry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.confusion] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Confusion);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.cry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Cry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.disappointment] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Disappointment);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.joke] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Joke);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.shameful] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Shameful);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.DefaultMode][(int)E_CharacterFaceType.smile] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Default_Smile);

        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.faceDefault] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Default);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.angry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Angry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.confusion] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Confusion);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.cry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Cry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.disappointment] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Disappointment);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.joke] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Joke);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.shameful] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Shameful);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.blueMode][(int)E_CharacterFaceType.smile] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Blue_Smile);

        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.faceDefault] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Default);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.angry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Angry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.confusion] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Confusion);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.cry] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Cry);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.disappointment] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Disappointment);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.joke] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Joke);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.shameful] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Shameful);
        m_arSpriteCharacter_Origin[(int)E_CharacterMode.redMode][(int)E_CharacterFaceType.smile] =
        Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteCharacter_Origin_Red_Smile);
		
		m_arSpriteIcon_Fairy = new Sprite[C_FairyManager.nFairyCount][];
		for(int i = 0; i < C_FairyManager.nFairyCount; i++)
		{
			m_arSpriteIcon_Fairy[i] = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteIconFairy + "/F_" + i.ToString());
			//for (int j = 0; j < m_arSpriteIcon_Fairy[i].Length; j++)
			//{
			//	Debug.Log("sprite : " + m_arSpriteIcon_Fairy[i][j].ToString());
			//}
		}
		
		m_arSpriteVehicleBackward = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteVehicleBackward);
		m_arSpriteVehicleForward = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteVehicleForward);
		m_arSpriteStarLight = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteStarLight);
		m_arSpriteBackgroundNormal = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteBackgroundNormal);
		m_arBonusObj = Resources.LoadAll<Sprite>(C_ResourcesPath.SpriteBonusObj);

		C_BaseDataController.Instance.SetVehicleMaxLV_BySpriteCount(m_arSpriteVehicleBackward.Length);
	}

	public Sprite GetSpriteIcon_Fairy(int nFairyNum)
	{ 
		return m_arSpriteIcon_Fairy[nFairyNum][0];
	}

	public Sprite GetSpriteCharacter(E_CharacterMode eMode, E_CharacterFaceType eFaceType, E_CharacterTalkType eTalkType)
	{
		return m_arSpriteCharacter[(int)eMode][(int)eFaceType][(int)eTalkType];
	}

    public Sprite GetSpriteCharacter_Origin(E_CharacterMode eMode, E_CharacterFaceType eFaceType, E_CharacterTalkType eTalkType)
    {
        return m_arSpriteCharacter_Origin[(int)eMode][(int)eFaceType][(int)eTalkType];
    }

    public Sprite GetSpriteVehicleBackward(int nVehicleLevel)
	{
		return m_arSpriteVehicleBackward[nVehicleLevel];
	}
	public Sprite GetSpriteVehicleForward(int nVehicleLevel)
	{
		return m_arSpriteVehicleForward[nVehicleLevel];
	}

	public Sprite GetSpriteStarLight(E_StarLightType eStarLightType)
	{
		return m_arSpriteStarLight[(int)eStarLightType];
	}

	public Sprite[] GetSpriteArrayBackground_Normal()
	{
		return m_arSpriteBackgroundNormal;
	}

	public Sprite[] GetSpriteArrayBonusObj()
	{
		return m_arBonusObj;
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
