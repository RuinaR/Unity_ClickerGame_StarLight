using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_StarLightGainUIController : MonoBehaviour {

	[SerializeField]
	private Text m_text;

	public void SetText(string strText)
	{
		m_text.text = strText;
	}

	public void SetColor(Color color)
	{
		m_text.color = color;
	}
	
	private void SetActiveFalse()
	{
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		Invoke("SetActiveFalse", 1.0f);
	}

}
