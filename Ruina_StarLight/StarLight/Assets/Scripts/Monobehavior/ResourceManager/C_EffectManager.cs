using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EffectManager : MonoBehaviour
{
	private delegate C_EffectController delGetEffectStarLightTap();

	public static C_EffectManager Instance { get; private set; }

	[SerializeField]
	private C_EffectPool[] m_arEffectPool;

	delGetEffectStarLightTap[] m_arDelGetEffectStarLightTap;

	public void Load()
	{
		m_arEffectPool[(int)E_Effect.starLight_Create].Init(Resources.Load<C_EffectController>(C_ResourcesPath.EffectStarLightCreate));
		m_arEffectPool[(int)E_Effect.starLightTap_Default].Init(Resources.LoadAll<C_EffectController>(C_ResourcesPath.EffectStarLightTap)[(int)E_StarLightType.starLightDefault]);
		m_arEffectPool[(int)E_Effect.starLightTap_Blue].Init(Resources.LoadAll<C_EffectController>(C_ResourcesPath.EffectStarLightTap)[(int)E_StarLightType.starLightBlue]);
		m_arEffectPool[(int)E_Effect.starLightTap_Red].Init(Resources.LoadAll<C_EffectController>(C_ResourcesPath.EffectStarLightTap)[(int)E_StarLightType.starLightRed]);
		m_arEffectPool[(int)E_Effect.upgrade].Init(Resources.Load<C_EffectController>(C_ResourcesPath.EffectUpgrade));

		m_arDelGetEffectStarLightTap = new delGetEffectStarLightTap[3];
		m_arDelGetEffectStarLightTap[(int)E_StarLightType.starLightDefault] = GetEffect_StarLightTap_Default;
		m_arDelGetEffectStarLightTap[(int)E_StarLightType.starLightBlue] = GetEffect_StarLightTap_Blue;
		m_arDelGetEffectStarLightTap[(int)E_StarLightType.starLightRed] = GetEffect_StarLightTap_Red;
	}

	public C_EffectController GetEffect(E_Effect eEffect)
	{
		return m_arEffectPool[(int)eEffect].GetFromPool();
	}

	public C_EffectController GetEffect_StarLightTap(E_StarLightType eStarLightType)
	{
		if(eStarLightType == E_StarLightType.starLightFairy)
		{
			return null;
		}
		return m_arDelGetEffectStarLightTap[(int)eStarLightType]();
	}



	private C_EffectController GetEffect_StarLightTap_Default()
	{
		return m_arEffectPool[(int)E_Effect.starLightTap_Default].GetFromPool();
	}
	private C_EffectController GetEffect_StarLightTap_Blue()
	{
		return m_arEffectPool[(int)E_Effect.starLightTap_Blue].GetFromPool();
	}
	private C_EffectController GetEffect_StarLightTap_Red()
	{
		return m_arEffectPool[(int)E_Effect.starLightTap_Red].GetFromPool();
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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
