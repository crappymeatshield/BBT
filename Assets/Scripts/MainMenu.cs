using UnityEngine;
using System.Collections;

public class MainMenu : achievements {
	public Texture background;
	public GUISkin gSkin;
	public AudioClip MainTheme;
	public AudioClip LevelSelectTheme;
	public AudioSource Asource;
	private static bool LevelSelectScreen = false;
	private bool CreditScreen = false;
	private bool OptionScreen = false;
	private bool Keybindingset=false;
	private bool Resolutionset=false;
	private bool InstructScreen = false;
	private bool bEditingControls=false;
	private int nControlCounter=0;
	private int primsec=0;

	void Awake()
	{
		if(LevelSelectScreen)
		{
			Asource.clip=LevelSelectTheme;
			Asource.Play();
		}
	}

	void OnGUI()
	{
		GUI.skin = gSkin;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);
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
			GUI.Label(new Rect (Screen.width *.25f ,Screen.height *0,(Screen.width * .5f),Screen.height *.05f), "Controller Configuration");
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Action");
			GUI.Label(new Rect (Screen.width * .25f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Primary Button");
			GUI.Label(new Rect (Screen.width * .625f, Screen.height * .07f, Screen.width *.25f, Screen.height *.075f), "Secondary Button");
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .15f, Screen.width *.25f, Screen.height *.075f), "UP");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .23f, Screen.width *.25f, Screen.height *.075f), "DOWN");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .31f, Screen.width *.25f, Screen.height *.075f), "LEFT");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .39f, Screen.width *.25f, Screen.height *.075f), "RIGHT");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .47f, Screen.width *.25f, Screen.height *.075f), "JUMP");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .55f, Screen.width *.25f, Screen.height *.075f), "CONTROL");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .63f, Screen.width *.25f, Screen.height *.075f), "ATTACK");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .71f, Screen.width *.25f, Screen.height *.075f), "RESET");
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
			GUI.Label(new Rect (Screen.width * .05f, Screen.height * .79f, Screen.width *.25f, Screen.height *.075f), "PAUSE");
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
		else if(Resolutionset)
		{
			int Ressettingcount=0; 
			Resolution current=Screen.currentResolution;
			Debug.Log(Ressettingcount.ToString()+"  "+ResolutionOptions.Length.ToString());
			for (int i = 0; i<ResolutionOptions.Length; i++)
			{
				if(current.height==ResolutionOptions[i].height&&current.width==ResolutionOptions[i].width)
				{
					Ressettingcount=i;
					i+=ResolutionOptions.Length;
				}
			}
			GUI.Label(new Rect (Screen.width *.25f ,Screen.height *.05f,(Screen.width * .5f),Screen.height *.05f), "Resolution Settings");
			if (GUI.Button (new Rect (Screen.width * .15f, Screen.height * .35f, Screen.width *.15f, Screen.height *.15f), "Left" )) 
			{
				if(Ressettingcount>0)
					Ressettingcount--;
			}
			GUI.Label(new Rect (Screen.width *.35f ,Screen.height *.35f,(Screen.width * .3f),Screen.height *.15f), ResolutionOptions[Ressettingcount].width.ToString()+" x "+ResolutionOptions[Ressettingcount].height.ToString());
			if (GUI.Button (new Rect (Screen.width * .7f, Screen.height * .35f, Screen.width *.15f, Screen.height *.15f), "Right" )) 
			{
				if(Ressettingcount<(ResolutionOptions.Length-1))
				{
					Ressettingcount++;
					Debug.Log(Ressettingcount.ToString()+"  "+ResolutionOptions.Length.ToString());
				}
			}
			//add fullscreen checkbox (toggle)
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .8f, Screen.width *.25f, Screen.height *.1f), "Save Settings" )) 
			{
				Screen.SetResolution(ResolutionOptions[Ressettingcount].width, ResolutionOptions[Ressettingcount].height, true);
				//add undo timer
			}
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .9f, Screen.width *.25f, Screen.height *.1f), "Back" )) 
			{
				Resolutionset = false;
			}
		}
		else if(OptionScreen)
		{
			GUI.Label(new Rect (Screen.width *.25f ,Screen.height *.05f,(Screen.width * .5f),Screen.height *.05f), "Option Screen");
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .2f, Screen.width *.25f, Screen.height *.1f), "Key Bindings")) 
			{
				Keybindingset = true;
			} 
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .35f, Screen.width *.25f, Screen.height *.1f), "Resolution Settings" )) 
			{
				Resolutionset = true;
			}
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .9f, Screen.width *.25f, Screen.height *.1f), "Back" )) 
			{
				OptionScreen = false;
			}
		}
		else if(InstructScreen)
		{
			GUI.Label(new Rect (Screen.width *.25f ,Screen.height *.05f,(Screen.width * .5f),Screen.height *.05f), "Instruction Screen");
			GUI.Label (new Rect (Screen.width * .35f, Screen.height * .2f, Screen.width *.25f, Screen.height *.1f), "Instructions go here");
			if(GUI.Button(new Rect(Screen.width*0.35f, Screen.height*0.8f, Screen.width*0.3f, Screen.height*0.1f), "Options"))
			{
				OptionScreen=true;
			}
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .9f, Screen.width *.3f, Screen.height *.1f), "Back" )) 
			{
				InstructScreen = false;
			}
		}
		else if(CreditScreen)
		{
			GUI.Label(new Rect (Screen.width *.25f ,Screen.height *.05f,(Screen.width * .5f),Screen.height *.05f), "Credit Screen");
			GUI.Label (new Rect (Screen.width * .35f, Screen.height * .2f, Screen.width *.25f, Screen.height *.1f), "Credits go here");
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .9f, Screen.width *.3f, Screen.height *.1f), "Back" )) 
			{
				CreditScreen = false;
			}
		}
		else if(LevelSelectScreen)
		{
			if(GUI.Button(new Rect(Screen.width*0.1f, Screen.height*0.4f, Screen.width*0.15f, Screen.height*0.15f), "1"))
			{
				Application.LoadLevel(1);
			}
			if(GUI.Button(new Rect(Screen.width*0.3f, Screen.height*0.4f, Screen.width*0.15f, Screen.height*0.15f), "2"))
			{
				Application.LoadLevel(2);
			}
			if(GUI.Button(new Rect(Screen.width*0.5f, Screen.height*0.4f, Screen.width*0.15f, Screen.height*0.15f), "3"))
			{
				Application.LoadLevel(3);
			}
			if(GUI.Button(new Rect(Screen.width*0.7f, Screen.height*0.4f, Screen.width*0.15f, Screen.height*0.15f), "4"))
			{
				Application.LoadLevel(4);
			}
			if (GUI.Button (new Rect (Screen.width * .35f, Screen.height * .9f, Screen.width *.3f, Screen.height *.1f), "Main Menu" )) 
			{
				LevelSelectScreen = false;
				Asource.clip=MainTheme;
				Asource.Play();
			}
		}
		else
		{
			GUI.Label(new Rect (Screen.width * 0.35f, Screen.height * 0.15f, Screen.width * 0.3f, Screen.height * 0.1f), "Bear, Bird, Turtle:");
			GUI.Label (new Rect (Screen.width * 0.40f, Screen.height * 0.25f, Screen.width * 0.2f, Screen.height * 0.1f), "Infection");
			if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.1f), "Play"))
			{
				LevelSelectScreen=true;
				Asource.clip=LevelSelectTheme;
				Asource.Play();
				//Application.LoadLevel("levelselect");
			}
			if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height*0.525f, Screen.width*0.2f, Screen.height*0.1f), "Instructions"))
			{
				InstructScreen=true;
			}
			if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height*0.65f, Screen.width*0.2f, Screen.height*0.1f), "Options"))
			{
				OptionScreen=true;
			}
			if(GUI.Button(new Rect(Screen.width*0.25f, Screen.height*0.775f, Screen.width*0.2f, Screen.height*0.1f), "Credits"))
			{
				CreditScreen=true;
			}
			if(GUI.Button(new Rect(Screen.width*0.55f, Screen.height*0.775f, Screen.width*0.2f, Screen.height*0.1f), "Quit"))
			{
				Application.Quit();
			}
			if(GUI.Button(new Rect(Screen.width*0.55f, Screen.height*0.4f, Screen.width*0.2f, Screen.height*0.1f), "Achievements"))
			{
				//Application.LoadLevel("levelselect");
			}
			if(GUI.Button(new Rect(Screen.width*0.55f, Screen.height*0.525f, Screen.width*0.2f, Screen.height*0.1f), "Level Editor"))
			{
				Application.LoadLevel("leveleditor");
			}
			if(GUI.Button(new Rect(Screen.width*0.55f, Screen.height*0.65f, Screen.width*0.2f, Screen.height*0.1f), "Play Custom Level"))
			{
				GameObject camera = null;
				camera=GameObject.FindGameObjectWithTag("MainCamera");
				CustomLevelGUI control = (CustomLevelGUI) camera.gameObject.GetComponent(typeof(CustomLevelGUI));
				control.loadCustom=true;
				control.gotoSceneIndex="playlevels";
			}
		}
	}
}