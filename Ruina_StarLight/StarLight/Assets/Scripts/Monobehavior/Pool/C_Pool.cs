using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class C_Pool<T> : MonoBehaviour where T: Component {

	protected T m_Origin;
	protected List<T> m_listPool;

	public virtual void Init(T origin)
	{
		m_listPool = new List<T>();
		m_Origin = origin;

		for (int i = 0; i < 20; i++)
		{
			T newGameObject = Instantiate(m_Origin, gameObject.transform);
			m_listPool.Add(newGameObject);
		}
	}
	
	public virtual T GetFromPool()
	{
		for (int i = 0; i < m_listPool.Count; i++)
		{
			if (!m_listPool[i].gameObject.activeInHierarchy)
			{
				return m_listPool[i];
			}
		}
		T newGameObject = Instantiate(m_Origin, gameObject.transform);
		m_listPool.Add(newGameObject);
		return newGameObject;
	}

	public virtual bool GetFromPool(out T get)
	{
		for (int i = 0; i < m_listPool.Count; i++)
		{
			if (!m_listPool[i].gameObject.activeInHierarchy)
			{
				get = m_listPool[i];
				return false;
			}
		}
		T newGameObject = Instantiate(m_Origin, gameObject.transform);
		m_listPool.Add(newGameObject);
		get = newGameObject;
		return true;
	}
}
