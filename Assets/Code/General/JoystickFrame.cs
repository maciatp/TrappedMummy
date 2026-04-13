using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoystickFrame : MonoBehaviour 
{

	public CustomJoystick Joystick;
	private Image gui;   
	
	void Start()
	{
		gui = (Image)GetComponent(typeof(Image));	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Joystick.initPos != Vector2.zero)
		{
			gui.enabled = true;
			gui.rectTransform.rect.Set(Joystick.initPos.x, Joystick.initPos.y, 100, 100);
			
		}
		else
		{
			gui.enabled = false;	
		}
	}
}
