using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class C_BaseDataController : MonoBehaviour {

	public static C_BaseDataController Instance { get; private set; }
	public static char cParagraphDivision;
	public static int nDecimalPlace_Expression;
	public static float fWaitTimeDialogue_Explain;
	public static float fWaitTimeDialogue_Talk;
    public static int nVehicleChangeLV;

	public static C_STR[] arCSTR;
	public static string[][] arCharacterText;
	public static string[][] arCharacterText_Random;
	public static string[][] arCharacterText_Random_Ed;
	public static string[][] arCharacterText_Greeting;
	public static string[] arTutorialText;

	public static string[][] arCharacterText_Greeting_End;
	public static string[] arCharacterText_NoAd;
	public static string[] arCharacterText_BannerAd;


	private C_CharacterModeData[] m_arCharacterModeData;
	public C_UpgradeData[] m_arPlayerUpgradeData;
	private C_UpgradeData[] m_arStarLightUpgradeData;
	private C_FairyUpgradeData[] m_arFairyUpgradeData;
	private C_SkillData[] m_arSkillData;


	public void Load()
	{
		cParagraphDivision = '|';
		nDecimalPlace_Expression = 2;
		fWaitTimeDialogue_Explain = 0.05f;
		fWaitTimeDialogue_Talk = 0.1f;
		nVehicleChangeLV = 3;
		////////////////////////////////////////////////////////////////////////////////////////////////	
		arCSTR = new C_STR[(int)E_Language.max];
		for (int i = 0; i < arCSTR.Length; i++)
		{
			arCSTR[i] = new C_STR();
		}

		string[] arCSTRtextData = new string[(int)E_Language.max];
		arCSTRtextData[(int)E_Language.korean] = (Resources.Load("BaseData/CSTR_Kor") as TextAsset).text;
		arCSTRtextData[(int)E_Language.english] = (Resources.Load("BaseData/CSTR_Eng") as TextAsset).text;

		if (string.IsNullOrEmpty(arCSTRtextData[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCSTRtextData[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error!");
		}
		else
		{
			arCSTR[(int)E_Language.korean] = JsonUtility.FromJson<C_STR>(arCSTRtextData[(int)E_Language.korean]);
			arCSTR[(int)E_Language.english] = JsonUtility.FromJson<C_STR>(arCSTRtextData[(int)E_Language.english]);
		}


		arCharacterText = new string[(int)E_Language.max][];
		string[] arCharacterTextData = new string[(int)E_Language.max];

		arCharacterTextData[(int)E_Language.korean] = (Resources.Load("BaseData/CharacterText_Kor") as TextAsset).text;
		arCharacterTextData[(int)E_Language.english] = (Resources.Load("BaseData/CharacterText_Eng") as TextAsset).text;

		if (string.IsNullOrEmpty(arCharacterTextData[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCharacterTextData[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error_CharacterText!");
		}
		else
		{
			TextData Kor = JsonUtility.FromJson<TextData>(arCharacterTextData[(int)E_Language.korean]);
			TextData Eng = JsonUtility.FromJson<TextData>(arCharacterTextData[(int)E_Language.english]);

			arCharacterText[(int)E_Language.korean] = Kor.str;
			arCharacterText[(int)E_Language.english] = Eng.str;
		}

		arCharacterText_Random = new string[(int)E_Language.max][];
		string[] arCharacterText_Random_Data = new string[(int)E_Language.max];

		arCharacterText_Random_Data[(int)E_Language.korean] = (Resources.Load("BaseData/CharacterText_Random_Kor") as TextAsset).text;
		arCharacterText_Random_Data[(int)E_Language.english] = (Resources.Load("BaseData/CharacterText_Random_Eng") as TextAsset).text;
		if (string.IsNullOrEmpty(arCharacterText_Random_Data[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCharacterText_Random_Data[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error_CharacterText_Random!");
		}
		else
		{
			TextData Kor = JsonUtility.FromJson<TextData>(arCharacterText_Random_Data[(int)E_Language.korean]);
			TextData Eng = JsonUtility.FromJson<TextData>(arCharacterText_Random_Data[(int)E_Language.english]);

			arCharacterText_Random[(int)E_Language.korean] = Kor.str;
			arCharacterText_Random[(int)E_Language.english] = Eng.str;
		}

		arCharacterText_Random_Ed = new string[(int)E_Language.max][];
		string[] arCharacterText_Random_Ed_Data = new string[(int)E_Language.max];

		arCharacterText_Random_Ed_Data[(int)E_Language.korean] = (Resources.Load("BaseData/CharacterText_Random_Ed_Kor") as TextAsset).text;
		arCharacterText_Random_Ed_Data[(int)E_Language.english] = (Resources.Load("BaseData/CharacterText_Random_Ed_Eng") as TextAsset).text;
		if (string.IsNullOrEmpty(arCharacterText_Random_Ed_Data[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCharacterText_Random_Ed_Data[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error_CharacterText_Random_Ed!");
		}
		else
		{
			TextData Kor = JsonUtility.FromJson<TextData>(arCharacterText_Random_Ed_Data[(int)E_Language.korean]);
			TextData Eng = JsonUtility.FromJson<TextData>(arCharacterText_Random_Ed_Data[(int)E_Language.english]);

			arCharacterText_Random_Ed[(int)E_Language.korean] = Kor.str;
			arCharacterText_Random_Ed[(int)E_Language.english] = Eng.str;		
		}

		arCharacterText_Greeting = new string[(int)E_Language.max][];
		string[] arCharacterText_Greeting_Data = new string[(int)E_Language.max];

		arCharacterText_Greeting_Data[(int)E_Language.korean] = (Resources.Load("BaseData/CharacterText_Greeting_Kor") as TextAsset).text;
		arCharacterText_Greeting_Data[(int)E_Language.english] = (Resources.Load("BaseData/CharacterText_Greeting_Eng") as TextAsset).text;
		if (string.IsNullOrEmpty(arCharacterText_Greeting_Data[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCharacterText_Greeting_Data[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error_CharacterText_Greeting!");
		}
		else
		{
			TextData Kor = JsonUtility.FromJson<TextData>(arCharacterText_Greeting_Data[(int)E_Language.korean]);
			TextData Eng = JsonUtility.FromJson<TextData>(arCharacterText_Greeting_Data[(int)E_Language.english]);

			arCharacterText_Greeting[(int)E_Language.korean] = Kor.str;
			arCharacterText_Greeting[(int)E_Language.english] = Eng.str;		
		}

		arCharacterText_Greeting_End = new string[(int)E_Language.max][];
		string[] arCharacterText_Greeting_End_Data = new string[(int)E_Language.max];
		arCharacterText_Greeting_End_Data[(int)E_Language.korean] = (Resources.Load("BaseData/CharacterText_Greeting_End_Kor") as TextAsset).text;
		arCharacterText_Greeting_End_Data[(int)E_Language.english] = (Resources.Load("BaseData/CharacterText_Greeting_End_Eng") as TextAsset).text;
		if (string.IsNullOrEmpty(arCharacterText_Greeting_End_Data[(int)E_Language.korean]) ||
			string.IsNullOrEmpty(arCharacterText_Greeting_End_Data[(int)E_Language.english]))
		{
			Debug.Log("BaseData_Load_Error_CharacterText_Greeting_End!");
		}
		else
		{
			TextData Kor = JsonUtility.FromJson<TextData>(arCharacterText_Greeting_End_Data[(int)E_Language.korean]);
			TextData Eng = JsonUtility.FromJson<TextData>(arCharacterText_Greeting_End_Data[(int)E_Language.english]);

			arCharacterText_Greeting_End[(int)E_Language.korean] = Kor.str;
			arCharacterText_Greeting_End[(int)E_Language.english] = Eng.str;
		}
		////////////////////////////////////////////////////////////////////////////////////////////////

		TextAsset[] textCharacterModeData = (Resources.LoadAll<TextAsset>("BaseData/CharacterModeData"));
		TextAsset[] textPlayerUpgradeData = (Resources.LoadAll<TextAsset>("BaseData/PlayerUpgradeData"));
		TextAsset[] textStarLightUpgradeData = (Resources.LoadAll<TextAsset>("BaseData/StarLightUpgradeData"));
		TextAsset[] textFairyUpgradeData = (Resources.LoadAll<TextAsset>("BaseData/FairyUpgradeData"));
		TextAsset[] textSkillData = (Resources.LoadAll<TextAsset>("BaseData/SkillData"));

		if (textCharacterModeData == null ||
			textPlayerUpgradeData == null ||
			textStarLightUpgradeData == null ||
			textFairyUpgradeData == null ||
			textSkillData == null)
		{
			Debug.Log("BaseData_Load_Error!");
		}
		else
		{
			m_arCharacterModeData = new C_CharacterModeData[textCharacterModeData.Length];
			for (int i = 0; i < textCharacterModeData.Length; i++)
			{
				m_arCharacterModeData[i] = JsonUtility.FromJson<C_CharacterModeData>(textCharacterModeData[i].text);
			}

			m_arPlayerUpgradeData = new C_UpgradeData[textPlayerUpgradeData.Length];
			for(int i = 0; i < textPlayerUpgradeData.Length; i++)
			{
				m_arPlayerUpgradeData[i] = JsonUtility.FromJson<C_UpgradeData>(textPlayerUpgradeData[i].text);
			}

			m_arStarLightUpgradeData = new C_UpgradeData[textStarLightUpgradeData.Length];
			for (int i = 0; i < textStarLightUpgradeData.Length; i++)
			{
				m_arStarLightUpgradeData[i] = JsonUtility.FromJson<C_UpgradeData>(textStarLightUpgradeData[i].text);
			}

			m_arFairyUpgradeData = new C_FairyUpgradeData[textFairyUpgradeData.Length];
			for (int i = 0; i < textFairyUpgradeData.Length; i++)
			{
				m_arFairyUpgradeData[i] = JsonUtility.FromJson<C_FairyUpgradeData>(textFairyUpgradeData[i].text);
			}

			m_arSkillData = new C_SkillData[textSkillData.Length];
			for (int i = 0; i < textSkillData.Length; i++)
			{
				m_arSkillData[i] = JsonUtility.FromJson<C_SkillData>(textSkillData[i].text);
			}
		}

		///////////////////////////////////////////////
		arTutorialText = new string[(int)E_Language.max];

		TextData_Single tutorial_Kor = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/Tutorial_Kor") as TextAsset).text);
		TextData_Single tutorial_Eng = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/Tutorial_Eng") as TextAsset).text);

		arTutorialText[(int)E_Language.korean] = tutorial_Kor.str;
		arTutorialText[(int)E_Language.english] = tutorial_Eng.str;

		arCharacterText_NoAd = new string[(int)E_Language.max];

		TextData_Single NoAd_Kor = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/CharacterText_NoAd_Kor") as TextAsset).text);
		TextData_Single NoAd_Eng = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/CharacterText_NoAd_Eng") as TextAsset).text);

		arCharacterText_NoAd[(int)E_Language.korean] = NoAd_Kor.str;
		arCharacterText_NoAd[(int)E_Language.english] = NoAd_Eng.str;

		arCharacterText_BannerAd = new string[(int)E_Language.max];

		TextData_Single BannerAd_Kor = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/CharacterText_BannerAd_Kor") as TextAsset).text);
		TextData_Single BannerAd_Eng = JsonUtility.FromJson<TextData_Single>((Resources.Load("BaseData/CharacterText_BannerAd_Eng") as TextAsset).text);

		arCharacterText_BannerAd[(int)E_Language.korean] = BannerAd_Kor.str;
		arCharacterText_BannerAd[(int)E_Language.english] = BannerAd_Eng.str;
	}


	public void SetVehicleMaxLV_BySpriteCount(int nCount)
	{
		m_arPlayerUpgradeData[(int)E_PlayerUpgrade.vehicle].nLevelMax = nVehicleChangeLV * nCount - 3;
	}

	public C_CharacterModeData[] GetCharacterModeData()
	{
		return m_arCharacterModeData;
	}

	public C_UpgradeData[] GetPlayerUpgradeData()
	{
		return m_arPlayerUpgradeData;
	}

	public C_UpgradeData[] GetStarLightUpgradeData()
	{
		return m_arStarLightUpgradeData;
	}

	public C_FairyUpgradeData[] GetFairyUpgradeData()
	{
		return m_arFairyUpgradeData;
	}

	public C_SkillData[] GetSkillData()
	{
		return m_arSkillData;
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
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}


public class TextData
{
	public string[] str;
}

public class TextData_Single
{
	public string str;
}



