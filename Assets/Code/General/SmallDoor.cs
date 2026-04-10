using UnityEngine;
using System.Collections;

public class SmallDoor : MonoBehaviour 
{
	
	public void Open()
	{
		//Temporarily destroy
		audio.Play();
		animation.Play("PuertaAbrir");
	}
}
