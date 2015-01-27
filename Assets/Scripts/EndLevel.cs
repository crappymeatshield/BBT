using UnityEngine;
using System.Collections;

public class EndLevel : achievements {
	public bool LastLevel=false;
	public int NextIndex=0;
	public float TimetoBeat=100.0f;
	public bool BonusLevel=false;
	public int numofnonbonuslevels=2;
	public bool UserCreatedLevel=false;
}
