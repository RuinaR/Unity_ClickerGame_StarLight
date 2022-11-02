using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class C_AnimationHashList
{
	public static int nTap;
	public static int nBlend;
	public static int nFeverStart;
	public static int nFeverEnd;
	public static int nMainUIActive;
	public static int nMainUIDisable;

	public static int nCat_Walk;
	public static int[] arCat_Motion;



	public static void Init()
	{
		nTap = Animator.StringToHash("Tap");
		nBlend = Animator.StringToHash("Blend");
		nFeverStart = Animator.StringToHash("FeverStart");
		nFeverEnd = Animator.StringToHash("FeverEnd");
		nMainUIActive = Animator.StringToHash("MainUIActive");
		nMainUIDisable = Animator.StringToHash("MainUIDisable");

		nCat_Walk = Animator.StringToHash("bWalk");

		arCat_Motion = new int[15];
		arCat_Motion[(int)E_CatMotion.Scratch] = Animator.StringToHash("bScratch");
		arCat_Motion[(int)E_CatMotion.Attack] = Animator.StringToHash("bAttack");
		arCat_Motion[(int)E_CatMotion.Ennui] = Animator.StringToHash("bEnnui");
		arCat_Motion[(int)E_CatMotion.Lick] = Animator.StringToHash("bLick");
		arCat_Motion[(int)E_CatMotion.Relax] = Animator.StringToHash("bRelax");
		arCat_Motion[(int)E_CatMotion.Fly] = Animator.StringToHash("bFly");
		arCat_Motion[(int)E_CatMotion.Paw] = Animator.StringToHash("bPaw");
		arCat_Motion[(int)E_CatMotion.Tail] = Animator.StringToHash("bTail");
		arCat_Motion[(int)E_CatMotion.Dig] = Animator.StringToHash("bDig");
		arCat_Motion[(int)E_CatMotion.Sway] = Animator.StringToHash("bSway");
		arCat_Motion[(int)E_CatMotion.Stretch] = Animator.StringToHash("bStretch");
		arCat_Motion[(int)E_CatMotion.Sniff] = Animator.StringToHash("bSniff");
		arCat_Motion[(int)E_CatMotion.Sleep1] = Animator.StringToHash("bSleep1");
		arCat_Motion[(int)E_CatMotion.Sleep2] = Animator.StringToHash("bSleep2");
		arCat_Motion[(int)E_CatMotion.Sleep3] = Animator.StringToHash("bSleep3");
	}
}
