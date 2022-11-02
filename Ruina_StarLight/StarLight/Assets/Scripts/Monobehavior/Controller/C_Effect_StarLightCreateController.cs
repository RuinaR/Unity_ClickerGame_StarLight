using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Effect_StarLightCreateController : C_EffectController {

	private C_StarLightController m_cStarLight;

	public override void SetStarLight(C_StarLightController cStarLight)
	{
		m_cStarLight = cStarLight;
	}

	private void ActiveStarLight()
	{
		m_cStarLight.SetRb();
		m_cStarLight.SetColorWhite();
		m_cStarLight.SetParticleActive(true);
		C_SoundManager.Instance.ActiveSoundEffect(E_Sound_Effect.starLight_Create);
	}

	private new void OnEnable()
	{
		base.OnEnable();
	}
}
