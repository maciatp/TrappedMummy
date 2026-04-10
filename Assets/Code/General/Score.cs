using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public GUISkin skin;
	private GUITexture button;
	public Transform star1;
	public Transform star2;
	public Transform star3;
	public Transform text;
	public float minTime;
	public Transform pauseButton;
	
	private float sumapuntos = 0;
    
    void OnEnable () 
	{
		pauseButton.gameObject.SetActiveRecursively(false);
		Generico.timer.GetComponent<Timer>().StopTime();
		Generico.joystick.transform.parent.gameObject.SetActiveRecursively(false);
		star2.renderer.enabled = false;
		star3.renderer.enabled = false;
		
		int items = Generico.PlayerControl.itemsActuales;
		print("items: " + items);
		switch(items)
		{
			case 0:
			break;
			
			case 1:
			sumapuntos += 2000;
			break;
			
			case 2:
			sumapuntos += 4000;
			break;
			
			case 3:
			sumapuntos += 6000;
			print("Sumapuntos: " + sumapuntos);
			break;
		}
		
		if(Time.timeSinceLevelLoad <= minTime)
		{
			sumapuntos += 2000;
			sumapuntos += (minTime - (int)Time.timeSinceLevelLoad)*163;
			print("Sumapuntos: " + sumapuntos);
		}
		
		if(sumapuntos >= 1000)
		{
			star2.renderer.enabled = true;
		}
		if(sumapuntos >= 2000)
		{
			star3.renderer.enabled = true;	
		}
		print("Sumapuntos: " + sumapuntos);
		text.GetComponent<TextMesh>().text = sumapuntos.ToString();
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (Screen.width/(1.4f), Screen.height/3.2f,Screen.width/5,Screen.height/5), "")) 
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
		if (GUI.Button (new Rect (Screen.width/(1.4f), Screen.height/1.8f,Screen.width/5,Screen.height/5), "")) 
		{
			Application.LoadLevel(Application.loadedLevel+1);
		}
		
	}

}//=)