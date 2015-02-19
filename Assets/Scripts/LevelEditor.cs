using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Text;

public class LevelEditor : Commons {
	private int objectnumber=0;
	private string objectname="";
	private GameObject target=null;
	private string typeofobj = "";
	private bool mousedown = false;

	private float targetxpos=0.0f;
	private string stringtarxpos = "0";
	private float targetypos=0.0f;
	private string stringtarypos="0";

	private float targetxscale=1.0f;
	private string stringtarxscale = "1";
	private float targetyscale=1.0f;
	private string stringtaryscale="1";

	private bool popupshowing=false;
	private int popupentry=0;
	private string[] typelist = new string[]{"Bacon", "Bear", "Bird", "boxes", "BreakableWall", "Door", "EndFlag", "Gun", "Parasite", "Spike", "Switch", "Sludge", "Turtle", "Wall", "Water",};
	private int strindex=0;
	public GUIStyle liststyle;
	public Action<int> callbackvar;
	public GUIContent popupdisplay;
	private bool setentry=false;

	private bool doorvertical=false;
	private bool doorpositive=false;
	private bool destroydoor=false;
	private bool holddoor=false;
	private int doornum = 0;
	private string stringdoornum="0";
	private float howfardoor = 0;
	private string stringhowfar="0";

	private float timebetween=0;
	private string stringtimebetween="0";
	private float timedelaystart=0;
	private string stringtimedelaystart="0";
	private float bulletspeed=10;
	private string stringbulletspeed="10";
	private bool shootvertical=true;
	private bool shootpositive=false;
	private bool shootposion = true;
	private bool shootwater=false;

	private int switchnum=0;
	private string switchnumstring="0";

	private bool bonuslevel=false;//remove this for public release
	private bool UserCreatedLevel=false;//set to true for public release
	private int timetobeat=100;
	private string stringtimetobeat="100";

	private string levelname="";
	private string fileName="";
	private bool nameexists = true;
	private bool loading = false;
	private bool loaded=false;
	private bool checknamebool=false;

	private WWW w;
	private WWW w2;
	private WWW wname;
	private WWWForm form;
	private int loadingcounter=0;
	private string[] loadingstring=new string[4];
	private string loadstrg="";

	private bool savingerror=false;
	private string errorstring="";

	private bool repeatbutton1Ready=true;
	private bool repeatbutton2Ready=true;
	private bool repeatbutton3Ready=true;
	private bool repeatbutton4Ready=true;

	// Use this for initialization
	void Start () {
		objectname="Obj"+objectnumber;
		callbackvar = (int x) => popupentry=x;
		popupdisplay = new GUIContent ("");
		Time.timeScale=0.0f;
		editormode=true;
		loadingstring[0]="loading.";
		loadingstring[1]="loading..";
		loadingstring[2]="loading...";
		loadingstring[3]="loading....";
//		rgxfloat=new Regex("[^0-9, .]");
	}
	
