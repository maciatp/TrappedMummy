using UnityEngine;
using System.Collections;

public class Puente : MonoBehaviour 
{

	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == Generico.Player)
		{
			audio.Play();
			animation.Play("Puente");
		}
		else
		{
			print(other);	
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == Generico.Player)
		{
			audio.Stop();
			animation.Stop();	
		}
	}
}
