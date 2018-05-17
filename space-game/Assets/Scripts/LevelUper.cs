﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUper : MonoBehaviour 
{
	int exp=0;
	int reqExp=30;
	int level=0;
	public GameObject UI;
	public GameObject UI2;
	
	public Weapons[] WeaponsPresets;
	
	void Start()
	{
		LevelUp(0);
		AdjustUI();
	}
	
	void LevelUp (int level)
	{
		Destroy(GetComponent<Weapons>());
		Weapons wpns = gameObject.AddComponent<Weapons>() as Weapons;
		wpns.Wpns = WeaponsPresets[level].Wpns;
		wpns.Configure();
	}
	
	void AddXP(int xp)
	{
		if(level==5)
		{
			exp=0;
			SendMessage("SuperHeal",xp);
		}
		else
		{
			exp+=xp;
			if(exp>=reqExp&&level<5)
			{
				level++;
				LevelUp(level);
				exp-=reqExp;
				reqExp=reqExp+20;
				if(level==5)
				{
					reqExp=0;
				}
			}
		}
		AdjustUI();
	}
	
	void AdjustUI()
	{
		UI.GetComponent<Text>().text=""+reqExp;
		if(level==5)
			UI.GetComponent<Text>().text="99999";
		
		UI2.GetComponent<Text>().text=""+exp;
	}
}