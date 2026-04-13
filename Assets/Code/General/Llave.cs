using UnityEngine;
using System.Collections;

public class Llave : MonoBehaviour 
{
	private bool tieneLlave = false;
	private int velocity = 70;
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == Generico.Player)
		{
			if(!tieneLlave)
			{
				Generico.PlayerControl.llavesActuales++;	
				GetComponent<MeshRenderer>().enabled = false;
				GetComponent<AudioSource>().Play();
				tieneLlave = true;
			}
		}
	}
	
	void FixedUpdate()
	{
		transform.Rotate(Vector3.forward*Time.deltaTime*velocity);	   
	}
}
