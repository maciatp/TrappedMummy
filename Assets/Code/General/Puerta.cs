using UnityEngine;
using System.Collections;

public class Puerta : MonoBehaviour 
{
	// Update is called once per frame
	void Update () 
	{
		if(Generico.PlayerControl.llavesActuales == Generico.PlayerControl.llavesNecesarias)
		{
			print("open");
			animation.Play("open");
			audio.Play();
			//animation.Play("open");
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(Generico.PlayerControl.llavesActuales == Generico.PlayerControl.llavesNecesarias)
		{
				Generico.PlayerControl.Freeze(true);
				Generico.score.SetActiveRecursively(true);
		}
	}
}
