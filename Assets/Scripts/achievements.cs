using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct LevelAchievements
{
	public bool completelevel;
	public bool animalslive;
	public bool animalsdead;
	public bool allbacon;
	public bool undertime;
}

public class achievements : Commons {
	public static List<LevelAchievements> LevelAchieveList=new List<LevelAchievements>();
	public static List<LevelAchievements> BonusLevelAchieveList=new List<LevelAchievements>();
	public static int numoflevels=2;
	public static int bonuslevels=1;
	public static float percentcomplete=0;
	public static LevelAchievements OverallAchieve = new LevelAchievements();
	public static bool alllevelscompleted=true;
	public static bool allanimalslive=true;
	public static bool allanimalsdead=true;
	public static bool allbaconeaton = true;
	public static bool allundertime=true;
	public static bool allbonuslevelscompleted=true;
	public static bool allbonusanimalslive=true;
	public static bool allbonusanimalsdead=true;
	public static bool allbonusbaconeaton = true;
	public static bool allbonusundertime=true;
	public static int numberofbaconeaton=0;
	private static bool firsttime=true;
	
	// Update is called once per frame
	void Update () {
		if(firsttime)
		{
			for(int i=0; i<numoflevels; i++)
			{
				LevelAchievements Level=new LevelAchievements();
				Level.completelevel=false;
				Level.animalslive=false;
				Level.animalsdead=false;
				Level.allbacon=false;
				Level.undertime=false;
				LevelAchieveList.Add(Level);
			}
			for(int j=0; j<bonuslevels; j++)
			{
				LevelAchievements Level=new LevelAchievements();
				Level.completelevel=false;
				Level.animalslive=false;
				Level.animalsdead=false;
				Level.allbacon=false;
				Level.undertime=false;
				BonusLevelAchieveList.Add(Level);
			}
			firsttime=false;
		}
	}
	
	public void updatepercent()
	{
		float percentperachieve = 100/(numoflevels*5);
		float numofachievecomplete=0;
		foreach(LevelAchievements levelachieve in LevelAchieveList)
		{
			if(levelachieve.completelevel==true)
				numofachievecomplete++;
			if(levelachieve.animalslive==true)
				numofachievecomplete++;
			if(levelachieve.animalsdead==true)
				numofachievecomplete++;
			if(levelachieve.allbacon==true)
				numofachievecomplete++;
			if(levelachieve.undertime==true)
				numofachievecomplete++;
		}
		foreach(LevelAchievements levelachieve in BonusLevelAchieveList)
		{
			if(levelachieve.completelevel==true)
				numofachievecomplete++;
			if(levelachieve.animalslive==true)
				numofachievecomplete++;
			if(levelachieve.animalsdead==true)
				numofachievecomplete++;
			if(levelachieve.allbacon==true)
				numofachievecomplete++;
			if(levelachieve.undertime==true)
				numofachievecomplete++;
		}
		percentcomplete=numofachievecomplete*percentperachieve;
	}
	
	public void updatebaconeaton()
	{
		numberofbaconeaton=0;
		foreach(LevelAchievements levelachieve in LevelAchieveList)
		{
			if(levelachieve.allbacon==true)
				numberofbaconeaton++;
		}
		foreach(LevelAchievements levelachieve in BonusLevelAchieveList)
		{
			if(levelachieve.allbacon==true)
				numberofbaconeaton++;
		}
	}
	
