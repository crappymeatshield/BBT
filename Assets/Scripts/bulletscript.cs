using UnityEngine;
using System.Collections;

public class bulletscript : MonoBehaviour {
	public bool vertical = true;
	public bool positive = false;
	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection=new Vector3();
		if(vertical)
		{
			if(positive)
			{
				moveDirection = new Vector3(0, 1, 0);
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
			}
			else
			{
				moveDirection = new Vector3(0, -1, 0);
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
			}
		}
		else
		{
			if(positive)
			{
				moveDirection = new Vector3(1, 0, 0);
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
			}
			else
			{
				moveDirection = new Vector3(-1, 0, 0);
	        	moveDirection = transform.TransformDirection(moveDirection);
	        	moveDirection *= speed;
			}
		}
		//Debug.Log(moveDirection.ToString());
		rigidbody.velocity=moveDirection;
	}
	
	void OnCollisionEnter(Collision hit)
	{
		if(tag!="water")
		{
			if(hit.gameObject.tag!="gun")// && hit.gameObject.tag!="parasite" && hit.gameObject.tag!="bear" && hit.gameObject.tag!="bird" && hit.gameObject.tag!="turtle"
			{
				Destroy(gameObject);
			}
		}
		else
		{
			if(hit.gameObject.tag!="gun" && hit.gameObject.tag!="turtle" && hit.gameObject.tag!="bird" && hit.gameObject.tag!= "parasite" && hit.gameObject.tag!= "bear" && hit.gameObject.tag!="Bacon")
			{
				Destroy(gameObject);
			}
		}
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if(tag!="water")
		{
			if(hit.gameObject.tag!="gun")// && hit.gameObject.tag!="parasite" && hit.gameObject.tag!="bear" && hit.gameObject.tag!="bird" && hit.gameObject.tag!="turtle"
			{
				Destroy(gameObject);
			}
		}
		else
		{
			if(hit.gameObject.tag!="gun" && hit.gameObject.tag!="turtle" && hit.gameObject.tag!="bird" && hit.gameObject.tag!= "parasite" && hit.gameObject.tag!= "bear" && hit.gameObject.tag!="Bacon")
			{
				Destroy(gameObject);
			}
		}
	}
}
