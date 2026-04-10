using UnityEngine;
using System.Collections;

public class JoystickFrame : MonoBehaviour 
{

	public CustomJoystick Joystick;
	private GUITexture gui;   
	
	void Start()
	{
		gui = (GUITexture)GetComponent(typeof(GUITexture));	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Joystick.initPos != Vector2.zero)
		{
			gui.enabled = true;
			gui.pixelInset = new Rect(Joystick.initPos.x, Joystick.initPos.y, 100, 100);
			
		}
		else
		{
			gui.enabled = false;	
		}
	}
}
