using UnityEngine;
using System.Collections;

public class Options : Commons {
	public bool Keybindingset=false;
	public bool Resolutionset=false;
	private bool bEditingControls=false;
	private int nControlCounter=0;
	private int primsec=0;

	void OnGUI()
	{
		if(Keybindingset)
		{
			if(bEditingControls)
			{
				Event e = Event.current;
				if(e.isKey)
				{
					int tempcount=0;
					foreach(KeyCode[] code in ControllerConfig)
					{
						if(code[0]==e.keyCode)
						{
							ControllerConfig[tempcount][0]=KeyCode.None;
						}
						else if(code[1]==e.keyCode)
						{
							ControllerConfig[tempcount][1]=KeyCode.None;
						}
						tempcount++;
					}
					Debug.Log("Detected key code: " + e.keyCode);
					KeyCode[] tempkeycode = new KeyCode[2];
					tempkeycode=ControllerConfig[nControlCounter];
					tempkeycode[primsec] = e.keyCode;
					ControllerConfig[nControlCounter]=tempkeycode;
					bEditingControls = false;
				}	
			}
			
			GUI.Box(new Rect ((Screen.width *.025f ),Screen.height *.01f,(Screen.width * .95f),Screen.height *.98f), "Controller Configuration");
			GUI.Label(new Rect (Screen.width * .1f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Action");
			GUI.Label(new Rect (Screen.width * .325f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Primary Button");
			GUI.Label(new Rect (Screen.width * .685f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Secondary Button");
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .15f, Screen.width *.25f, Screen.height *.075f), "UP");
			if (GUI.Button (new Rect ((Screen.width * .25f),Screen.height * .15f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[0][0].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 0;
				primsec=0;
			} 
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .15f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[0][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 0;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .23f, Screen.width *.25f, Screen.height *.075f), "DOWN");
			if (GUI.Button (new Rect ((Screen.width *.25f ),Screen.height *.23f,(Screen.width *.25f ),Screen.height *.075f), ControllerConfig[1][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 1;
				primsec=0;
			} 
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .23f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[1][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 1;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .31f, Screen.width *.25f, Screen.height *.075f), "LEFT");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.31f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[2][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 2;
				primsec=0;
			} 
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .31f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[2][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 2;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .39f, Screen.width *.25f, Screen.height *.075f), "RIGHT");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.39f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[3][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 3;
				primsec=0;
			}
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .39f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[3][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 3;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .47f, Screen.width *.25f, Screen.height *.075f), "JUMP");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.47f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[4][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 4;
				primsec=0;
			} 
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .47f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[4][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 4;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .55f, Screen.width *.25f, Screen.height *.075f), "CONTROL");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.55f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[5][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 5;
				primsec=0;
			}
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .55f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[5][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 5;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .63f, Screen.width *.25f, Screen.height *.075f), "ATTACK");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.63f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[6][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 6;
				primsec=0;
			}
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .63f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[6][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 6;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .71f, Screen.width *.25f, Screen.height *.075f), "RESET");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.71f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[7][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 7;
				primsec=0;
			}
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .71f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[7][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 7;
				primsec=1;
			}
			GUI.Label(new Rect (Screen.width * .115f, Screen.height * .79f, Screen.width *.25f, Screen.height *.075f), "PAUSE");
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.79f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[8][0].ToString())) 
			{
				bEditingControls = true;
				nControlCounter = 8;
				primsec=0;
			}
			if (GUI.Button (new Rect ((Screen.width * .625f),Screen.height * .79f,(Screen.width *.25f),Screen.height *.075f), ControllerConfig[8][1].ToString() )) 
			{
				bEditingControls = true;
				nControlCounter = 8;
				primsec=1;
			}
			if (GUI.Button (new Rect ((Screen.width *.25f),Screen.height *.9f,(Screen.width *.5f),Screen.height *.075f), "BACK")) 
			{
				Keybindingset=false;
			}
		}
	}
}
