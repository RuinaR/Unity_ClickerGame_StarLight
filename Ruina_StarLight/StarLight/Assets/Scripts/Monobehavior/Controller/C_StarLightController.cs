using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StarLightController : MonoBehaviour {

	private Rigidbody2D m_rb;
	[SerializeField]
	private Transform m_Star_tf;
	[SerializeField]
	private SpriteRenderer m_Star_sr;

	[SerializeField]
	private ParticleSystem m_Particle;
	private ParticleSystem.MainModule m_mainModule;

	private float m_fVelocity;
	private float m_fAngularVelocity;

	public void Init()
	{
		m_rb = GetComponent<Rigidbody2D>();
		m_mainModule = m_Particle.main;
		m_fVelocity = 2.0f;
		if(GetRandom(0.5f))
		{
			m_fAngularVelocity = 180.0f;
		}
		else
		{
			m_fAngularVelocity = -180.0f;
		}
	}

	public void SetTfPosition(Vector3 vec3Pos)
	{
		transform.position = vec3Pos;
	}

	public void SetStarLightType(E_StarLightType eType)
	{
		m_Star_sr.sprite = C_SpriteManager.Instance.GetSpriteStarLight(eType);

		m_mainModule.startColor = C_StarLightManager.arColorByStarLightType[(int)eType];
	}

	public void SetRb()
	{
		m_rb.velocity = Vector2.up * m_fVelocity;
	}

	public void ClearRb()
	{
		m_rb.velocity = Vector2.zero;
	}

	public void SetParticleActive(bool bIsActive)
	{
		m_Particle.gameObject.SetActive(bIsActive);
	}

	public void SetColorClear()
	{
		m_Star_sr.color = Color.clear;
	}
	public void SetColorWhite()
	{
		m_Star_sr.color = Color.white;
	}

	private bool GetRandom(float fPercentage)
	{
		if (Random.value <= fPercentage)
		{
			return true;
		}
		return false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("StarLightBoundary"))
		{
			ClearRb();
			gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		m_Star_tf.Rotate(0.0f, 0.0f, m_fAngularVelocity * Time.deltaTime);
	}

}
