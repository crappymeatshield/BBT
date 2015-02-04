using UnityEngine;
using System.Collections;

public class camera_movement : MonoBehaviour {
	public GameObject parasite = null;

	// Use this for initialization
	void Start () {
		parasite = GameObject.FindGameObjectWithTag("parasite");
		transform.position = new Vector3(parasite.transform.position.x, parasite.transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(parasite==null)
			parasite = GameObject.FindGameObjectWithTag("parasite");
		transform.position = new Vector3(parasite.transform.position.x, parasite.transform.position.y, transform.position.z);
	}
}
