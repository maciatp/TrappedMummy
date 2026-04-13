using UnityEngine;
using System.Collections;

public class Puente : MonoBehaviour 
{

	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == Generico.Player)
		{
			GetComponent<AudioSource>().Play();
			GetComponent<Animation>().Play("Puente");
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
			GetComponent<AudioSource>().Stop();
			GetComponent<Animation>().Stop();	
		}
	}
}
