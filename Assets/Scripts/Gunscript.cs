using UnityEngine;
using System.Collections;

public class Gunscript : Commons {
	private float timer=0.0f;
	public float timetowait=1.0f;
	public float startDelay=0.0f;
	private bool delayed=false;
	public bool shootvertical=true;
	public bool shootpositive=false;
	public float bulletspeed=10.0f;
	public bool posion=true;
	public bool water=false;
	//public GameObject bulletspawn=null;
	
	// Use this for initialization
	void Start () {
		if(startDelay==0.0f)
			delayed=true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!editormode)
		{
			if(delayed)
			{
				if(Time.timeSinceLevelLoad>=(timer+timetowait))
				{
					timer=Time.timeSinceLevelLoad;
					//Debug.Log("shoot "+ Time.time.ToString());
					//Spawn bullet here
					spawnBullet();
				}
			}
			else
			{
				if(Time.timeSinceLevelLoad>=startDelay)
				{
					delayed=true;
					timer=Time.timeSinceLevelLoad;
				}
			}
		}
	}
	
	void spawnBullet()
	{ 
		Vector3 spawnposition = transform.position;
		GameObject spawnObject = null;
		if(shootvertical)
		{
			if(shootpositive)
			{
				spawnposition.y+=0.5f;
			}
			else
			{
				spawnposition.y-=0.5f;
			}
		}
		else
		{
			if(shootpositive)
			{
				spawnposition.x+=0.5f;
			}
			else
			{
				spawnposition.x-=0.5f;
			}
		}
		if(posion)
			spawnObject = (GameObject)Instantiate(Resources.Load("bullets"), spawnposition, Quaternion.identity);
		else if(water)
			spawnObject = (GameObject)Instantiate(Resources.Load("WaterBullet"), spawnposition, Quaternion.identity);
		//Debug.Log("Spawn bullet "+ Time.time.ToString());
		bulletscript bullet = (bulletscript) spawnObject.GetComponent("bulletscript");
		bullet.positive=shootpositive;
		bullet.vertical=shootvertical;
		bullet.speed=bulletspeed;	
	}
}
