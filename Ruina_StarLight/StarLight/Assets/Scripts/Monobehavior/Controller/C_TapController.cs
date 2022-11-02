using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TapController : MonoBehaviour
{
	public static C_TapController Instance { get; private set; }

	private LayerMask m_LayerMask;

	public void Init()
	{
		m_LayerMask = LayerMask.GetMask("TapArea");
	}

	private Ray GenerateRay(Vector3 vec3Position)
	{
		Vector3 vec3FarPos = Camera.main.ScreenToWorldPoint(
											new Vector3(vec3Position.x, vec3Position.y, Camera.main.farClipPlane));
		Vector3 vec3NearPos = Camera.main.ScreenToWorldPoint(
											new Vector3(vec3Position.x, vec3Position.y, Camera.main.nearClipPlane));

		return new Ray(vec3NearPos, Vector3.Normalize(vec3FarPos - vec3NearPos));
	}

	public bool Tap(Collider colliderTap, ref RaycastHit hitOut)
	{
#if UNITY_ANDROID || UNITY_IOS
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					Ray ray = GenerateRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask))
					{
						if (hit.collider.Equals(colliderTap))
						{
							hitOut = hit;
							return true;
						}
					}
				}
			}
			return false;
#elif UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = GenerateRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask))
			{
				if (hit.collider.Equals(colliderTap))
				{
					hitOut = hit;
					return true;
				}
			}
		}
		return false;
#endif
	}
	public bool Tap(string strColliderObjectTag, ref RaycastHit hitOut)
	{
#if UNITY_ANDROID || UNITY_IOS
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					Ray ray = GenerateRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
					{
						if (hit.transform.gameObject.CompareTag(strColliderObjectTag))
						{
							hitOut = hit;
							return true;
						}
					}
				}
			}
			return false;
#elif UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = GenerateRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
			{
				if (hit.transform.gameObject.CompareTag(strColliderObjectTag))
				{
					hitOut = hit;
					return true;
				}
			}
		}
		return false;
#endif
	}
	public bool Tap(Collider colliderTap)
	{
#if UNITY_ANDROID || UNITY_IOS
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					Ray ray = GenerateRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
					{
						if (hit.collider.Equals(colliderTap))
						{
							return true;
						}
					}
				}
			}
			return false;
#elif UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = GenerateRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
			{
				if (hit.collider.Equals(colliderTap))
				{
					return true;
				}
			}
		}
		return false;
#endif
	}
	public bool Tap(string strColliderObjectTag)
	{
#if UNITY_ANDROID || UNITY_IOS
			for (int i = 0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if (touch.phase == TouchPhase.Began)
				{
					Ray ray = GenerateRay(touch.position);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
					{
						if (hit.transform.gameObject.CompareTag(strColliderObjectTag))
						{
							return true;
						}
					}
				}
			}
			return false;
#elif UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = GenerateRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f, m_LayerMask)) 
			{
				if (hit.transform.gameObject.CompareTag(strColliderObjectTag))
				{
					return true;
				}
			}
		}
		return false;
#endif
	}
	public bool Tap()
	{
#if UNITY_ANDROID || UNITY_IOS
			
		if(Input.touchCount > 0)
		{
			return true;
		}
		return false;
#elif UNITY_EDITOR

		if (Input.GetMouseButtonDown(0))
		{
			return true;
		}
		return false;
#endif
	}

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
