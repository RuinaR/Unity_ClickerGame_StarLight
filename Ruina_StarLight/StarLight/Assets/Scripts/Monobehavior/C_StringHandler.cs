using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_StringHandler : MonoBehaviour
{
	public static C_StringHandler Instance { get; private set; }

	static private readonly string[] units = { "", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
											  "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	static public readonly double MAX = 1286569735282830000000000000000000000000.00;

	public string GetUnitText(double dData)
	{
		string strResult = "";

		string[] arSplited = dData.ToString("N").Split(',');

		if (arSplited.Length >= 2)
		{
			strResult = string.Format("{0}.{1} {2}", arSplited[0], arSplited[1].Substring(0, 2), units[arSplited.Length - 1]);
		}
		else
		{
			strResult = arSplited[0];
		}

		return strResult;
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}
}
