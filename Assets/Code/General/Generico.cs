using UnityEngine;
using System.Collections;

public class Generico : MonoBehaviour {

	public static GameObject Player;
	public GameObject PlayerAux;
	public static PlayerControl PlayerControl;
	public static float playerSpeed = 10f;
	public float playerSpeedAux = 10F;
	
	public static GameObject direccionMovimiento;
	public GameObject direccionMovimientoAux;
	
	public static GameObject camara;
	public GameObject camaraAux;
	
	public static GameObject HUD;
	public GameObject HUDAux;
	
	public static CustomJoystick joystick;
	public CustomJoystick joystickAux;
	
	public static GameObject timer;
	public GameObject timerAux;
	
	public static GameObject menuFinNivel;
	public GameObject menuFinNivelAux;
	
	public static GameObject score;
	public GameObject scoreAux;
	
	public static GameObject[] buttons;
	public GameObject[] buttonsAux;
	
	public static bool mute;
	public bool muteAux = false;
	
	void Start()
	{
		Player = PlayerAux;
		camara = camaraAux;
		direccionMovimiento = direccionMovimientoAux;
		PlayerControl = Player.GetComponent<PlayerControl>();
		joystick = joystickAux;
		playerSpeed = playerSpeedAux;
		timer = timerAux;
		menuFinNivel = menuFinNivelAux;
		buttons = buttonsAux;
		score = scoreAux;
		if(mute == null)
			mute = muteAux;
		
		ApplySettings();
	}
	
	public void ApplySettings()
	{
		if(mute)
		{
			AudioListener.volume = 0;
		}
		else
		{
			AudioListener.volume = 1;
		}
	}
	
}