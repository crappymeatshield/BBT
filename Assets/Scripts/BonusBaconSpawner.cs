using UnityEngine;
using System.Collections;

public class BonusBaconSpawner : achievements {
	
	// Use this for initialization
	void Start () {
		updatebaconeaton();
		int tempbacon=numberofbaconeaton;
		int rows = Mathf.CeilToInt((float)numberofbaconeaton/4.0f);		
		Vector3 spawnposition = transform.position;
		for(int i=0; i<rows; i++)
		{
			int col=tempbacon;
			if(col>4)
				col=4;
			for(int j=0; j<col; j++)
			{
				GameObject spawnObject = null;
				spawnObject = (GameObject)Instantiate(Resources.Load("ExtraBacon"), spawnposition, Quaternion.identity);
				spawnposition.x+=2;
			}
			spawnposition.y+=2;
			spawnposition.x=-14;
			tempbacon-=col;
		}
	}
}
