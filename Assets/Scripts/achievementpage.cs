using UnityEngine;
using System.Collections;

public class achievementpage : achievements {
	public bool levelscompleted=false;
	public bool animalsalive=false;
	public bool animalsdead=false;
	public bool baconeaton=false;
	public bool undertime=false;
	public bool hundredpercent=false;
	public bool bonusAchieve=false;
	public static bool abovehundred=false;
	
	// Use this for initialization
	void Start () {		
	}
	
	// Update is called once per frame
	void Update () {
		if(tag=="Percent")
		{
			TextMesh textm = gameObject.GetComponent<TextMesh>();
			textm.text=percentcomplete.ToString()+"%";
		}
		if(tag=="achievement")
		{
			TextMesh textm = gameObject.GetComponent<TextMesh>();
			if(!bonusAchieve)
			{
				if(levelscompleted)
				{
					if(alllevelscompleted)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(animalsalive)
				{
					if(allanimalslive)				
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(animalsdead)
				{
					if(allanimalsdead)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(baconeaton)
				{
					if(allbaconeaton)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(undertime)
				{
					if(allundertime)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(hundredpercent)
				{
					if(percentcomplete>=100)
					{
						textm.color=Color.white;
						abovehundred=true;
					}
					else
						textm.color=Color.gray;
				}
			}
			else
			{
				if(levelscompleted)
				{
					if(allbonuslevelscompleted)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(animalsalive)
				{
					if(allbonusanimalslive)				
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(animalsdead)
				{
					if(allbonusanimalsdead)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(baconeaton)
				{
					if(allbonusbaconeaton)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(undertime)
				{
					if(allbonusundertime)					
						textm.color=Color.white;
					else
						textm.color=Color.gray;
				}
				else if(hundredpercent)
				{
					if(allbonusanimalsdead&&allbonusanimalslive&&allbonusbaconeaton&&allbonuslevelscompleted&&allbonusundertime)
					{
						textm.color=Color.white;
					}
					else
						textm.color=Color.gray;
				}
			}
		}
		if(tag=="achievementtrigger")
		{
			if(abovehundred)
			{
				GameObject[] objects = GameObject.FindGameObjectsWithTag("achievement");
				for(int w=0; w<objects.Length; w++)
				{
					achievementpage achpg = (achievementpage) objects[w].GetComponent(typeof(achievementpage));
					if(achpg.bonusAchieve)
					{
						MeshRenderer meshr = objects[w].GetComponent<MeshRenderer>();
						meshr.enabled=true;
					}
					else
					{
						if(!achpg.hundredpercent)
						{
							Vector3 vec = objects[w].transform.position;
							vec.x=-25;
							objects[w].transform.position=vec;
						}
					}
				}
			}
		}
	}
}
