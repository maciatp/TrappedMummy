using UnityEngine;
using System.Collections;

public class MenuFinNivel : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Generico.timer.GetComponent<Timer>().StopTime();
	}
	
	void OnGUI () 
	{
		// Make a background box
		GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "NIVEL COMPLETADO");
		
		GUI.Label(new Rect(50, 50, 150, 50), "Items Conseguidos: " + Generico.PlayerControl.itemsActuales + "/" + Generico.PlayerControl.itemsTotales);
		GUI.Label(new Rect(90, 70, 150, 50), "Tiempo: " + Generico.timer.GetComponent<TextMesh>().text);

		// Botón reintentar nivel
		if (GUI.Button (new Rect (20,Screen.height - 50,130,30), "Reintentar")) 
		{
			Application.LoadLevel (Application.loadedLevelName);
		}
	
		// Botón Siguiente Nivel
		if (GUI.Button (new Rect (Screen.width - 150,Screen.height - 50,130,30), "Siguiente Nivel")) 
		{
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
}
