using UnityEngine;
using System.Collections;

public class SmallDoor : MonoBehaviour 
{
	
	public void Open()
	{
		//Temporarily destroy
		GetComponent<AudioSource>().Play();
		GetComponent<Animation>().Play("PuertaAbrir");
	}
}
