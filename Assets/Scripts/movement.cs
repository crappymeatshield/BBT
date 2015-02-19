using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class movement : Commons {
	public float speed = 6.0F;
    public float jumpSpeed = 10.0F;
    public float gravity = 20.0F;
	public float pushSpeed = 6.0F;
	public int health =100;
	public int maxhealth = 100;
	public bool activecharacter = false;
	public GameObject HPHealth = null;
	public GameObject greenHPHealth = null;
	public Camera activecam = null;
	public float redbarxpos=0.065f;
	public float redbarypos=0.1f;
	public float redbarpercentlength=0.6f;
	public int levelminus=4;
	public Texture2D emptybubble=null;
	public Texture2D filledbubble=null;
	private bool swim = false;
	private float maxsmash=2.0f;
    private Vector3 moveDirection = Vector3.zero;
	private Vector3 pushDirection = Vector3.zero;
	private GameObject ObjToFollow=null;
	private string[] animallist= new string[]{"bear", "turtle", "bird"};
	private bool buttondown=false;	
	private int spikecount=0;
	private bool Gamepaused=false;
	private int healthdeccount=0;
	private int maxcount=45;
	private float invuntime=0;
	private float invunmaxtime=0.2f;
	private bool invun=false;
	private float hpbarlength=20.0f;
	private float greenhpbarlength=20.0f;
	private GameObject redhb=null;
	private GameObject greenhb=null;
	private bool canpush=false;
	private bool completeachieve=false;
	private bool allliveachieve=false;
	private bool alldeadachieve=false;
	private bool allbaconachieve=false;
	private bool timeachieve=false;
	private bool keybindingset=false;
	private bool bEditingControls=false;
	private int nControlCounter = 0;
	private int primsec=0;
	private float vertaxis=0;
	private float hortaxis=0;
	private bool dontunpause=false;
	private bool playingSplash = false;
	private int splashTimer = 0;
	private int deathDelay = 0;
	private Animator anim;
	private int walkhash = Animator.StringToHash("speed");
	private int swimhash = Animator.StringToHash("Swimming");


	// Use this for initialization
	void Start () {
		if(tag=="turtle")//this will be removed later it is just here for testing purposes as the other animals don't have animators
			anim = GetComponent<Animator>();
		if(!editormode)
			Time.timeScale=1.0f;
		Gamepaused=false;
		redhb = (GameObject)Instantiate(HPHealth, transform.position, Quaternion.identity);
		greenhb = (GameObject)Instantiate(greenHPHealth, transform.position, Quaternion.identity);
		GameObject endobject=GameObject.FindGameObjectWithTag("end");
		EndLevel setachievescript = (EndLevel) endobject.gameObject.GetComponent(typeof(EndLevel));
		activecam= (Camera)GameObject.FindGameObjectWithTag("MainCamera").GetComponent(typeof(Camera));
//		if(tag=="parasite"&&!editormode)
//		{
//			GameObject cameraview= transform.GetChild(0).gameObject;
//			cameraview.SetActive(true);
//		}
		/*if(!setachievescript.BonusLevel)
		{
			completeachieve=setachievescript.getcomplete(Application.loadedLevel-levelminus);
			allliveachieve=setachievescript.getalive(Application.loadedLevel-levelminus);
			alldeadachieve=setachievescript.getdead(Application.loadedLevel-levelminus);
			allbaconachieve=setachievescript.getbacon(Application.loadedLevel-levelminus);
			timeachieve=setachievescript.gettime(Application.loadedLevel-levelminus);
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!Input.GetKey(ControllerConfig[0][0])&&!Input.GetKey(ControllerConfig[0][1])&&!Input.GetKey(ControllerConfig[1][0])&&!Input.GetKey(ControllerConfig[1][1]))
		{
			vertaxis=0;

		}
		if(!Input.GetKey(ControllerConfig[2][0])&&!Input.GetKey(ControllerConfig[2][1])&&!Input.GetKey(ControllerConfig[3][0])&&!Input.GetKey(ControllerConfig[3][1]))
		{
			hortaxis=0;
		}
//		if(activecharacter&&!editormode)
//		{
//			activecam=transform.GetChild(0).gameObject.camera;
//			//Debug.Log(transform.forward.ToString());
//		}
//		else
//		{
//			GameObject animal = null;
//			if(tag!="bear"&&!editormode)
//			{
//
//				animal = GameObject.FindGameObjectWithTag("bear");
//				if(animal!=null)
//				{
//
//					movement control = (movement) animal.gameObject.GetComponent(typeof(movement));
//					if(control.activecharacter)
//					{
//						activecam=animal.transform.GetChild(0).gameObject.camera;
//					}
//				}
//			}
//			if(tag!="bird"&&!editormode)
//			{
//				animal = GameObject.FindGameObjectWithTag("bird");
//				if(animal!=null)
//				{
//
//					movement control = (movement) animal.gameObject.GetComponent(typeof(movement));
//					if(control.activecharacter)
//					{
//						activecam=animal.transform.GetChild(0).gameObject.camera;
//					}
//				}
//			}
//			if(tag!="parasite"&&!editormode)
//			{
//				animal = GameObject.FindGameObjectWithTag("parasite");
//				if(animal!=null)
//				{
//					movement control = (movement) animal.gameObject.GetComponent(typeof(movement));
//					if(control.activecharacter)
//					{
//						activecam=animal.transform.GetChild(0).gameObject.camera;
//					}
//				}
//			}
//			if(tag!="turtle"&&!editormode)
//			{
//				animal = GameObject.FindGameObjectWithTag("turtle");
//				if(animal!=null)
//				{
//					//Debug.Log(animal.name.ToString());
//					movement control = (movement) animal.gameObject.GetComponent(typeof(movement));
//					if(control.activecharacter)
//					{
//						activecam=animal.transform.GetChild(0).gameObject.camera;
//					}
//				}
//			}
//		}
		if(health>0&&!editormode)
		{
			redhb.transform.position=activecam.WorldToViewportPoint(transform.position);
			float xpos=redhb.transform.position.x;
			float ypos=redhb.transform.position.y;
			float zpos=0;
			Vector3 pos=redhb.transform.position;
			ypos+=redbarypos;
			xpos-=redbarxpos;
			pos.x=xpos;
			pos.y=ypos;
			pos.z=zpos;
			redhb.transform.position=pos;
			redhb.transform.localScale=Vector3.zero;
			float healthpercent= (float)health/maxhealth;
			healthpercent = 100;
			hpbarlength = healthpercent*redbarpercentlength;
			redhb.guiTexture.pixelInset=new Rect(10,10,hpbarlength,5);
		}
		if(health>0&&!editormode)
		{
			greenhb.transform.position=activecam.WorldToViewportPoint(transform.position);
			float xpos=greenhb.transform.position.x;
			float ypos=greenhb.transform.position.y;
			float zpos=0.01f;
			Vector3 pos=greenhb.transform.position;
			ypos+=redbarypos;
			xpos-=redbarxpos;
			pos.x=xpos;
			pos.y=ypos;
			pos.z=zpos;
			greenhb.transform.position=pos;
			greenhb.transform.localScale=Vector3.zero;
			float healthpercent= (float)health/maxhealth;
			healthpercent = healthpercent * 100;
			greenhpbarlength = healthpercent*redbarpercentlength;
			greenhb.guiTexture.pixelInset=new Rect(10,10,greenhpbarlength,5);
		}
		if(invun)
		{
			if(Time.time>invuntime+invunmaxtime)
				invun=false;
		}
		
		if(Input.GetKeyDown(ControllerConfig[8][0])&& tag=="parasite"&&!editormode&&!dontunpause || Input.GetKeyDown(ControllerConfig[8][1])&& tag=="parasite"&&!editormode&&!dontunpause)
		{
			PauseGame();
			int temptimescale =(int)Time.timeScale;
			//Debug.Log(temptimescale.ToString());
		}
		if(Input.GetKeyDown(ControllerConfig[7][0]) && tag=="parasite"&&!editormode || Input.GetKeyDown(ControllerConfig[7][1]) && tag=="parasite"&&!editormode)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if(Input.GetKeyDown(ControllerConfig[6][0]) && tag=="bear" && activecharacter&&!editormode || Input.GetKeyDown(ControllerConfig[6][1]) && tag=="bear" && activecharacter&&!editormode)
		{

			GameObject[] breakableobjects = GameObject.FindGameObjectsWithTag("breakable");
			for(int b=0; b<breakableobjects.Length; b++)
			{
				float distance = Vector3.Distance(transform.position, breakableobjects[b].transform.position);
				if(distance <= maxsmash)
				{
//					Vector3 dir = (breakableobjects[b].transform.position-transform.position).normalized;
//					float direction = Vector3.Dot(dir, transform.forward);
//					if(direction>0)
//					{
					GameObject.Find("AudioCube").SendMessage("Crack");
						Destroy(breakableobjects[b].gameObject);

//					}
				}
			}
		}
		if(dontunpause)
		{
			if(!bEditingControls)
			{
				dontunpause=false;
			}
		}
		//Debug.Log (tag + " = " + gameObject.layer.ToString());
		Vector3 newposition=transform.position;
		newposition.z=0;
		if(tag!="parasite"||activecharacter)
			transform.position=newposition;
		CharacterController controller = GetComponent<CharacterController>();
		if(!activecharacter&&tag=="parasite")
		{
			if(healthdeccount<maxcount)
					healthdeccount+=1*(int)Time.timeScale;
			else
			{
				int temphealth=health+1;
				if(temphealth<=maxhealth)
				{
					health+=1*(int)Time.timeScale;
					healthdeccount=0;
				}
			}
		}
		if(activecharacter)
		{
			if(Input.GetKey(ControllerConfig[0][0])||Input.GetKey(ControllerConfig[0][1]))
			{
				vertaxis=1;
			}
			else if(Input.GetKey(ControllerConfig[1][0])||Input.GetKey(ControllerConfig[1][1]))
			{
				vertaxis=-1;
			}
			if(Input.GetKey(ControllerConfig[2][0])||Input.GetKey(ControllerConfig[2][1]))
			{
				hortaxis=-1;
			}
			else if(Input.GetKey(ControllerConfig[3][0])||Input.GetKey(ControllerConfig[3][1]))
			{
				hortaxis=1;
			}
			if(hortaxis!=0)
			{
				Quaternion target= Quaternion.Euler(0, 90*hortaxis, 0);
				transform.rotation= target;
//				GameObject cameratomove = transform.GetChild(0).gameObject;
//				cameratomove.transform.localPosition=new Vector3(25*hortaxis,0, 0);
//				Quaternion camrot = Quaternion.Euler(0, (-1*hortaxis)*90, 0);
//				cameratomove.transform.localRotation=camrot;
//				//Debug.Log(cameratomove.transform.localRotation.ToString());
			}
			//Debug.Log(moveDirection.y.ToString());
			if(healthdeccount<maxcount)
				healthdeccount+=1*(int)Time.timeScale;
			else
			{
				health-=1*(int)Time.timeScale;
				healthdeccount=0;
			}
	        if (gameObject.tag != "bird" && !swim && controller.isGrounded)
			{
				moveDirection = new Vector3(0, 0, Math.Abs(hortaxis)); //Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
				ObjToFollow=null;
				if (Input.GetKey(ControllerConfig[4][0])||Input.GetKey(ControllerConfig[4][1])||Input.GetKey(ControllerConfig[0][0])||Input.GetKey(ControllerConfig[0][1]))
	                moveDirection.y = jumpSpeed;   
				if(gameObject.tag=="turtle")
				{
					float temphortaxis = Mathf.Abs(hortaxis);
					anim.SetFloat(walkhash, temphortaxis);
					anim.SetBool(swimhash, false);
				}
			}
			else if(gameObject.tag=="bird" || swim)
			{
				if(tag=="parasite")
				{
					speed=1;
					healthdeccount+=1*(int)Time.timeScale;
				}
				if(tag=="bird")
					gravity=100;
				moveDirection = new Vector3(0, vertaxis, Math.Abs(hortaxis)); //Input.GetAxis("Vertical")
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
				ObjToFollow=null;
				if(gameObject.tag=="turtle")
				{
					float temphortaxis = Mathf.Abs(hortaxis);
					anim.SetFloat(walkhash, temphortaxis);
					anim.SetBool(swimhash, true);
				}
			}
			else if (gameObject.tag != "bird" && !swim && !controller.isGrounded)
			{
				moveDirection = new Vector3(0, moveDirection.y, Math.Abs(hortaxis)); //Input.GetAxis("Vertical")
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection.x *= speed;
				ObjToFollow=null;
				if(gameObject.tag=="turtle")
				{
					float temphortaxis = Mathf.Abs(hortaxis);
					anim.SetFloat(walkhash, temphortaxis);
					anim.SetBool(swimhash, false);
				}
			}
			//Debug.Log("after move "+moveDirection.y.ToString());
			if(tag=="parasite")
			{
				if(!swim)
					speed=5;
				if(Input.GetKeyDown(ControllerConfig[5][0])&&!buttondown&&canpush&&Time.timeScale>0.0f||Input.GetKeyDown(ControllerConfig[5][1])&&!buttondown&&canpush&&Time.timeScale>0.0f)
				{

					healthdeccount=0;
					buttondown=true;
					int animalnum=0;
					GameObject animal=null;
					while(animalnum<=2)
					{
						animal = GameObject.FindGameObjectWithTag(animallist[animalnum]);

						if(animal!=null)
						{
							if(Vector3.Distance(transform.position, animal.transform.position)<=1)
							{
								animalnum+=3;
							}
							else
							{
								animal=null;
								animalnum++;
							}
						}
						else
							animalnum++;
					}
					if(animal==null)
						animalnum+=3;
					//PLAY THE CORRECT SOUND FDR ENTERING ANIMAL
					if(animalnum == 3)
						GameObject.Find("AudioCube").SendMessage("Grunt");
					else if(animalnum == 4)
						GameObject.Find("AudioCube").SendMessage("Urp");
					else if(animalnum == 5)
						GameObject.Find("AudioCube").SendMessage("Tweet");
					if(animal!=null)
					{
						//Debug.Log("YO SUP");
						Vector3 place = animal.transform.gameObject.transform.position;
						place.z=2;
						//place.y+=1;
						movement control = (movement) animal.gameObject.GetComponent(typeof(movement));
//						activecharacter=false;
//						control.activecharacter=true;
						if(animal.tag=="turtle")
						{
							//Physics.IgnoreLayerCollision(10, 8);
							Physics.IgnoreLayerCollision(8, 10);
							Physics.IgnoreLayerCollision(11, 10);
							Physics.IgnoreLayerCollision(12, 10);
						}
						if(animal.tag=="bear")
						{
							GameObject[] Waterobjects = GameObject.FindGameObjectsWithTag("DeepWater");
							for(int w=0; w<Waterobjects.Length; w++)
							{
								BoxCollider waterCollider = (BoxCollider) Waterobjects[w].GetComponent("BoxCollider");
								if(waterCollider!=null)
									waterCollider.isTrigger=false;
							}
						}
						//collider.enabled=false;
						//rigidbody.useGravity=false;	
//						GameObject cameraview= animal.transform.GetChild(0).gameObject;
//						cameraview.SetActive(true);
//						cameraview= transform.GetChild(0).gameObject;
//						cameraview.SetActive(false);
						//transform.position=place;
						//GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
						//camera.transform.parent=hit.transform;
						//Destroy(rigidbody);
						//transform.parent = hit.transform;
						transform.position=place;
						ObjToFollow=animal;
						moveDirection=Vector3.zero;
						activecharacter=false;
						control.activecharacter=true;
					}
				}
			}
			else
			{
				if(Input.GetKeyDown(ControllerConfig[5][0])&&!buttondown&&canpush&&Time.timeScale>0.0f||Input.GetKeyDown(ControllerConfig[5][1])&&!buttondown&&canpush&&Time.timeScale>0.0f)
				{
					GameObject.Find("AudioCube").SendMessage("Site");
					//Debug.Log("Hola activechar="+activecharacter.ToString());
					healthdeccount=0;
					buttondown=true;
					Vector3 place = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
					GameObject bug = GameObject.FindGameObjectWithTag("parasite");
					movement control = (movement) bug.gameObject.GetComponent(typeof(movement));
					activecharacter=false;
					control.activecharacter=true;
					control.ObjToFollow=null;
					control.buttondown=true;
					if(tag=="turtle")
					{
						//Physics.IgnoreLayerCollision(10, 8);
						Physics.IgnoreLayerCollision(8, 10, false);
						Physics.IgnoreLayerCollision(11, 10, false);
						Physics.IgnoreLayerCollision(12, 10, false);
//						GameObject[] Waterobjects = GameObject.FindGameObjectsWithTag("DeepWater");
//						for(int w=0; w<Waterobjects.Length; w++)
//						{
//							BoxCollider waterCollider = (BoxCollider) Waterobjects[w].GetComponent("BoxCollider");
//							if(waterCollider!=null)
//								waterCollider.isTrigger=false;
//						}
					}
					if(tag=="bear")
					{
						GameObject[] Waterobjects = GameObject.FindGameObjectsWithTag("DeepWater");
							for(int w=0; w<Waterobjects.Length; w++)
							{
								BoxCollider waterCollider = (BoxCollider) Waterobjects[w].GetComponent("BoxCollider");
								if(waterCollider!=null)
									waterCollider.isTrigger=true;
							}
					}
					//transform.position=place2;
					bug.transform.position=place;
					//bug.rigidbody.useGravity=true;
//					GameObject cameraview= bug.transform.GetChild(0).gameObject;
//					cameraview.SetActive(true);
//					cameraview= transform.GetChild(0).gameObject;
//					cameraview.SetActive(false);
					moveDirection=Vector3.zero;
					//rigidbody.velocity=Vector3.zero;
					//GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
					//camera.transform.parent=bug.transform;
				}
			}
			canpush=true;
		}
		else
		{
			if(tag=="parasite")
			{
				Vector3 placetobe=ObjToFollow.transform.position;
				placetobe.z=2;
				transform.position=placetobe;
			}
			else if(tag=="bird")
			{
				gravity=20;
			}
			canpush=false;
		}
		if(health<=0)
		{
			if(tag!="parasite"&&activecharacter)
			{
				Vector3 place = transform.position;
				GameObject bug = GameObject.FindGameObjectWithTag("parasite");
				movement control = (movement) bug.gameObject.GetComponent(typeof(movement));
				activecharacter=false;
				control.activecharacter=true;
				control.ObjToFollow=null;
				if(tag=="turtle")
				{
					Physics.IgnoreLayerCollision(8, 10, false);
					Physics.IgnoreLayerCollision(11, 10, false);
					Physics.IgnoreLayerCollision(12, 10, false);
				}
				bug.transform.position=place;
//				GameObject cameraview= bug.transform.GetChild(0).gameObject;
//				cameraview.SetActive(true);
//				cameraview= transform.GetChild(0).gameObject;
//				cameraview.SetActive(false);
				moveDirection=Vector3.zero;
				Destroy(redhb);
				Destroy(greenhb);
				Destroy(gameObject);

			}
			else if(tag!="parasite")
			{
				//check the game object then play the correct sound before dying
				if (gameObject.tag == "bird")
					GameObject.Find("AudioCube").SendMessage("Tweet");
				if (gameObject.tag == "turtle")
					GameObject.Find("AudioCube").SendMessage("Urp");
				if (gameObject.tag == "bear")
					GameObject.Find("AudioCube").SendMessage("Grunt");
				Destroy(redhb);
				Destroy(greenhb);
				Destroy(gameObject);

			}
			else if(tag=="parasite")
			{
				//Play Death sound and disable parasite BEFORE instant reset
				speed = 0;
				if(deathDelay == 0)
				GameObject.Find("AudioCube").SendMessage("Die");
				deathDelay += 1;
				if(deathDelay > 50)
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if(tag=="turtle")
		{
			if(swim)
			{
				speed=10;
				if(activecharacter)
					gravity=20;
				else
					gravity=0;
			}
			else
				speed=3;
		}
		if(Input.GetKeyUp(ControllerConfig[5][0])||Input.GetKeyUp(ControllerConfig[5][1]))
			buttondown=false;
		if(tag!="parasite"&&!controller.isGrounded||activecharacter&&!controller.isGrounded)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}
		if (tag == "turtle" && !activecharacter && !controller.isGrounded) 
		{
			if(gravity==0)
				moveDirection.y=0;
		}
	   	controller.Move(moveDirection * Time.deltaTime);

		//Prevent Bubble from being spammed
		if(playingSplash)
		{
			splashTimer += 1;
			print (splashTimer);
			if(splashTimer > 100)
			{
				playingSplash = false;
				splashTimer = 0;

			}
		}
	}

	void OnGUI()
	{
		if(Gamepaused&&!editormode)// && activecharacter
		{
			if(!keybindingset)
			{
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Pause Menu");
				if(GUI.Button(new Rect(10, Screen.height/10, (Screen.width*0.25f)-20, 25), "Resume")){
					PauseGame();}
				if(GUI.Button(new Rect((Screen.width*0.25f)+10, Screen.height/10, (Screen.width*0.25f)-20, 25), "Restart Level")){
					Application.LoadLevel(Application.loadedLevel);}
				if(GUI.Button(new Rect((Screen.width*0.75f)+10, Screen.height/10, (Screen.width*0.25f)-20, 25), "Level Select")){
					Application.LoadLevel(0);}
				if(GUI.Button(new Rect((Screen.width*0.5f)+10, Screen.height/10, (Screen.width*0.25f)-20, 25), "Key Bindings"))
				{
					keybindingset=true;
				}
				if(completeachieve)
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*2, Screen.height/7, Screen.height/7), filledbubble);//change size to be based on screen size.
				else
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*2, Screen.height/7, Screen.height/7), emptybubble);
				GUI.Label(new Rect((Screen.width/25)*3,(Screen.height/7)*2.333f, (Screen.width/25)*20, Screen.height/7), "Level Completed.");
				if(allliveachieve)
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*3, Screen.height/7, Screen.height/7), filledbubble);
				else
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*3, Screen.height/7, Screen.height/7), emptybubble);
				GUI.Label(new Rect((Screen.width/25)*3,(Screen.height/7)*3.333f, (Screen.width/25)*22, Screen.height/7), "Completed Level with all animals still alive.");
				if(alldeadachieve)
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*4, Screen.height/7, Screen.height/7), filledbubble);
				else
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*4, Screen.height/7, Screen.height/7), emptybubble);
				GUI.Label(new Rect((Screen.width/25)*3,(Screen.height/7)*4.333f, (Screen.width/25)*22, Screen.height/7), "Completed Level with all animals dead.");
				if(allbaconachieve)
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*5, Screen.height/7, Screen.height/7), filledbubble);
				else
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*5, Screen.height/7, Screen.height/7), emptybubble);
				GUI.Label(new Rect((Screen.width/25)*3,(Screen.height/7)*5.333f, (Screen.width/25)*22, Screen.height/7), "Completed Level and collect all the bacon.");
				if(timeachieve)
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*6, Screen.height/7, Screen.height/7), filledbubble);
				else
					GUI.Label(new Rect(Screen.width/25, (Screen.height/7)*6, Screen.height/7, Screen.height/7), emptybubble);
				GUI.Label(new Rect((Screen.width/25)*3,(Screen.height/7)*6.333f, (Screen.width/25)*22, Screen.height/7), "Completed Level under time limit.");
			}
			else if(keybindingset)
			{
				if(bEditingControls)
				{
					dontunpause=true;
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
						//Debug.Log("Detected key code: " + e.keyCode);
						KeyCode[] tempkeycode = new KeyCode[2];
						tempkeycode=ControllerConfig[nControlCounter];
						tempkeycode[primsec] = e.keyCode;
						ControllerConfig[nControlCounter]=tempkeycode;
						bEditingControls = false;
					}	
				}
				
				GUI.Box(new Rect ((0f ),0f,(Screen.width),Screen.height), "Controller Configuration");
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
					keybindingset=false;
				}
			}
		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit)//Make Sure to up the mass of the boxes
	{
		//Debug.Log("hit!");
		if(activecharacter)
		{
			pushDirection = new Vector3(pushSpeed,0,0);
	        if (hit.gameObject.tag == "boxes" && tag=="bear") //&& controller.isGrounded
			{
				if(transform.position.y-1<hit.transform.position.y)
				{
					if(transform.position.x>hit.transform.position.x)
						pushDirection *=-1;
	            	hit.rigidbody.velocity=pushDirection;
				}
			}
		}
		if(hit.gameObject.tag=="sludge")
		{
			health=0;
		}
	}
	
	void OnCollisionEnter(Collision hit)
	{
		if(hit.gameObject.tag=="bullet" && !invun)
		{
			//Debug.Log("Before Hit health=" + health.ToString());
			health-=1;
			//Debug.Log("After Hit health=" + health.ToString());
			invun=true;
			invuntime=Time.time;
			//Destroy(hit.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if(gameObject.tag!="parasite"&&hit.gameObject.tag=="switch"&&!editormode)//hit.gameObject.tag == "switch1" &&
		{
			int matchnumber;
			Switch switchscript = (Switch) hit.gameObject.GetComponent(typeof(Switch));
			matchnumber=switchscript.switchnumber;
			GameObject[] doors;
			doors = GameObject.FindGameObjectsWithTag("door");
			foreach(GameObject door in doors)
			{
				DoorMove doorscript = (DoorMove) door.GetComponent(typeof(DoorMove));
				if(matchnumber==doorscript.numberofdoor)
					doorscript.open=true;

			}
		}
		if(hit.gameObject.tag=="water" || hit.gameObject.tag=="DeepWater" || hit.gameObject.tag=="turtlewater" &&!editormode)
		{
			if(gameObject.tag=="bird")
				health=0;
			else if(gameObject.tag!="bear")
			{
				swim=true;
//				if(gameObject.tag=="turtle")
//				{
//					anim.SetBool(swimhash, true);
//				}
			}
			else if(tag=="bear" && !activecharacter)
				health=0;
		}
		if(hit.gameObject.tag=="Spike")
		{
			health-=(1*(int)Time.timeScale);
		}		
		if(hit.gameObject.tag=="Bacon"&&!editormode)
		{
			if(tag=="parasite")
			{
				health-=10;
				Destroy(hit.gameObject);
			}
			else
			{
				health+=10;
				if(health>maxhealth)
					health=maxhealth;
				Destroy(hit.gameObject);
			}
			GameObject.Find("AudioCube").SendMessage("Chime");
		}
//		if(hit.gameObject.tag=="ExtraBacon")
//		{
//			if(tag=="parasite")
//			{
//				health-=10;
//				Destroy(hit.gameObject);
//			}
//			else
//			{
//				health+=10;
//				if(health>maxhealth)
//					health=maxhealth;
//				Destroy(hit.gameObject);
//			}
//		}
		if(hit.gameObject.tag=="end" && activecharacter && !editormode)
		{
			//Debug.Log(editormode.ToString());
			EndLevel endscript = (EndLevel) hit.gameObject.GetComponent(typeof(EndLevel));
//			if(!endscript.BonusLevel)  achievement area needs work<<<<<<<<<<<<<<<<<<<<<<<<<=================================================
//			{
//				if(Time.timeSinceLevelLoad<endscript.TimetoBeat && !endscript.gettime(Application.loadedLevel-levelminus))
//				{
//					endscript.settime(Application.loadedLevel-levelminus, true);
//				}
//				if(!endscript.getcomplete(Application.loadedLevel-levelminus))
//				{
//					endscript.setcomplete(Application.loadedLevel-levelminus, true);
//				}
//				if(!endscript.getalive(Application.loadedLevel-levelminus))
//				{
//					bool allalive=true;
//					int num=0;
//					while (allalive&&num<=2)
//					{
//						GameObject animal=null;
//						animal = GameObject.FindGameObjectWithTag(animallist[num]);
//						//Debug.Log(animal.ToString());
//						if(animal==null)
//							allalive=false;
//						num++;
//					}
//					//Debug.Log(allalive.ToString());
//					if(allalive)
//					{
//						endscript.setalive(Application.loadedLevel-levelminus, true);
//						//Debug.Log(endscript.getalive(Application.loadedLevel-levelminus).ToString());
//					}
//				}
//				if(!endscript.getdead(Application.loadedLevel-levelminus))
//				{
//					bool allalivez=false;
//					int num=0;
//					while (!allalivez&&num<=2)
//					{
//						GameObject animalz=null;
//						animalz = GameObject.FindGameObjectWithTag(animallist[num]);
//						//Debug.Log(animalz.ToString());
//						if(animalz!=null)
//							allalivez=true;
//						num++;
//					}
//					if(!allalivez)
//					{
//						endscript.setdead(Application.loadedLevel-levelminus, true);
//					}
//				}
//				if(!endscript.getbacon(Application.loadedLevel-levelminus))
//				{
//					GameObject bacon=null;
//					bacon = GameObject.FindGameObjectWithTag("Bacon");
//					if(bacon==null)
//						endscript.setbacon(Application.loadedLevel-levelminus, true);
//				}
//			}
//			else
//			{
//				if(Time.timeSinceLevelLoad<endscript.TimetoBeat && !endscript.getBonustime(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels))
//				{
//					endscript.setBonustime(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels, true);
//				}
//				if(!endscript.getBonuscomplete(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels))
//				{
//					endscript.setBonuscomplete(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels, true);
//				}
//				if(!endscript.getBonusalive(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels))
//				{
//					bool allalive=true;
//					int num=0;
//					while (allalive&&num<=2)
//					{
//						GameObject animal=null;
//						animal = GameObject.FindGameObjectWithTag(animallist[num]);
//						//Debug.Log(animal.ToString());
//						if(animal==null)
//							allalive=false;
//						num++;
//					}
//					//Debug.Log(allalive.ToString());
//					if(allalive)
//					{
//						endscript.setBonusalive(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels, true);
//						//Debug.Log(endscript.getalive(Application.loadedLevel-levelminus).ToString());
//					}
//				}
//				if(!endscript.getBonusdead(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels))
//				{
//					bool allalivez=false;
//					int num=0;
//					while (!allalivez&&num<=2)
//					{
//						GameObject animalz=null;
//						animalz = GameObject.FindGameObjectWithTag(animallist[num]);
//						//Debug.Log(animalz.ToString());
//						if(animalz!=null)
//							allalivez=true;
//						num++;
//					}
//					if(!allalivez)
//					{
//						endscript.setBonusdead(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels, true);
//					}
//				}
//				if(!endscript.getBonusbacon(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels))
//				{
//					GameObject bacon=null;
//					bacon = GameObject.FindGameObjectWithTag("Bacon");
//					if(bacon==null)
//						endscript.setBonusbacon(Application.loadedLevel-levelminus-endscript.numofnonbonuslevels, true);
//				}
//			}
			if(endscript.UserCreatedLevel)
			{
				Application.LoadLevel(0);
			}
			else if(endscript.LastLevel)
			{
				Application.LoadLevel(endscript.NextIndex);
			}
			else
			{
				int level = Application.loadedLevel;
				level++;
				Application.LoadLevel(level);
			}
		}
	}
	
	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.tag=="water" || hit.gameObject.tag=="DeepWater")
		{
			if(gameObject.tag!="bear"&&gameObject.tag!="bird")
			{
				swim=true;
//				if(gameObject.tag=="turtle")
//				{
//					anim.SetBool(swimhash, true);
//				}
			}
			else if(tag=="bear" && !activecharacter)
				health=0;

			if(!playingSplash && activecharacter && !Gamepaused)
			{
				GameObject.Find("AudioCube").SendMessage("Bubble");
				playingSplash = true;
			}
		}
		if(hit.gameObject.tag=="Spike")
		{
			if(spikecount<maxcount)
				spikecount+=1*(int)Time.timeScale;
			else
			{
				spikecount=0;
				health-=(1*(int)Time.timeScale);
			}
		}
		if(gameObject.tag!="parasite"&&hit.gameObject.tag=="switch")//hit.gameObject.tag == "switch1" &&
		{
			int matchnumber;
			Switch switchscript = (Switch) hit.gameObject.GetComponent(typeof(Switch));
			matchnumber=switchscript.switchnumber;
			GameObject[] doors;
			doors = GameObject.FindGameObjectsWithTag("door");
			foreach(GameObject door in doors)
			{
				DoorMove doorscript = (DoorMove) door.GetComponent(typeof(DoorMove));
				if(matchnumber==doorscript.numberofdoor)
					doorscript.open=true;
			}
		}
	}
	
	void OnTriggerExit(Collider hit)
	{
		if(hit.gameObject.tag=="water" || hit.gameObject.tag=="DeepWater")
		{
			if(gameObject.tag!="bear"&&gameObject.tag!="bird")
			{
				swim=false;
//				if(gameObject.tag=="turtle")
//				{
//					anim.SetBool(swimhash, false);
//				}
			}
		}
		if(hit.gameObject.tag=="Spike")
		{
			spikecount=0;
		}
		if(gameObject.tag!="parasite"&&hit.gameObject.tag=="switch")//hit.gameObject.tag == "switch1" &&
		{
			int matchnumber;
			Switch switchscript = (Switch) hit.gameObject.GetComponent(typeof(Switch));
			matchnumber=switchscript.switchnumber;
			GameObject[] doors;
			doors = GameObject.FindGameObjectsWithTag("door");
			foreach(GameObject door in doors)
			{
				DoorMove doorscript = (DoorMove) door.GetComponent(typeof(DoorMove));
				if(matchnumber==doorscript.numberofdoor)
					if(doorscript.holddoor)
						doorscript.open=false;
			}
		}
	}
	
	void PauseGame()
	{
		if(Gamepaused)
		{
			GameObject.Find("AudioCube").SendMessage("ClickUp");
			Time.timeScale=1.0f;
			Gamepaused=false;
			keybindingset=false;
		}
		else
		{
			GameObject.Find("AudioCube").SendMessage("ClickDown");
			Time.timeScale=0.0f;
			Gamepaused=true;
		}	
	}
	
}
