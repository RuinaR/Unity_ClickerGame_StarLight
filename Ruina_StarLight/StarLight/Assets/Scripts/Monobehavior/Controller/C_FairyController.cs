using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FairyController : MonoBehaviour {

	private int m_nFairyNum;
	private Coroutine m_coroutineGetStarLight;
	private Rigidbody2D m_rb;
	private SpriteRenderer m_sr;

	private IEnumerator coroutineGetStarLight()
	{
		Vector2 vec2Velocity = new Vector2(0.0f, 0.0f);
		float fCheckTime = 0.1f;
		WaitForSeconds CheckTime = new WaitForSeconds(fCheckTime);
		WaitForSeconds OneSec = new WaitForSeconds(1.0f);
		bool bChangeDirFlag_x = false;
		bool bChangeDirFlag_y = false;


		while (true)
		{
			
			float fWaitTime = Random.Range(0.5f, 1.2f);
			float fElapsedTime = 0.0f;
			vec2Velocity.x = Random.Range(C_ScreenArea.fX_Left, C_ScreenArea.fX_Right);
			vec2Velocity.y = Random.Range(C_ScreenArea.fY_Bottom, C_ScreenArea.fY_Top);
			vec2Velocity.Normalize();
			//vec2Velocity *= 1.5f;
			m_rb.velocity = vec2Velocity;

			bChangeDirFlag_x = false;
			bChangeDirFlag_y = false;
			while (fElapsedTime <= fWaitTime)
			{

				if (!bChangeDirFlag_x)
				{
					if (gameObject.transform.position.x < C_ScreenArea.fX_Left ||
						gameObject.transform.position.x > C_ScreenArea.fX_Right)
					{
						vec2Velocity.x *= -1.0f;
						m_rb.velocity = vec2Velocity;
						bChangeDirFlag_x = true;
					}
				}
				if (!bChangeDirFlag_y)
				{
					if (gameObject.transform.position.y < C_ScreenArea.fY_Bottom ||
						gameObject.transform.position.y > C_ScreenArea.fY_Top)
					{
						vec2Velocity.y *= -1.0f;
						m_rb.velocity = vec2Velocity;
						bChangeDirFlag_y = true;
					}
				}

				if (vec2Velocity.x < 0)
				{
					m_sr.flipX = false;
				}
				else
				{
					m_sr.flipX = true;
				}

				gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, C_ScreenArea.fX_Left, C_ScreenArea.fX_Right),
															Mathf.Clamp(gameObject.transform.position.y, C_ScreenArea.fY_Bottom, C_ScreenArea.fY_Top), 0.0f);
				yield return CheckTime;
				fElapsedTime += fCheckTime;
			}
			m_rb.velocity = Vector2.zero;
			C_StarLightManager.Instance.GetStarLightByFairy(m_nFairyNum, gameObject.transform);
			yield return OneSec;
		}
	}


	public void Init(int nFairyNum)
	{
		m_rb = GetComponent<Rigidbody2D>();
		m_sr = GetComponent<SpriteRenderer>();
		m_nFairyNum = nFairyNum;
	}
	public void StartCoroutine()
	{
		m_coroutineGetStarLight = StartCoroutine(coroutineGetStarLight());
	}
	public void StopCoroutine()
	{
		StopCoroutine(m_coroutineGetStarLight);
	}
}
