using UnityEngine;
using System.Collections;

public class LevelSelect : achievements {
	public bool complete=false;
	public bool alive=false;
	public bool dead=false;
	public bool bacon=false;
	public bool time=false;
	public int levelnum=0;
	public bool bonuslevel=false;
	private bool bonusunlocked=false;
	private int nonlevelscenes=2;
	private int regularlevels=2;
	private bool showing=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(tag=="achievebubble")
		{
			if(!bonuslevel)
			{
				if(complete)
				{
					if(getcomplete(levelnum))
					{
						transform.renderer.material.color=Color.yellow;
					}
					else
					{
						transform.renderer.material.color=Color.grey;
					}
				}
				else if(alive)
				{
					if(getalive(levelnum))
					{
						transform.renderer.material.color=Color.yellow;
					}
					else
					{
						transform.renderer.material.color=Color.grey;
					}
				}
				else if(dead)
				{
					if(getdead(levelnum))
					{
						transform.renderer.material.color=Color.yellow;
					}
					else
					{
						transform.renderer.material.color=Color.grey;
					}
				}
				else if(bacon)
				{
					if(getbacon(levelnum))
					{
						transform.renderer.material.color=Color.yellow;
					}
					else
					{
						transform.renderer.material.color=Color.grey;
					}
				}
				else if(time)
				{
					if(gettime(levelnum))
					{
						transform.renderer.material.color=Color.yellow;
					}
					else
					{
						transform.renderer.material.color=Color.grey;
					}
				}
			}
			else
			{
				if(bonusunlocked)
				{
					if(complete)
					{
						if(getBonuscomplete(levelnum))
						{
							transform.renderer.material.color=Color.yellow;
						}
						else
						{
							transform.renderer.material.color=Color.grey;
						}
					}
					else if(alive)
					{
						if(getBonusalive(levelnum))
						{
							transform.renderer.material.color=Color.yellow;
						}
						else
						{
							transform.renderer.material.color=Color.grey;
						}
					}
					else if(dead)
					{
						if(getBonusdead(levelnum))
						{
							transform.renderer.material.color=Color.yellow;
						}
						else
						{
							transform.renderer.material.color=Color.grey;
						}
					}
					else if(bacon)
					{
						if(getBonusbacon(levelnum))
						{
							transform.renderer.material.color=Color.yellow;
						}
						else
						{
							transform.renderer.material.color=Color.grey;
						}
					}
					else if(time)
					{
						if(getBonustime(levelnum))
						{
							transform.renderer.material.color=Color.yellow;
						}
						else
						{
							transform.renderer.material.color=Color.grey;
						}
					}
					if(!showing)
					{
						if(bonusunlocked)
						{
							MeshRenderer meshr = gameObject.GetComponent<MeshRenderer>();
							meshr.enabled=true;
						}
						showing=true;
					}
				}
				else
				{
					if(levelnum==0)
					{
						if(percentcomplete>=100)
						{
							bonusunlocked=true;
						}
					}
					else
					{
						GameObject[] objects = GameObject.FindGameObjectsWithTag("achievementbubble");
						for(int w=0; w<objects.Length; w++)
						{
							LevelSelect lvsel = (LevelSelect) objects[w].GetComponent(typeof(LevelSelect));
							if(lvsel.bonuslevel&&lvsel.bonusunlocked&&lvsel.levelnum==levelnum-1)
							{
								if(lvsel.getBonuscomplete(levelnum))
								{
									bonusunlocked=true;
									w=objects.Length;
								}
							}
						}
					}
				}
			}
		}
		else if(tag=="levelbutton")
		{
			if(bonuslevel)
			{
				if(!bonusunlocked)
				{
					if(levelnum==0)
					{
						if(percentcomplete>=100)
						{
							bonusunlocked=true;
						}
					}
					else
					{
						GameObject[] objects = GameObject.FindGameObjectsWithTag("levelbutton");
						for(int w=0; w<objects.Length; w++)
						{
							LevelSelect lvsel = (LevelSelect) objects[w].GetComponent(typeof(LevelSelect));
							if(lvsel.bonuslevel&&lvsel.bonusunlocked&&lvsel.levelnum==levelnum-1)
							{
								if(lvsel.getBonuscomplete(levelnum))
								{
									bonusunlocked=true;
									w=objects.Length;
								}
							}
						}
					}
				}
				else
				{
					if(!showing)
					{
						if(bonusunlocked)
						{
							MeshRenderer meshr = gameObject.GetComponent<MeshRenderer>();
							meshr.enabled=true;
						}
						showing=true;
					}
				}
			}
		}
	}
	
//	GameObject[] objects = GameObject.FindGameObjectsWithTag("achievement");
//				for(int w=0; w<objects.Length; w++)
//				{
//					achievementpage achpg = (achievementpage) objects[w].GetComponent(typeof(achievementpage));
//					if(achpg.bonusAchieve)
//					{
//						MeshRenderer meshr = objects[w].GetComponent<MeshRenderer>();
//						meshr.enabled=true;
//					}
//				}
	
	void OnLevelWasLoaded(int level) {
        if(level==3)
		{
			UpDateAchievements();
			updatepercent();
		}        
    }
	
	void OnMouseDown()
	{		
		if(tag=="levelbutton")
		{
			editormode=false;
			if(!bonuslevel)
			{
				int num=levelnum+nonlevelscenes;
				Application.LoadLevel(num);
			}
			else
			{
				if(bonusunlocked)
				{
					int num=levelnum+nonlevelscenes+regularlevels;
					Application.LoadLevel(num);
				}
			}
		}
	}
}
