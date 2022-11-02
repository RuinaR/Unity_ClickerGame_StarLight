using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_BackgroundNormalController : MonoBehaviour {

	private Sprite[] m_arSpriteBackgroundNormal;

	private Vector2 m_vec2BackgroundPosDefault;
	private SpriteRenderer m_sr;
	private Rigidbody2D m_rb;
	private Animator animator;

	private int m_nCurrentBackgroundIndexNormal;


	public void Init()
	{
		m_arSpriteBackgroundNormal = C_SpriteManager.Instance.GetSpriteArrayBackground_Normal();
		m_rb = gameObject.GetComponent<Rigidbody2D>();
		m_sr = gameObject.GetComponent<SpriteRenderer>();
		animator = gameObject.GetComponent<Animator>();

		m_vec2BackgroundPosDefault = new Vector2(6.5f, 0.0f);
		m_nCurrentBackgroundIndexNormal = 0;

		m_sr.sprite = m_arSpriteBackgroundNormal[m_nCurrentBackgroundIndexNormal];
	}

	public void MoveStart()
	{
		m_rb.velocity = Vector2.left * 0.5f;
	}
	private void MoveStop()
	{
		m_rb.velocity = Vector2.zero;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("BackgroundBoundary"))
		{
			MoveStop();
			animator.SetTrigger(C_AnimationHashList.nBlend);
		}
	}

	private void ChangeBackgroundNormalForAnim()
	{
		if (m_nCurrentBackgroundIndexNormal + 1 < m_arSpriteBackgroundNormal.Length)
		{
			m_nCurrentBackgroundIndexNormal++;
		}
		else
		{
			m_nCurrentBackgroundIndexNormal = 0;
		}
		m_sr.sprite = m_arSpriteBackgroundNormal[m_nCurrentBackgroundIndexNormal];
		gameObject.transform.position = m_vec2BackgroundPosDefault;
		MoveStart();
	}

}
