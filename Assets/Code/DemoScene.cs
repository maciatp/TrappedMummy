using UnityEngine;
using System.Collections;

public class DemoScene : MonoBehaviour 
{
	public GUISkin skin;
	
	// Use this for initialization
	void OnGUI()
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (0, 0,Screen.width,Screen.height), "")) 
		{
			Application.LoadLevel("menu_inicio");
		}
		
	}
}
