using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_ElementController_Shop : MonoBehaviour {

	[SerializeField]
	private Button m_Btn;
	[SerializeField]
	private Text m_text;

	public void SetBtnInteractable(bool bInteractable)
	{
		m_Btn.interactable = bInteractable;
	}
	public void InitBtnFunc(UnityEngine.Events.UnityAction Func)
	{
		m_Btn.onClick.AddListener(Func);
	}
	public void SetText(string str)
	{
		m_text.text = str;
	}
}
