using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.IO;

public class PlayLevel : Commons {
	public string filelocation = "";
	public List<GameType> gamelist = new List<GameType>();
	private bool downloadedlevel =false;
	private WWW wxml;
	public string typeofobj="";
	public GameObject target = null;
	// Use this for initialization
	void Start () {
		editormode=false;
		filelocation = levelLocation;
		wxml = new WWW(filelocation);
		StartCoroutine(LoadLevel(wxml));
		//FileStream fs = new FileStream(filelocation, FileMode.Open);
		//List<GameType> gamelist = (List<GameType>) ser.Deserialize(fs);
		//fs.Close();
		//Move this out of the start routine<<<<<<<<<<<<<<<<<<<<<<<<=====================================!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	}
	void Update()
	{
		if(wxml.progress>=1&&!downloadedlevel)
		{
			Debug.Log(gamelist[0].typevar.ToString());
			downloadedlevel=true;
			StopCoroutine("LoadLevel");
			foreach(GameType obj in gamelist)
			{
				Vector3 spawnposition = Vector3.zero;
				typeofobj=obj.typevar;
				if(typeofobj=="Wall")
				{
					target = GameObject.CreatePrimitive(PrimitiveType.Cube);
				}
				else if(typeofobj=="Turtle"||typeofobj=="Bird")
				{
					Quaternion spawn = Quaternion.Euler(0, 90, 0);
					target = (GameObject)Instantiate(Resources.Load(typeofobj), spawnposition, spawn);
				}
				else
				{
					target = (GameObject)Instantiate(Resources.Load(obj.typevar), spawnposition, Quaternion.identity);
				}
				target.transform.position=obj.positionvar;
				target.transform.localScale = obj.scalevar;
				target.name= obj.namevar;
				if(typeofobj=="Switch")
				{
					int switchnum= (int)obj.number1var;
					Switch switchscript = (Switch) target.GetComponent(typeof(Switch));
					switchscript.switchnumber= switchnum;
				}
				else if(typeofobj=="Door")
				{
					int doornum=(int)obj.number1var;
					int howfardoor=(int)obj.number2var;
					DoorMove doorscript = (DoorMove) target.GetComponent(typeof(DoorMove));
					doorscript.Vertical=obj.boolean1var;
					doorscript.positive=obj.boolean2var;
					doorscript.destroydoor=obj.boolean3var;
					doorscript.holddoor=obj.boolean4var;
					doorscript.numberofdoor=doornum;
					doorscript.howfar=howfardoor;
				}
				else if(typeofobj=="Gun")
				{
					float timebetween=obj.number1var;
					float timedelaystart=obj.number2var;
					float bulletspeed=obj.number3var;
					Gunscript cannon = (Gunscript) target.GetComponent(typeof(Gunscript));
					cannon.timetowait=timebetween;
					cannon.startDelay=timedelaystart;
					cannon.bulletspeed=bulletspeed;
					cannon.shootvertical=obj.boolean1var;
					cannon.shootpositive=obj.boolean2var;
					cannon.posion=obj.boolean3var;
					cannon.water=obj.boolean4var;
				}
				else if(typeofobj=="EndFlag")
				{
					int timetobeat=(int)obj.number1var;
					EndLevel end = (EndLevel) target.GetComponent(typeof(EndLevel));
					end.BonusLevel=obj.boolean1var;
					end.UserCreatedLevel = obj.boolean2var;
					end.LastLevel= obj.boolean3var;
					end.TimetoBeat=timetobeat;
				}
				else if(typeofobj=="Water")
				{
					Debug.Log("Y Scale: "+target.transform.localScale.y.ToString());
					if(target.transform.localScale.y>1)
					{
						Debug.Log("it should spawn stuff here");
						Water wet = (Water) target.GetComponent(typeof(Water));
						wet.mainwater=target.gameObject;
						GameObject turtlewater = (GameObject)Instantiate(Resources.Load("TurtleWater"), spawnposition, Quaternion.identity);
						Vector3 scale = new Vector3(target.transform.position.x, target.transform.position.y+((target.transform.localScale.y/2)-0.5f), target.transform.position.z);
						turtlewater.transform.position=scale;
						scale = new Vector3(target.transform.localScale.x, 1, target.transform.localScale.z*0.95f);
						turtlewater.transform.localScale=scale;
						wet.turtlewater=turtlewater;
						GameObject deepwater = (GameObject)Instantiate(Resources.Load("DeepWater"), spawnposition, Quaternion.identity);
						scale=new Vector3(target.transform.position.x, target.transform.position.y-0.5f, target.transform.position.z);
						deepwater.transform.position=scale;
						scale = new Vector3(target.transform.localScale.x, target.transform.localScale.y-1, target.transform.localScale.z*0.95f);
						deepwater.transform.localScale=scale;
						wet.deepwater=deepwater;
					}
				}
			}
			Debug.Log("Load Completed");
			editormode=false;
		}
	}

	IEnumerator LoadLevel(WWW w)
	{
		yield return w;
		Debug.Log (w.progress.ToString());
		if (w.progress >= 1)
		{
			//Debug.Log(w.text);
			XmlSerializer ser = new XmlSerializer(typeof(List<GameType>));
			gamelist = (List<GameType>) ser.Deserialize(new StringReader(w.text));
			//Debug.Log(gamelist[0].typevar.ToString());
		}
	}
}
