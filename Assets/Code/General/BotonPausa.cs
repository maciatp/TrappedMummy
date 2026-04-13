using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BotonPausa : MonoBehaviour
{
	public GUISkin skin;
	private Image button;
	public GameObject MenuPausa;
    
    public void Start ()
    {
        button = this.GetComponent<Image>();
            
    }
    
	//recoge todos los touchs de la pantalla y si alguno esta dandose encima del GUITexture devuelve true
	void OnGUI()
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (0, 0,Screen.width/5.5f,Screen.height/4f), "")) 
		{
			MenuPausa.SetActive(true);
		}
	}

}//=)