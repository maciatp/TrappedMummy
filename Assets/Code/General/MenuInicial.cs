using UnityEngine;
using System.Collections;

public class MenuInicial : MonoBehaviour 
{
	public GUISkin customSkin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI () 
	{	
		GUI.skin = customSkin;
		// Botón Siguiente Nivel
		if (GUI.Button (new Rect (Screen.width/(4f), Screen.height/7,540,250), "")) 
		{
			Application.LoadLevel("menu_niveles");
		}
	}
}
