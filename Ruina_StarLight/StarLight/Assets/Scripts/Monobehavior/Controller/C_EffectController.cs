using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_EffectController : MonoBehaviour {

	private void SetActiveFalse()
	{
		gameObject.SetActive(false);
	}

	protected void OnEnable()
	{
		Invoke("SetActiveFalse", 1.0f);
	}
	public virtual void SetStarLight(C_StarLightController cStarLight)
	{ }
}
