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
					audio.clip = sound1;
					break;
				
				case 1:
					audio.clip = sound2;
					break;
					
				case 2:
					audio.clip = sound3;
					break;
				
			}
			audio.Play();
			Generico.PlayerControl.itemsActuales++;	
			picked = true;
			gameObject.SetActiveRecursively(false);
			
		}
	}
}
