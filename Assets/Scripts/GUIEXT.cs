using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GUIEXT:MonoBehaviour  {

	static int popupListHash = "PopupList".GetHashCode();
	public static GUIStyle inbuiltListStyle ;
	private static Dictionary<int,int> lastSelectedCache = new Dictionary<int, int>();
	
	static GUIEXT() { // setup
		inbuiltListStyle = new GUIStyle();
		inbuiltListStyle.normal.textColor = Color.white;
		Texture2D tex = new Texture2D(2, 2);
		Color[] colors = new Color[4] {Color.white,Color.white,Color.white,Color.white};
		tex.SetPixels(colors);
		tex.Apply();
		inbuiltListStyle.hover.background = tex;
		inbuiltListStyle.active.background = tex;
		inbuiltListStyle.focused.background = tex;
		inbuiltListStyle.onHover.background = tex;
		inbuiltListStyle.onActive.background = tex;
		inbuiltListStyle.onFocused.background = tex;
		inbuiltListStyle.onNormal.background = tex;
		inbuiltListStyle.padding.left = inbuiltListStyle.padding.right = inbuiltListStyle.padding.top = 
			inbuiltListStyle.padding.bottom = 4;
	}
	
	public static bool Dropdown (Rect position,  bool showList,  GUIContent buttonContent, IList list,
	                             Action<int> callback) {
		
		return Dropdown(position, showList,  buttonContent, list, "button", "box", inbuiltListStyle,
		                callback);
	}
	
	public static bool Dropdown (Rect position, bool showList, GUIContent buttonContent, IList list ,
	                             GUIStyle listStyle,Action<int> callBack) {
		
		
		
		return Dropdown(position, showList, buttonContent, list, "button", "box", listStyle,
		                callBack);
	}
	
	public static bool Dropdown (Rect position, bool showList, GUIContent buttonContent,  IList list,
	                             GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle, Action<int> callBack) {
		
		
		int controlID = GUIUtility.GetControlID(popupListHash, FocusType.Passive);
		bool done = false;
		switch (Event.current.GetTypeForControl(controlID)) {
		case EventType.mouseDown:
			if (position.Contains(Event.current.mousePosition)) {
				GUIUtility.hotControl = controlID;
				showList = true;
			}
			break;
		case EventType.mouseUp:
			if (showList) {
				done = true;
			}
			break;
		}
		
		GUI.Label(position, buttonContent, buttonStyle);
		if (showList) {
			
			// Get our list of strings
			string[] text = new string[list.Count];
			// convert to string
			for (int i =0; i<list.Count; i++)
			{
				text[i] = list[i].ToString();
			}
			
			Vector2 screenWindowPos = GUIUtility.GUIToScreenPoint(new Vector2(position.x,position.y));
			Rect windowRect = 
				new Rect((int)(screenWindowPos.x), (int)(screenWindowPos.y), position.width, list.Count * 20);
			GUI.Window(GUIUtility.GetControlID(FocusType.Passive),windowRect,delegate(int id) {
				Rect listRect = new Rect(0,0,windowRect.width,windowRect.height);
				GUI.Box(listRect,"", boxStyle);
				int listEntry=-1;
				Vector2 mousePosition = Event.current.mousePosition;
				if (listRect.Contains(mousePosition)){
					listEntry = (int)(mousePosition.y/20);
					lastSelectedCache[controlID] = listEntry;
				}
				//Debug.Log("List entry = "+listEntry);
				GUI.SelectionGrid(listRect,listEntry, text, 1, listStyle);
				
			},"");
			
			
		}
		if (done) {
			showList = false;
			callBack(lastSelectedCache[controlID]);
			lastSelectedCache.Remove(controlID);
		}
		return showList;
	}
}

public class GUILayoutEXT {
	public static bool Dropdown (bool showList,  GUIContent buttonContent, IList list,
	                             Action<int> callback ) {
		
		return Dropdown(showList, buttonContent, list, "button", "box", GUIEXT.inbuiltListStyle,
		                callback);
	}
	
	public static bool Dropdown (bool showList, GUIContent buttonContent, IList list ,
	                             GUIStyle listStyle, Action<int> callback) {
		
		
		
		return Dropdown(showList, buttonContent, list, "button", "box", listStyle,  callback);
	}
	
	public static bool Dropdown (bool showList, GUIContent buttonContent,  IList list,
	                             GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle,
	                             Action<int> callback) {
		Vector2 sz = buttonStyle.CalcSize(buttonContent);
		Rect r = GUILayoutUtility.GetRect(sz.x,sz.y);
		
		return GUIEXT.Dropdown(r, showList, buttonContent, list, "button", "box", listStyle,
		                       callback);
	}
	
}