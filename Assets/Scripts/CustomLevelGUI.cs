using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public struct CustomLevel
{
	public string Name;
	public string location;
	public int totalvotes;
	public int totalvoters;
	public int uncompletevotes;
	public string creator;
}

public class CustomLevelGUI : MenuButtonsScript {
	public WWW w;
	public string gotoSceneIndex="";
	public bool loadCustom=false;
	private bool listdownloaded=false;
	private List<CustomLevel> CustomLevels = new List<CustomLevel>();
	private ArrayList templist = new ArrayList();
	private ArrayList tempstrings = new ArrayList();
	public Vector2 scrollPostion = Vector2.zero;
	public int buttonheight=50;
	public bool stringsplit=false;

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			resetcustom();
		}
	}

	void OnGUI()
	{
		if (listdownloaded) 
		{
			StopCoroutine("getlevels");
			//GUI.Box(new Rect(Screen.width*0.1f,Screen.height*0.1f, Screen.width*0.8f, Screen.height*0.8f), "Custom Levels");
			GUI.Window(1, new Rect(Screen.width*0.05f,Screen.height*0.1f, Screen.width*0.9f, Screen.height*0.8f), customlevelbuttonlist, "Custom Levels");
			//Debug.Log(CustomLevels.Capacity.ToString());
			//Debug.Log(CustomLevels.Count.ToString());
		}
		else if(loadCustom)
		{
			GUI.Box(new Rect(Screen.width*0.25f, Screen.height*0.25f,Screen.width*0.5f, Screen.height*0.5f), "Progress");
			w = new WWW(ipmain + "getlevel.php?name=");
			StartCoroutine(getlevels(w));
			GUI.Label(new Rect(Screen.width*0.4f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.2f), "LOADING...");
			loadCustom=true;
		}

	}

	void customlevelbuttonlist(int windowID)
	{
		float count=0;
		scrollPostion=GUILayout.BeginScrollView(scrollPostion, false, true, GUILayout.Width(Screen.width*0.85f), GUILayout.Height(Screen.height*0.65f));
		foreach(CustomLevel level in CustomLevels)
		{
			if(GUILayout.Button(level.Name))
			{
				Commons.levelLocation=CustomLevels[(int)count].location;
				updatepercent();
				UpDateAchievements();
				Application.LoadLevel(gotoSceneIndex);
				Debug.Log(CustomLevels[(int)count].location.ToString());
				Debug.Log(SceneIndex);
			}
			count++;
		}
		GUILayout.EndScrollView();
		if(GUI.Button(new Rect(Screen.width*0.35f, Screen.height*0.725f, Screen.width*0.2f, Screen.height*0.05f), new GUIContent("Cancel")))
		{
			resetcustom();
		}
		//GUI.DragWindow();
	}
	
	IEnumerator getlevels(WWW w)
	{
		yield return w;
		if (w.progress >= 1&&!stringsplit) 
		{
			templist.AddRange(Regex.Split(w.text, "==>"));
			foreach(string temp in templist)
			{
				if(temp!="")
				{
					tempstrings.Clear();
					tempstrings.AddRange(Regex.Split(temp, "->"));
					CustomLevel templevel = new CustomLevel();
					//Debug.Log(tempstrings[0].ToString());
					//Debug.Log(tempstrings[1].ToString());
					templevel.Name =tempstrings[0].ToString();
	  				templevel.location = tempstrings[1].ToString();
					templevel.totalvotes = Convert.ToInt32(tempstrings[2]);
					templevel.totalvoters = Convert.ToInt32(tempstrings[3]);
					templevel.creator = tempstrings[4].ToString();
					templevel.uncompletevotes = Convert.ToInt32(tempstrings[5]);
					CustomLevels.Add(templevel);
				}
			}
			templist.Clear();
			listdownloaded=true;
			loadCustom=false;
			stringsplit=true;
		}
		//Debug.Log (w.text.ToString());
		//Debug.Log (w.progress.ToString());
	}

	void resetcustom()
	{
		CustomLevels.Clear();
		//Debug.Log(CustomLevels.Count.ToString());
		listdownloaded=false;
		loadCustom=false;
		stringsplit=false;
	}
}
