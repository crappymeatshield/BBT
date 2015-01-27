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

		int[] option1 = new int[2]{800, 600};
		ResolutionOptions.Add(option1);
		int[] option2 = new int[2]{1024, 768};
		ResolutionOptions.Add(option2);
		int[] option3 = new int[2]{1280, 720};
		ResolutionOptions.Add(option3);
		int[] option4 = new int[2]{1360, 768};
		ResolutionOptions.Add(option4);
		int[] option5 = new int[2]{1366, 768};
		ResolutionOptions.Add(option5);
		int[] option6 = new int[2]{1600, 900};
		ResolutionOptions.Add(option6);
		int[] option7 = new int[2]{1920, 1080};
		ResolutionOptions.Add(option7);
		int[] option8 = new int[2]{2560, 1440};
		ResolutionOptions.Add(option8);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
