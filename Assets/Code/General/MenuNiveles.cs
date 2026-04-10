using UnityEngine;
using System.Collections;

public class MenuNiveles: MonoBehaviour 
{
	public GUISkin skin;
	// Use this for initialization
	void Start () 
	{
		Generico.timer.GetComponent<Timer>().StopTime();
	}
	
	void OnGUI () 
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (Screen.width/(2)-Screen.width/15, Screen.height/4.5f,Screen.width/6,Screen.height/3.5f), "")) 
		{
			Application.LoadLevel("TutoScene");
		}
		
		if (GUI.Button (new Rect (Screen.width/(2)-Screen.width/6.5f, Screen.height/1.9f,Screen.width/6f,Screen.height/3.5f), "")) 
		{
			Application.LoadLevel("Nivel2");
		}
		if (GUI.Button (new Rect (Screen.width/(2)+Screen.width/(50), Screen.height/1.9f,Screen.width/5,Screen.height/3.5f), "")) 
		{
			Application.LoadLevel("Nivel3");
		}
		if (GUI.Button (new Rect (0, Screen.height - Screen.height/5,Screen.width/6,Screen.height/3.5f), "")) 
		{
			Application.LoadLevel("menu_inicio");
		}
		
	}
}