	public void UpDateAchievements()
	{
		alllevelscompleted=true;
		allanimalslive=true;
		allanimalsdead=true;
		allbaconeaton=true;
		allundertime=true;
		foreach(LevelAchievements levelachieve in LevelAchieveList)
		{
			if(levelachieve.completelevel==false)
				alllevelscompleted=false;
			if(levelachieve.animalslive==false)
				allanimalslive=false;
			if(levelachieve.animalsdead==false)
				allanimalsdead=false;
			if(levelachieve.allbacon==false)
				allbaconeaton=false;
			if(levelachieve.undertime==false)
				allundertime=false;
		}
		allbonuslevelscompleted=true;
		allbonusanimalslive=true;
		allbonusanimalsdead=true;
		allbonusbaconeaton=true;
		allbonusundertime=true;
		foreach(LevelAchievements levelachieve in BonusLevelAchieveList)
		{
			if(levelachieve.completelevel==false)
				allbonuslevelscompleted=false;
			if(levelachieve.animalslive==false)
				allbonusanimalslive=false;
			if(levelachieve.animalsdead==false)
				allbonusanimalsdead=false;
			if(levelachieve.allbacon==false)
				allbonusbaconeaton=false;
			if(levelachieve.undertime==false)
				allbonusundertime=false;
		}
	}
	//Get regular levels
	public bool getcomplete(int Levelnum)
	{
		return LevelAchieveList[Levelnum].completelevel;
	}
	public bool getalive(int Levelnum)
	{
		return LevelAchieveList[Levelnum].animalslive;
	}
	public bool getdead(int Levelnum)
	{
		return LevelAchieveList[Levelnum].animalsdead;
	}
	public bool gettime(int Levelnum)
	{
		return LevelAchieveList[Levelnum].undertime;
	}
	public bool getbacon(int Levelnum)
	{
		return LevelAchieveList[Levelnum].allbacon;
	}
	//get bonus levels
	public bool getBonuscomplete(int Levelnum)
	{
		return BonusLevelAchieveList[Levelnum].completelevel;
	}
	public bool getBonusalive(int Levelnum)
	{
		return BonusLevelAchieveList[Levelnum].animalslive;
	}
	public bool getBonusdead(int Levelnum)
	{
		return BonusLevelAchieveList[Levelnum].animalsdead;
	}
	public bool getBonustime(int Levelnum)
	{
		return BonusLevelAchieveList[Levelnum].undertime;
	}
	public bool getBonusbacon(int Levelnum)
	{
		return BonusLevelAchieveList[Levelnum].allbacon;
	}	
	//set regular achievements
	public void setcomplete(int Levelnum, bool completed)
	{
		LevelAchievements Level=LevelAchieveList[Levelnum];
		Level.completelevel=completed;
		LevelAchieveList[Levelnum]=Level;
	}
	public void setdead(int Levelnum, bool completed)
	{
		LevelAchievements Level=LevelAchieveList[Levelnum];
		Level.animalsdead=completed;
		LevelAchieveList[Levelnum]=Level;
	}
	public void setalive(int Levelnum, bool completed)
	{
		LevelAchievements Level=LevelAchieveList[Levelnum];
		Level.animalslive=completed;
		LevelAchieveList[Levelnum]=Level;
	}
	public void settime(int Levelnum, bool completed)
	{
		LevelAchievements Level=LevelAchieveList[Levelnum];
		Level.undertime=completed;
		LevelAchieveList[Levelnum]=Level;;
	}
	public void setbacon(int Levelnum, bool completed)
	{
		LevelAchievements Level=LevelAchieveList[Levelnum];
		Level.allbacon=completed;
		LevelAchieveList[Levelnum]=Level;
	}
	//set bonus achievements
	public void setBonuscomplete(int Levelnum, bool completed)
	{
		LevelAchievements Level=BonusLevelAchieveList[Levelnum];
		Level.completelevel=completed;
		BonusLevelAchieveList[Levelnum]=Level;
	}
	public void setBonusdead(int Levelnum, bool completed)
	{
		LevelAchievements Level=BonusLevelAchieveList[Levelnum];
		Level.animalsdead=completed;
		BonusLevelAchieveList[Levelnum]=Level;
	}
	public void setBonusalive(int Levelnum, bool completed)
	{
		LevelAchievements Level=BonusLevelAchieveList[Levelnum];
		Level.animalslive=completed;
		BonusLevelAchieveList[Levelnum]=Level;
	}
	public void setBonustime(int Levelnum, bool completed)
	{
		LevelAchievements Level=BonusLevelAchieveList[Levelnum];
		Level.undertime=completed;
		BonusLevelAchieveList[Levelnum]=Level;;
	}
	public void setBonusbacon(int Levelnum, bool completed)
	{
		LevelAchievements Level=BonusLevelAchieveList[Levelnum];
		Level.allbacon=completed;
		BonusLevelAchieveList[Levelnum]=Level;
	}
}
