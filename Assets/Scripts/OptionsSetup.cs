using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionsSetup : Commons {

	// Use this for initialization
	void Start () {
		KeyCode[] up = new KeyCode[2]{KeyCode.UpArrow, KeyCode.W};//0
		ControllerConfig.Add(up);
		KeyCode[] down = new KeyCode[2]{KeyCode.DownArrow, KeyCode.S};//1
		ControllerConfig.Add(down);
		KeyCode[] left = new KeyCode[2]{KeyCode.LeftArrow, KeyCode.A};//2
		ControllerConfig.Add(left);
		KeyCode[] right = new KeyCode[2]{KeyCode.RightArrow, KeyCode.D};//3
		ControllerConfig.Add(right);
		KeyCode[] jump = new KeyCode[2]{KeyCode.Space, KeyCode.RightShift};//4
		ControllerConfig.Add(jump);
		KeyCode[] control = new KeyCode[2]{KeyCode.C, KeyCode.Q};//5
		ControllerConfig.Add(control);
		KeyCode[] attack = new KeyCode[2]{KeyCode.X, KeyCode.E};//6
		ControllerConfig.Add(attack);
		KeyCode[] reset = new KeyCode[2]{KeyCode.R, KeyCode.O};//7
		ControllerConfig.Add(reset);
		KeyCode[] pause = new KeyCode[2]{KeyCode.Escape, KeyCode.P};//8
		ControllerConfig.Add(pause);
		ResolutionOptions = Screen.resolutions;
		foreach (Resolution res in ResolutionOptions) 
		{
			Debug.Log (res.width.ToString ()+" x " +res.height.ToString());
		}
	}
}
