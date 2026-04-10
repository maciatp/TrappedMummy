using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
	private GUITexture button;
    
    public void Start ()
    {
            button = this.GetComponent<GUITexture>();
		button.pixelInset = new Rect(Screen.width/2, 0,Screen.width,Screen.height);
            
    }
    
	//recoge todos los touchs de la pantalla y si alguno esta dandose encima del GUITexture devuelve true
	public bool isTouching()
	{
		
		Touch touch;
		int totalTouchs = Input.touchCount;
         
		for(int i=0;i<totalTouchs;i++)
		{
			touch = Input.GetTouch(i);
			if(touch.position.x > Screen.width/2 && touch.fingerId != Generico.joystick.LatchedFinger())
	        {
	           return true;
	        }	
		}
		return false;
		
	}

}//=)