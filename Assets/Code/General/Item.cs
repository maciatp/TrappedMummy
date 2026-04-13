using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour 
{

	private bool picked = false;
	public int velocity = 70;
	
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerControl>() != null && !picked)
		{
			switch(Generico.PlayerControl.itemsActuales)
			{
				case 0:
					GetComponent<AudioSource>().clip = sound1;
					break;
				
				case 1:
					GetComponent<AudioSource>().clip = sound2;
					break;
					
				case 2:
					GetComponent<AudioSource>().clip = sound3;
					break;
				
			}
			GetComponent<AudioSource>().Play();
			Generico.PlayerControl.itemsActuales++;	
			picked = true;
			gameObject.SetActiveRecursively(false);
			
		}
	}
}
