using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Commons : MonoBehaviour {
	public static bool editormode = false;
	public static string levelLocation = "";
	public static Resolution[] ResolutionOptions = null;
	public static List<KeyCode[]> ControllerConfig = new List<KeyCode[]>();
	public static float axisspeed = 0.1f;
	public static float axistime = 0.1f;
	public static string ipmain = "http://playingtofun.com/";
}
