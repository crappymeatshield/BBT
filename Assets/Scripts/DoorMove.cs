using UnityEngine;
using System.Collections;

public class DoorMove : MonoBehaviour {
	public bool open = false;
	public bool Vertical = true;
	public bool positive = true;
	public bool firsttime = true;
	public bool destroydoor = false;
	public bool holddoor = false;
	public int numberofdoor;
	public float howfar = 3.0f;
	public float speed = 5.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(destroydoor)
			holddoor=false;
		if(open)
		{
			Vector3 target =transform.position;
			if(destroydoor)
				Destroy(gameObject);
			if(Vertical && firsttime)
			{
				if(positive)			
					target.y+=howfar;
				else 
					target.y-=howfar;
				firsttime = false;
			}
			else if(!Vertical && firsttime)
			{
				if(positive)			
					target.x+=howfar;
				else 
					target.x-=howfar;
				firsttime=false;
			}
			transform.position = Vector3.MoveTowards(transform.position, target, speed + Time.deltaTime);
		}
		else
		{
			Vector3 target =transform.position;
			if(Vertical && !firsttime)
			{
				if(positive)			
					target.y-=howfar;
				else 
					target.y+=howfar;
				firsttime = true;
			}
			else if(!Vertical && !firsttime)
			{
				if(positive)			
					target.x-=howfar;
				else 
					target.x+=howfar;
				firsttime=true;
			}
			transform.position = Vector3.MoveTowards(transform.position, target, speed + Time.deltaTime);
		}
	}
}
