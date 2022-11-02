using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_SpeechBubbleController : MonoBehaviour {

	[SerializeField]
	private BubbleArray[] m_arBubble;

	private SpriteRenderer m_sr;
	private Coroutine coroutine;

	public void Init()
	{
		m_sr = GetComponent<SpriteRenderer>();
		m_sr.color = Color.clear;

		if (m_arBubble.Length != (int)E_SpeechBubble.max)
		{
			Debug.Log("Bublle Setting Error");
		}
	}

	public void StartBubbleCoroutine(E_SpeechBubble eBubble)
	{
		coroutine = StartCoroutine(CoroutineBubble(eBubble));
	}

	public void StopBubbleCoroutine()
	{
		StopCoroutine(coroutine);
		m_sr.color = Color.clear;
	}


	private IEnumerator CoroutineBubble(E_SpeechBubble eBubble)
	{
		int nCount = 0;
		WaitForSeconds wait = new WaitForSeconds(0.25f);
		m_sr.color = Color.white;
		while (true)
		{
			m_sr.sprite = m_arBubble[(int)eBubble].arSprite[nCount % 3];
			yield return wait;
			nCount++;
		}
	}

}

[System.Serializable]
public class BubbleArray
{
	public Sprite[] arSprite;
}