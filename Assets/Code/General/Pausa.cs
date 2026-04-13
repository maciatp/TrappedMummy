using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
	public GUISkin skin;
	private Image button;
    
    public void OnEnable ()
    {
		Generico.joystick.transform.parent.gameObject.SetActive(false);
		if(!Generico.mute)
		{
			transform.Find("Mute").GetComponent<Renderer>().enabled = false; 	
		}
		else
		{
			transform.Find("Mute").GetComponent<Renderer>().enabled = true;
		}
		Generico.timer.GetComponent<Timer>().StopTime();
        button = this.GetComponent<Image>();
		//button.pixelInset = new Rect(0, 152.4541f,Screen.width,-9.03f);
            
    }
    
	//recoge todos los touchs de la pantalla y si alguno esta dandose encima del GUITexture devuelve true
	void OnGUI()
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (Screen.width/(1.2f), Screen.height/9.5f,Screen.width/5,Screen.height/5), "")) 
		{
			//niveles
			SceneManager.LoadScene("menu_niveles");
		}
		if (GUI.Button (new Rect (Screen.width/(1.2f), Screen.height/3.2f,Screen.width/5,Screen.height/5), "")) 
		{
            //reintentar
            SceneManager.LoadScene(Application.loadedLevel);
		}
		if (GUI.Button (new Rect (Screen.width/(1.2f), Screen.height/1.95f,Screen.width/5,Screen.height/5), "")) 
		{
			//MUTE BUTTON
			if(Generico.mute)
			{
				transform.Find("Mute").GetComponent<Renderer>().enabled = false; 	
				Generico.mute = false;
			}
			else
			{
				transform.Find("Mute").GetComponent<Renderer>().enabled = true;
				Generico.mute = true;
			}
		}
		
		if (GUI.Button (new Rect (Screen.width/(1.2f), Screen.height-Screen.height/3.5f,Screen.width/5,Screen.height/5), "")) 
		{
			//tutorial
			Application.LoadLevel("TutoScene");
		}
		if (GUI.Button (new Rect (0, Screen.height - (Screen.height/5.3f),Screen.width/4.5f,Screen.height/5), "")) 
		{
			Generico.timer.GetComponent<Timer>().ResumeTime();
			Generico.joystick.transform.parent.gameObject.SetActiveRecursively(true);
			this.gameObject.SetActiveRecursively(false);
		}
		
	}

}//=)