using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_VehicleController : MonoBehaviour {	

	[SerializeField]
	private SpriteRenderer m_srVehicleBackward;
	[SerializeField]
	private SpriteRenderer m_srVehicleForward;
	private C_UserData m_cUserData;
	


	public void Init()
	{
		m_cUserData = C_UserDataController.Instance.GetUserData();

		UpdateVehicle();
	}

	public void UpdateVehicle()
	{
		m_srVehicleBackward.sprite = 
			C_SpriteManager.Instance.GetSpriteVehicleBackward
            (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] / C_BaseDataController.nVehicleChangeLV);
		m_srVehicleForward.sprite = 
			C_SpriteManager.Instance.GetSpriteVehicleForward
            (m_cUserData.arPlayerUpgradeLevel[(int)E_PlayerUpgrade.vehicle] / C_BaseDataController.nVehicleChangeLV);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