	// Update is called once per frame
	void Update () {
//		stringtarx=targetx.ToString();
//		stringtarx = rgxfloat.Replace(stringtarx,"");
//		targetx=System.Convert.ToSingle(stringtarx);
		//Debug.Log(editormode.ToString());
		if(Input.GetMouseButtonUp(0))
		{
			mousedown=false;
			repeatbutton1Ready=true;
			repeatbutton2Ready=true;
			repeatbutton3Ready=true;
			repeatbutton4Ready=true;
		}
		if(mousedown)
		{
			Vector3 mousepos = Input.mousePosition;
			mousepos.z=Camera.main.transform.position.z;
			//Debug.Log(Camera.main.ScreenToWorldPoint(mousepos));
			Vector3 tempposition = Camera.main.ScreenToWorldPoint(mousepos);
			tempposition.x=(tempposition.x*-1.0f)+(Camera.main.transform.position.x*2);
			tempposition.y=(tempposition.y*-1.0f)+(Camera.main.transform.position.y*2);
			tempposition.z=0;
			//Debug.Log(tempposition.ToString());
			target.transform.position=tempposition;
			stringtarxpos=tempposition.x.ToString();
			stringtarypos=tempposition.y.ToString();
			if(typeofobj=="Water")
			{
				Water wet = (Water) target.GetComponent(typeof(Water));
				if(target.transform.localScale.y>1)
				{
					Vector3 scale = new Vector3(target.transform.position.x, target.transform.position.y+(target.transform.localScale.y/2)-0.5f, target.transform.position.z);
					wet.turtlewater.transform.position=scale;
					scale=new Vector3(target.transform.position.x, target.transform.position.y-0.5f, target.transform.position.z);
					wet.deepwater.transform.position=scale;
				}
			}
		}
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100))
			{
				mousedown=true;
				target= hit.collider.gameObject;
				objectname=hit.collider.gameObject.name;
				stringtarxpos=hit.transform.position.x.ToString();
				stringtarypos=hit.transform.position.y.ToString();
				stringtarxscale=hit.transform.localScale.x.ToString();
				stringtaryscale=hit.transform.localScale.y.ToString();
				if(hit.collider.gameObject.tag=="Bacon")
				{
					typeofobj="Bacon";
					popupdisplay = new GUIContent("Bacon");
				}
				else if(hit.collider.gameObject.tag=="bear")
				{
					typeofobj="Bear";
					popupdisplay = new GUIContent("Bear");
				}
				else if(hit.collider.gameObject.tag=="bird")
				{
					typeofobj="Bird";
					popupdisplay = new GUIContent("Bird");
				}
				else if(hit.collider.gameObject.tag=="boxes")
				{
					typeofobj="boxes";
					popupdisplay = new GUIContent("boxes");
				}
				else if(hit.collider.gameObject.tag=="breakable")
				{
					typeofobj="BreakableWall";
					popupdisplay = new GUIContent("BreakableWall");
				}
				else if(hit.collider.gameObject.tag=="boxes")
				{
					typeofobj="boxes";
					popupdisplay = new GUIContent("boxes");
				}
				else if(hit.collider.gameObject.tag=="door")
				{
					typeofobj="Door";
					popupdisplay = new GUIContent("Door");
					DoorMove doorscript = (DoorMove) target.GetComponent(typeof(DoorMove));
					doorvertical=doorscript.Vertical;
					doorpositive=doorscript.positive;
					destroydoor=doorscript.destroydoor;
					holddoor=doorscript.holddoor;
					stringdoornum=doorscript.numberofdoor.ToString();
					stringhowfar=doorscript.howfar.ToString();
				}
				else if(hit.collider.gameObject.tag=="end")
				{
					typeofobj="EndFlag";
					popupdisplay = new GUIContent("EndFlag");
					EndLevel end=(EndLevel) target.GetComponent(typeof(EndLevel));
					bonuslevel = end.BonusLevel;
					UserCreatedLevel=end.UserCreatedLevel;
					stringtimetobeat=end.TimetoBeat.ToString();
				}
				else if(hit.collider.gameObject.tag=="gun")
				{
					typeofobj="Gun";
					popupdisplay = new GUIContent("Gun");
					Gunscript cannon = (Gunscript) target.GetComponent(typeof(Gunscript));
					stringtimebetween=cannon.timetowait.ToString();
					stringtimedelaystart=cannon.startDelay.ToString();
					stringbulletspeed=cannon.bulletspeed.ToString();
					shootvertical= cannon.shootvertical;
					shootpositive=cannon.shootpositive;
					shootposion=cannon.posion;
					shootwater=cannon.water;
				}
				else if(hit.collider.gameObject.tag=="parasite")
				{
					typeofobj="Parasite";
					popupdisplay = new GUIContent("Parasite");
				}
				else if(hit.collider.gameObject.tag=="sludge")
				{
					typeofobj="Sludge";
					popupdisplay = new GUIContent("Sludge");
				}
				else if(hit.collider.gameObject.tag=="Spike")
				{
					typeofobj="Spike";
					popupdisplay = new GUIContent("Spike");
				}
				else if(hit.collider.gameObject.tag=="switch")
				{
					typeofobj="Switch";
					popupdisplay = new GUIContent("Switch");
					Switch switchscript = (Switch) target.GetComponent(typeof(Switch));
					switchnumstring=switchscript.switchnumber.ToString();
				}
				else if(hit.collider.gameObject.tag=="turtle")
				{
					typeofobj="Turtle";
					popupdisplay = new GUIContent("Turtle");
				}
				else if(hit.collider.gameObject.tag=="water")
				{
					typeofobj="Water";
					popupdisplay = new GUIContent("Water");
				}
				else
				{
					typeofobj="Wall";
					popupdisplay = new GUIContent("Wall");
				}
			}
		}
		if(checknamebool)
		{
			if(wname.progress>=1)
			{
				//print (nameexists.ToString());
				if(!nameexists)
				{
					StartCoroutine(UploadLevel(w));
					loading=true;
					StopCoroutine("checkname");
					checknamebool=false;
				}
			}
		}
		if(loading)
		{
			if(w.progress<1)
			{
				loadstrg=loadingstring[loadingcounter];
				loadingcounter++;
				if(loadingcounter>3)
					loadingcounter=0;
			}
			else 
			{
				loading=false;
				loadingcounter=0;
				loaded =true;
				StopCoroutine("UploadLevel");
			}
		}
		Time.timeScale=0.0f;
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width*0.01f, Screen.height*0.01f, Screen.width*0.1f, Screen.height*0.055f), new GUIContent("Main Menu")))
		{
			Application.LoadLevel("MainMenu");
		}

		char chr=Event.current.character;
		GUI.Box(new Rect(Screen.width*0.75f, 0,Screen.width*0.25f, Screen.height), "Editor Menu");

		GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.045f, Screen.width*0.15f, Screen.height*0.055f), "Object Name:");
		GUI.SetNextControlName("ObjName");
		Event.current.character=LimitToAlphaNumeric(chr, "ObjName");
		objectname=GUI.TextField(new Rect(Screen.width*0.85f, Screen.height*0.05f, Screen.width*0.145f, Screen.height*0.05f), objectname, 32);

		GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.105f, Screen.width*0.15f, Screen.height*0.055f), "Object Type:");
		if (GUIEXT.Dropdown (new Rect (Screen.width * 0.85f, Screen.height * 0.11f, Screen.width * 0.145f, Screen.height * 0.05f), popupshowing, popupdisplay, typelist, callbackvar)) 
		{
			popupshowing = true;
			if(!setentry)
				setentry=true;
		} 
		else 
		{
			popupshowing = false;
			if(setentry)
			{
				popupdisplay=new GUIContent(typelist[popupentry]);
				typeofobj = typelist[popupentry];
				stringtarxpos="0";
				stringtarxscale="1";
				stringtarypos="0";
				stringtaryscale="1";
				//Debug.Log("here");
				setentry=false;
			}
		}

		GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.165f, Screen.width*0.075f, Screen.height*0.055f), "Postion");
		GUI.Label(new Rect(Screen.width*0.83f, Screen.height*0.165f, Screen.width*0.03f, Screen.height*0.055f), "X:");
		GUI.SetNextControlName("ObjXPos");
		Event.current.character=LimitToFloat(chr, "ObjXPos");
		stringtarxpos=GUI.TextField(new Rect(Screen.width*0.85f, Screen.height*0.17f, Screen.width*0.05f, Screen.height*0.05f), stringtarxpos, 10);
		GUI.Label(new Rect(Screen.width*0.925f, Screen.height*0.165f, Screen.width*0.03f, Screen.height*0.055f), "Y:");
		GUI.SetNextControlName("ObjYPos");
		Event.current.character=LimitToFloat(chr, "ObjYPos");
		stringtarypos=GUI.TextField(new Rect(Screen.width*0.945f, Screen.height*0.17f, Screen.width*0.05f, Screen.height*0.05f), stringtarypos, 10);

		GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.225f, Screen.width*0.075f, Screen.height*0.055f), "Scale");
		GUI.Label(new Rect(Screen.width*0.83f, Screen.height*0.225f, Screen.width*0.03f, Screen.height*0.055f), "X:");
		GUI.SetNextControlName("ObjXScale");
		Event.current.character=LimitToFloatNoNeg(chr, "ObjXScale");
		stringtarxscale=GUI.TextField(new Rect(Screen.width*0.85f, Screen.height*0.23f, Screen.width*0.05f, Screen.height*0.05f), stringtarxscale, 10);
		GUI.Label(new Rect(Screen.width*0.925f, Screen.height*0.225f, Screen.width*0.03f, Screen.height*0.055f), "Y:");
		GUI.SetNextControlName("ObjYScale");
		Event.current.character=LimitToFloatNoNeg(chr, "ObjYScale");
		stringtaryscale=GUI.TextField(new Rect(Screen.width*0.945f, Screen.height*0.23f, Screen.width*0.05f, Screen.height*0.05f), stringtaryscale, 10);

		if(typeofobj=="Switch")
		{
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.285f, Screen.width*0.15f, Screen.height*0.055f), "Switch Number:");
			GUI.SetNextControlName("SwitchNum");
			Event.current.character=LimitToInt(chr, "SwitchNum");
			switchnumstring=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.29f, Screen.width*0.05f, Screen.height*0.05f), switchnumstring, 10);
		}
		else if(typeofobj=="Door")
		{
			doorvertical=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.29f, Screen.width*0.2f, Screen.height*0.05f), doorvertical, "Move in Verticle Direction");
			doorpositive=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.35f, Screen.width*0.2f, Screen.height*0.05f), doorpositive, "Move in Positive Direction");
			destroydoor=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.41f, Screen.width*0.2f, Screen.height*0.05f), destroydoor, "Destroy the door");
			holddoor=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.47f, Screen.width*0.2f, Screen.height*0.05f), holddoor, "Have to hold door up");
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.525f, Screen.width*0.15f, Screen.height*0.055f), "Door Number:");
			GUI.SetNextControlName("DoorNum");
			Event.current.character=LimitToInt(chr, "DoorhNum");
			stringdoornum=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.53f, Screen.width*0.05f, Screen.height*0.05f), stringdoornum, 10);
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.585f, Screen.width*0.15f, Screen.height*0.055f), "How Far Too Move:");
			GUI.SetNextControlName("HowFar");
			Event.current.character=LimitToInt(chr, "HowFar");
			stringhowfar=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.59f, Screen.width*0.05f, Screen.height*0.05f), stringhowfar, 10);
		}
		else if(typeofobj=="Gun")
		{
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.285f, Screen.width*0.15f, Screen.height*0.055f), "Millisecond between Shots fired:");
			GUI.SetNextControlName("timetowait");
			Event.current.character=LimitToFloat(chr, "timetowait");
			stringtimebetween=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.29f, Screen.width*0.05f, Screen.height*0.05f), stringtimebetween, 10);
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.345f, Screen.width*0.15f, Screen.height*0.055f), "Millisecond before start shooting:");
			GUI.SetNextControlName("startdelay");
			Event.current.character=LimitToFloat(chr, "startdelay");
			stringtimedelaystart=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.35f, Screen.width*0.05f, Screen.height*0.05f), stringtimedelaystart, 10);
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.405f, Screen.width*0.15f, Screen.height*0.055f), "Speed of the Bullets:");
			GUI.SetNextControlName("bulletspeed");
			Event.current.character=LimitToFloat(chr, "bulletspeed");
			stringbulletspeed=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.41f, Screen.width*0.05f, Screen.height*0.05f), stringbulletspeed, 10);
			shootvertical=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.47f, Screen.width*0.2f, Screen.height*0.05f), shootvertical, "Shoot in Verticle Direction");
			shootpositive=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.53f, Screen.width*0.2f, Screen.height*0.05f), shootpositive, "Shoot in Positive Direction");
			if(GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.59f, Screen.width*0.2f, Screen.height*0.05f), shootposion, "Shoot Posion Bullets"))
			{
				shootposion=true;
				shootwater=false;
			}
			else
			{
				shootposion=false;
				shootwater=true;
			}
			if(GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.65f, Screen.width*0.2f, Screen.height*0.05f), shootwater, "Shoot Water Bullets"))
			{
				shootposion=false;
				shootwater=true;
			}
			else
			{
				shootposion=true;
				shootwater=false;
			}
		}
		else if(typeofobj=="EndFlag")
		{
			bonuslevel=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.29f, Screen.width*0.2f, Screen.height*0.05f), bonuslevel, "Bonus Level");
			UserCreatedLevel=GUI.Toggle(new Rect(Screen.width*0.8f, Screen.height*0.35f, Screen.width*0.2f, Screen.height*0.05f), UserCreatedLevel, "User Created Level");
			GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.405f, Screen.width*0.15f, Screen.height*0.055f), "Seconds to Beat Level:");
			GUI.SetNextControlName("leveltime");
			Event.current.character=LimitToInt(chr, "leveltime");
			stringtimetobeat=GUI.TextField(new Rect(Screen.width*0.88f, Screen.height*0.41f, Screen.width*0.05f, Screen.height*0.05f), stringtimetobeat, 10);
		}

		if(GUI.Button(new Rect(Screen.width*0.76f, Screen.height*0.84f, Screen.width*0.075f, Screen.height*0.055f), new GUIContent("Spawn")))
		{
			Vector3 spawnposition = Vector3.zero;
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
				target = (GameObject)Instantiate(Resources.Load(typeofobj), spawnposition, Quaternion.identity);
			}
			targetxpos = float.Parse(stringtarxpos);
			targetypos = float.Parse(stringtarypos);
			targetxscale = float.Parse(stringtarxscale);
			targetyscale = float.Parse(stringtaryscale);
			target.transform.position=new Vector3(targetxpos,targetypos, 0);
			target.transform.localScale = new Vector3(targetxscale, targetyscale, 1);
			Checkname("New");
			target.name= objectname;
			Checkname();
			if(typeofobj=="Switch")
			{
				switchnum= Int32.Parse(switchnumstring);
				Switch switchscript = (Switch) target.GetComponent(typeof(Switch));
				switchscript.switchnumber= switchnum;
			}
			else if(typeofobj=="Door")
			{
				doornum=Int32.Parse(stringdoornum);
				howfardoor=Int32.Parse(stringhowfar);
				DoorMove doorscript = (DoorMove) target.GetComponent(typeof(DoorMove));
				doorscript.Vertical=doorvertical;
				doorscript.positive=doorpositive;
				doorscript.destroydoor=destroydoor;
				doorscript.holddoor=holddoor;
				doorscript.numberofdoor=doornum;
				doorscript.howfar=howfardoor;
			}
			else if(typeofobj=="Gun")
			{
				timebetween=float.Parse(stringtimebetween);
				timedelaystart=float.Parse(stringtimedelaystart);
				bulletspeed=float.Parse(stringbulletspeed);
				Gunscript cannon = (Gunscript) target.GetComponent(typeof(Gunscript));
				cannon.timetowait=timebetween;
				cannon.startDelay=timedelaystart;
				cannon.bulletspeed=bulletspeed;
				cannon.shootvertical=shootvertical;
				cannon.shootpositive=shootpositive;
				cannon.posion=shootposion;
				cannon.water=shootwater;
			}
			else if(typeofobj=="EndFlag")
			{
				timetobeat=Int32.Parse(stringtimetobeat);
				EndLevel end = (EndLevel) target.GetComponent(typeof(EndLevel));
				end.BonusLevel=bonuslevel;
				end.UserCreatedLevel = UserCreatedLevel;
				end.TimetoBeat=timetobeat;
			}
			else if(typeofobj=="Water")
			{
				if(target.transform.localScale.y>1)
				{
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
		if(GUI.Button(new Rect(Screen.width*0.835f, Screen.height*0.84f, Screen.width*0.075f, Screen.height*0.055f), new GUIContent("Edit")))
		{
			Vector3 spawnposition = Vector3.zero;
			targetxpos = float.Parse(stringtarxpos);
			targetypos = float.Parse(stringtarypos);
			targetxscale = float.Parse(stringtarxscale);
			targetyscale = float.Parse(stringtaryscale);
			target.transform.position=new Vector3(targetxpos,targetypos, 0);
			target.transform.localScale = new Vector3(targetxscale, targetyscale, 1);
			Checkname("Edit");
			target.name= objectname;
			Checkname();
			if(typeofobj=="Switch")
			{
				switchnum= Int32.Parse(switchnumstring);
				Switch switchscript = (Switch) target.GetComponent(typeof(Switch));
				switchscript.switchnumber= switchnum;
			}
			else if(typeofobj=="Door")
			{
				doornum=Int32.Parse(stringdoornum);
				howfardoor=Int32.Parse(stringhowfar);
				DoorMove doorscript = (DoorMove) target.GetComponent(typeof(DoorMove));
				doorscript.Vertical=doorvertical;
				doorscript.positive=doorpositive;
				doorscript.destroydoor=destroydoor;
				doorscript.holddoor=holddoor;
				doorscript.numberofdoor=doornum;
				doorscript.howfar=howfardoor;
			}
			else if(typeofobj=="Gun")
			{
				timebetween=float.Parse(stringtimebetween);
				timedelaystart=float.Parse(stringtimedelaystart);
				bulletspeed=float.Parse(stringbulletspeed);
				Gunscript cannon = (Gunscript) target.GetComponent(typeof(Gunscript));
				cannon.timetowait=timebetween;
				cannon.startDelay=timedelaystart;
				cannon.bulletspeed=bulletspeed;
				cannon.shootvertical=shootvertical;
				cannon.shootpositive=shootpositive;
				cannon.posion=shootposion;
				cannon.water=shootwater;
			}			
			else if(typeofobj=="EndFlag")
			{
				timetobeat=Int32.Parse(stringtimetobeat);
				EndLevel end = (EndLevel) target.GetComponent(typeof(EndLevel));
				end.BonusLevel=bonuslevel;
				end.UserCreatedLevel = UserCreatedLevel;
				end.TimetoBeat=timetobeat;
			}
			else if(typeofobj=="Water")
			{
				Water wet = (Water) target.GetComponent(typeof(Water));
				if(target.transform.localScale.y>1)
				{
					if(wet.mainwater==null)
					{
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
					else
					{
						Vector3 scale = new Vector3(target.transform.position.x, target.transform.position.y+(target.transform.localScale.y/2)-0.5f, target.transform.position.z);
						wet.turtlewater.transform.position=scale;
						scale = new Vector3(target.transform.localScale.x, 1, target.transform.localScale.z*0.95f);
						wet.turtlewater.transform.localScale=scale;
						scale=new Vector3(target.transform.position.x, target.transform.position.y-0.5f, target.transform.position.z);
						wet.deepwater.transform.position=scale;
						scale = new Vector3(target.transform.localScale.x, target.transform.localScale.y-1, target.transform.localScale.z*0.95f);
						wet.deepwater.transform.localScale=scale;
					}
				}
				else
				{
					if(wet.turtlewater!=null)
					{
						Destroy(wet.turtlewater);
						wet.turtlewater=null;
					}
					if(wet.deepwater!=null)
					{
						Destroy(wet.deepwater);
						wet.deepwater=null;
					}
				}
			}
		}
		if(GUI.Button(new Rect(Screen.width*0.92f, Screen.height*0.84f, Screen.width*0.075f, Screen.height*0.055f), new GUIContent("Delete")))
		{
			if(typeofobj=="Water")
			{
				Water wet = (Water) target.GetComponent(typeof(Water));
				if(wet.turtlewater!=null)
					Destroy(wet.turtlewater);
				if(wet.deepwater!=null)
					Destroy(wet.deepwater);
			}
			Destroy(target);
			objectname="Obj"+objectnumber;
			stringtarxpos="0";
			stringtarypos="0";
			stringtarxscale="1";
			stringtaryscale="1";
		}

		GUI.Label(new Rect(Screen.width*0.76f, Screen.height*0.895f, Screen.width*0.15f, Screen.height*0.055f), "Level Name:");
		GUI.SetNextControlName("LvlName");
		Event.current.character=LimitToAlphaNumeric(chr, "LvlName");
		levelname=GUI.TextField(new Rect(Screen.width*0.9f, Screen.height*0.9f, Screen.width*0.145f, Screen.height*0.05f), levelname, 32);

		if(GUI.Button(new Rect(Screen.width*0.76f, Screen.height*0.96f, Screen.width*0.1f, Screen.height*0.04f), new GUIContent("Save")))
		{
			List<GameType> gtList = new List<GameType>();
			string objtype="";
			XmlSerializer ser = new XmlSerializer(typeof(List<GameType>));
			//add check for level of the same name. <<<<<<<<<<<<===========================================================
			//TextWriter writer = new XmlTextWriter();
			bool endflagthere=false;
			bool parathere=false;
			bool bearthere=false;
			bool turtlethere=false;
			bool birdthere=false;
			GameObject[] allobjects = GameObject.FindObjectsOfType<GameObject>();
			for(int i=0; i<allobjects.Length; i++)
			{
				if(allobjects[i].tag!="MainCamera" && allobjects[i].tag!="light" && allobjects[i].tag!="DeepWater" && allobjects[i].tag!="turtlewater")
				{
					if(allobjects[i].tag=="Bacon")
					{
						objtype="Bacon";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="bear")
					{
						bearthere=true;
						objtype="Bear";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="bird")
					{
						birdthere=true;
						objtype="Bird";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="boxes")
					{
						objtype="boxes";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="breakable")
					{
						objtype="BreakableWall";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="boxes")
					{
						objtype="boxes";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="door")
					{
						objtype="Door";
						DoorMove door = (DoorMove) allobjects[i].GetComponent(typeof(DoorMove));
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale, door.numberofdoor, door.howfar, door.Vertical, door.positive, door.destroydoor, door.holddoor);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="end")
					{
						endflagthere=true;
						objtype="EndFlag";
						EndLevel end = (EndLevel) allobjects[i].GetComponent(typeof(EndLevel));
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale, end.BonusLevel, end.UserCreatedLevel, end.TimetoBeat);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="gun")
					{
						objtype="Gun";
						Gunscript gun = (Gunscript) allobjects[i].GetComponent(typeof(Gunscript));
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale, gun.timetowait, gun.startDelay, gun.bulletspeed, gun.shootvertical, gun.shootpositive, gun.posion, gun.water);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="parasite")
					{
						parathere=true;
						objtype="Parasite";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="sludge")
					{
						objtype="Sludge";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="Spike")
					{
						objtype="Spike";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="switch")
					{
						objtype="Switch";
						Switch sch = (Switch) allobjects[i].GetComponent(typeof(Switch));
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale, (float)sch.switchnumber);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="turtle")
					{
						turtlethere=true;
						objtype="Turtle";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag=="water")
					{
						objtype="Water";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
					else if(allobjects[i].tag!="bar")
					{
						objtype="Wall";
						GameType newobj = new GameType(objtype, allobjects[i].name, allobjects[i].transform.position, allobjects[i].transform.localScale);
						gtList.Add(newobj);
					}
				}
			}
			if(bearthere)
			{
				if(birdthere)
				{
					if(endflagthere)
					{
						if(parathere)
						{
							if(turtlethere)
							{
								wname = new WWW(ipmain + "getlevel.php?name="+levelname);
								Debug.Log("starting name check");
								StartCoroutine(checkname(wname));
								checknamebool=true;
								//print(wname.text);
								StringWriter strwrite = new StringWriter();
								ser.Serialize(strwrite, gtList);
								//writer.Close();
								//XmlDocument map = new XmlDocument();
								//map.LoadXml();
								fileName=levelname+".xml";
								//print (strwrite.ToString());
								//byte[] leveldata = Encoding.UTF8.GetBytes(strwrite.ToString());
								form = new WWWForm();
								form.AddField("action", "level upload");
								form.AddField("file", "file");
								form.AddField("levelname", levelname);
								form.AddField("creator", "erickm");
								form.AddField("filename", fileName);
								form.AddField("filetext", strwrite.ToString());
								//form.AddBinaryData("file", leveldata, fileName, "text/xml");
								w = new WWW(ipmain + "levelupload.php",form);
							}
							else
							{
								savingerror=true;
								errorstring="Turtle not found in the level. \nPlease add a turtle to your level.";
							}
						}
						else
						{
							savingerror=true;
							errorstring="Dude there is no parasite in your level. \nWho are you suppose to play as. Please add a parasite to your level.";
						}
					}
					else
					{
						savingerror=true;
						errorstring="How you suppose to beat the level if there is no end. \nPlease add an end flag to your level.";
					}
				}
				else
				{
					savingerror=true;
					errorstring="I know everyone likes to kill bird, but it has to be in your level to kill it. \nPlease add a bird to your level.";
				}
			}
			else
			{
				savingerror=true;
				errorstring="Excuse me Good Sir you seem you have forgot to put me, \nBeary Manalow, into your level. Would you please fix that.";
			}
			//if(!nameexists)
			//StartCoroutine(UploadLevel(w));
			//loading=true;
			//Debug.Log("File Created");
		}
		if(loading||checknamebool)
		{
			GUI.Box(new Rect(Screen.width*0.25f, Screen.height*0.25f,Screen.width*0.5f, Screen.height*0.5f), "Progress");
			GUI.Label(new Rect(Screen.width*0.4f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.2f), loadstrg);
		}
		if(loaded)
		{
			GUI.Window(0, new Rect(Screen.width*0.4f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.2f), Windowbutton, "Level Saved.");
		}
		if(nameexists&&checknamebool)
		{
			GUI.Window(0, new Rect(Screen.width*0.4f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.2f), Windowbutton, "A Level by that name already exists. Please pick another.");
		}
		if(savingerror)
		{
			GUI.Window(0, new Rect(Screen.width*0.25f, Screen.height*0.4f, Screen.width*0.5f, Screen.height*0.2f), Windowbutton, errorstring);
		}

		if(GUI.RepeatButton(new Rect(Screen.width*0.0025f, Screen.height*0.9f, Screen.width*0.045f, Screen.height*0.075f), new GUIContent("Left")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x-1, camera.transform.position.y, camera.transform.position.z);
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.0475f, Screen.height*0.9f, Screen.width*0.05f, Screen.height*0.075f), new GUIContent("Down")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y-1, camera.transform.position.z);
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.0475f, Screen.height*0.82f, Screen.width*0.05f, Screen.height*0.075f), new GUIContent("Up")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y+1, camera.transform.position.z);
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.0975f, Screen.height*0.9f, Screen.width*0.0475f, Screen.height*0.075f), new GUIContent("Right")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x+1, camera.transform.position.y, camera.transform.position.z);
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.7f, Screen.height*0.82f, Screen.width*0.04f, Screen.height*0.075f), new GUIContent("In")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z+1);
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.7f, Screen.height*0.9f, Screen.width*0.04f, Screen.height*0.075f), new GUIContent("Out")))
		{
			GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
			camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z-1);
		}
		GUI.Box(new Rect(Screen.width*0.1475f, Screen.height*0.8f, Screen.width*0.55f, Screen.height*0.19f), "Objects");
		if(GUI.Button(new Rect(Screen.width*0.15f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent("left")))
		{
			if(strindex>0)
			{
				strindex--;
			}
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.245f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent(typelist[strindex])))
		{
			if(repeatbutton1Ready)
			{
				Vector3 mousepos = Input.mousePosition;
				mousepos.z=-25;
				//Debug.Log(Camera.main.ScreenToWorldPoint(mousepos));
				Vector3 tempposition = Camera.main.ScreenToWorldPoint(mousepos);
				tempposition.x=tempposition.x*-1.0f;
				tempposition.y=tempposition.y*-1.0f;
				tempposition.z=0;
				typeofobj=typelist[strindex];
				dragspawn(tempposition);
				repeatbutton1Ready=false;
			}
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.335f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent(typelist[strindex+1])))
		{
			if(repeatbutton2Ready)
			{
				Vector3 mousepos = Input.mousePosition;
				mousepos.z=-25;
				//Debug.Log(Camera.main.ScreenToWorldPoint(mousepos));
				Vector3 tempposition = Camera.main.ScreenToWorldPoint(mousepos);
				tempposition.x=tempposition.x*-1.0f;
				tempposition.y=tempposition.y*-1.0f;
				tempposition.z=0;
				typeofobj=typelist[strindex+1];
				dragspawn(tempposition);
				repeatbutton2Ready=false;
			}
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.425f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent(typelist[strindex+2])))
		{
			if(repeatbutton3Ready)
			{
				Vector3 mousepos = Input.mousePosition;
				mousepos.z=-25;
				//Debug.Log(Camera.main.ScreenToWorldPoint(mousepos));
				Vector3 tempposition = Camera.main.ScreenToWorldPoint(mousepos);
				tempposition.x=tempposition.x*-1.0f;
				tempposition.y=tempposition.y*-1.0f;
				tempposition.z=0;
				typeofobj=typelist[strindex+2];
				dragspawn(tempposition);
				repeatbutton3Ready=false;
			}
		}
		if(GUI.RepeatButton(new Rect(Screen.width*0.515f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent(typelist[strindex+3])))
		{
			if(repeatbutton4Ready)
			{
				Vector3 mousepos = Input.mousePosition;
				mousepos.z=-25;
				//Debug.Log(Camera.main.ScreenToWorldPoint(mousepos));
				Vector3 tempposition = Camera.main.ScreenToWorldPoint(mousepos);
				tempposition.x=tempposition.x*-1.0f;
				tempposition.y=tempposition.y*-1.0f;
				tempposition.z=0;
				typeofobj=typelist[strindex+3];
				dragspawn(tempposition);
				repeatbutton4Ready=false;
			}
		}
		if(GUI.Button(new Rect(Screen.width*0.615f, Screen.height*0.85f, Screen.width*0.08f, Screen.height*0.14f), new GUIContent("right")))
		{
			if(strindex<11)
			{
				strindex++;
			}
		}
	}

	char LimitToAlphaNumeric(char chr, string ControlName)
	{
		if((chr<'0' || chr > '9') && (chr<'a' || chr>'z') &&  (chr<'A' || chr>'Z')&& GUI.GetNameOfFocusedControl()==ControlName) //close but need to fix letters and . probably need to only call if this textbox has the cursor 
			return('\0');
		return(Event.current.character);
	}

	char LimitToFloat(char chr, string ControlName)
	{
		if((chr<'0' || chr > '9') && chr!='.' && chr!='-' && GUI.GetNameOfFocusedControl()==ControlName)
			return('\0');
		return(Event.current.character);
	}

	char LimitToFloatNoNeg(char chr, string ControlName)
	{
		if((chr<'0' || chr > '9') && chr!='.' && GUI.GetNameOfFocusedControl()==ControlName)
			return('\0');
		return(Event.current.character);
	}

	char LimitToInt(char chr, string ControlName)
	{
		if((chr<'0' || chr > '9') && GUI.GetNameOfFocusedControl()==ControlName)
			return('\0');
		return(Event.current.character);
	}

	void Checkname()
	{
		if(objectname=="Obj"+objectnumber)
		{
			objectnumber++;
			objectname="Obj"+objectnumber;
		}
	}
	void Checkname(String button)
	{
		GameObject[] allobjects = (GameObject[]) FindObjectsOfType(typeof(GameObject));
		for(int i=0; i<allobjects.Length; i++)
		{
			if(button=="New")
			{
				if(objectname == allobjects[i].name)
				{
					objectname = "Obj"+objectnumber;
				}
			}
			else if(button=="Edit")
			{
				if(objectname == allobjects[i].name && target!= allobjects[i])
				{
					objectname = "Obj"+objectnumber;
				}
			}
		}
	}
	void Windowbutton(int windowID)
	{
		if(GUILayout.Button("OK"))
		{
			if(savingerror)
			{
				savingerror=false;
				errorstring="";
			}
			else
			{
				loaded=false;
				nameexists=true;
				loading=false;
				checknamebool=false;
			}
		}
	}

	IEnumerator UploadLevel(WWW w)
	{
		yield return w;
		Debug.Log("after yield wul");
		Debug.Log(w.text.ToString());
		if (w.error != null)
		{
			Debug.Log("error");
			Debug.Log( w.error );    
		}
		else if(w.text.ToString()=="Error: Unable to open or create file!")
		{
			Debug.Log(w.text.ToString());
		}
		else
		{
			Debug.Log(w.text.ToString());
			Debug.Log("File Created starting checkfile");
			StartCoroutine(checkfile(w));
		}
	}

	IEnumerator checkfile(WWW w)
	{
		Debug.Log("upload progress: "+w.uploadProgress.ToString());
		Debug.Log("w.isDone = " + w.isDone.ToString());
		//this part validates the upload, by waiting 5 seconds then trying to retrieve it from the web
		if(w.uploadProgress == 1 && w.isDone)
		{
			yield return new WaitForSeconds(5);
			//change the url to the url of the folder you want it the levels to be stored, the one you specified in the php file
			w2 = new WWW(ipmain +"BBT/CustomLevels/" + fileName+".xml");
			Debug.Log("checking file...");
			yield return w2;
			Debug.Log("after yield w2cf");
			if(w2.error != null)
			{
				Debug.Log("error 2");
				Debug.Log( w2.error );  
			}
			else
			{
				//then if the retrieval was successful, validate its content to ensure the level file integrity is intact
				if(w2.text != null && w2.text != "")
				{
					//and finally announce that everything went well
					Debug.Log( "Level File " + fileName + " Contents are: \n\n" + w2.text);
					Debug.Log( "Finished Uploading Level " + fileName);
				}
				else
				{
					Debug.Log( "Level File " + fileName + " is Empty");
				}
			}
		} 
	}
	IEnumerator checkname(WWW ww)
	{
		yield return ww;
		if(ww.progress>=1)
		{
			//print(ww.text.TrimEnd());
			//print("Level by that name does not exist ");
			if(ww.text.TrimEnd()=="Level by that name does not exist")
			{
				nameexists=false;
			}
			else
			{
				nameexists=true;
			}
			//print(nameexists.ToString());
		}
	}
	void dragspawn(Vector3 spawnposition)
	{
		popupdisplay = new GUIContent(typeofobj);
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
			target = (GameObject)Instantiate(Resources.Load(typeofobj), spawnposition, Quaternion.identity);
		}
		Checkname("New");
		target.name= objectname;
		Checkname();
		if(typeofobj=="Switch")
		{
			switchnum= 0;
			Switch switchscript = (Switch) target.GetComponent(typeof(Switch));
			switchscript.switchnumber= switchnum;
		}
		else if(typeofobj=="Door")
		{
			doornum=0;
			howfardoor=0;
			doorvertical=false;
			doorpositive=false;
			destroydoor=false;
			holddoor=false;
			DoorMove doorscript = (DoorMove) target.GetComponent(typeof(DoorMove));
			doorscript.Vertical=doorvertical;
			doorscript.positive=doorpositive;
			doorscript.destroydoor=destroydoor;
			doorscript.holddoor=holddoor;
			doorscript.numberofdoor=doornum;
			doorscript.howfar=howfardoor;
		}
		else if(typeofobj=="Gun")
		{
			timebetween=0;
			timedelaystart=0;
			bulletspeed=1;
			shootvertical=false;
			shootpositive=false;
			shootposion=true;
			shootwater=false;
			Gunscript cannon = (Gunscript) target.GetComponent(typeof(Gunscript));
			cannon.timetowait=timebetween;
			cannon.startDelay=timedelaystart;
			cannon.bulletspeed=bulletspeed;
			cannon.shootvertical=shootvertical;
			cannon.shootpositive=shootpositive;
			cannon.posion=shootposion;
			cannon.water=shootwater;
		}
		else if(typeofobj=="EndFlag")
		{
			timetobeat=100;
			bonuslevel=false;
			UserCreatedLevel=true;
			EndLevel end = (EndLevel) target.GetComponent(typeof(EndLevel));
			end.BonusLevel=bonuslevel;
			end.UserCreatedLevel = UserCreatedLevel;
			end.TimetoBeat=timetobeat;
		}
		else if(typeofobj=="Water")
		{
			if(target.transform.localScale.y>1)
			{
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
		mousedown=true;
	}
}
