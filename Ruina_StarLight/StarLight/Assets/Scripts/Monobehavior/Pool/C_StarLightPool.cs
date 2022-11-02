using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StarLightPool : C_Pool<C_StarLightController>
{
	public override void Init(C_StarLightController origin)
	{
		m_listPool = new List<C_StarLightController>();
		m_Origin = origin;

		for (int i = 0; i < 20; i++)
		{
			C_StarLightController newGameObject = Instantiate(m_Origin, gameObject.transform);
			newGameObject.Init();
			m_listPool.Add(newGameObject);
		}
	}

	public override C_StarLightController GetFromPool()
	{
		C_StarLightController starLight;
		if (base.GetFromPool(out starLight))
		{
			starLight.Init();
		}
		return starLight;
	}

	public override bool GetFromPool(out C_StarLightController get)
	{
		C_StarLightController starLight;
		if (base.GetFromPool(out starLight))
		{
			starLight.Init();
			get = starLight;
			return true;
		}
		else
		{
			get = starLight;
			return false;
		}
	}

	public List<C_StarLightController> GetList()
	{
		return m_listPool;
	}

}