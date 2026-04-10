using UnityEngine;
using System.Collections;

public class TriggerPlataformaMovil : MonoBehaviour 
{
	private bool colliding = false;

	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == Generico.Player)
		{
			colliding = true;
			other.transform.parent = gameObject.transform; 
		}
	} 
	void OnTriggerExit (Collider other) 
	{ 
		if(other.gameObject == Generico.Player)
		{
			colliding = false;
			other.transform.parent = null; 
		}
	}
	
	public bool GetColliding()
	{
		return colliding;	
	}
}
