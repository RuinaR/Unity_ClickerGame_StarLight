using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ChangeModeEffect : MonoBehaviour {

	[SerializeField]
	private C_PlayerController m_cPlayer;

	public void PlayerUpdate()
	{
		m_cPlayer.UpdateCharacter();
	}
}
