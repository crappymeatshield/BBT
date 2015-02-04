using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuButtonsScript : achievements {
	public bool GoToScene=true;
	public string SceneIndex="";
	public bool Keybindings=false;
	public bool resolutionbtn=false;
	public bool QuitButton=false;
	public bool AchieveButton=false;
	public bool PlayCustom=false;
	
	void OnMouseDown()
	{
		if(GoToScene)
		{
			if(PlayCustom)
			{
				GameObject camera = null;
				camera=GameObject.FindGameObjectWithTag("MainCamera");
				CustomLevelGUI control = (CustomLevelGUI) camera.gameObject.GetComponent(typeof(CustomLevelGUI));
				control.loadCustom=true;
				control.gotoSceneIndex=SceneIndex;
			}
			else if(!QuitButton)
			{
				updatepercent();
				UpDateAchievements();
				Application.LoadLevel(SceneIndex);
			}
		}
		else if(Keybindings)
		{
			GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
			Options camgui = (Options) cam.GetComponent(typeof(Options));
			camgui.Keybindingset=true;
		}
		else if(resolutionbtn)
		{
			GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
			Options camgui = (Options) cam.GetComponent(typeof(Options));
			camgui.Resolutionset=true;
		}
		else if (QuitButton)
		{
			Application.Quit();
		}
	}
}
