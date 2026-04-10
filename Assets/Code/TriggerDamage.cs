using UnityEngine;
using System.Collections;

public class TriggerDamage : MonoBehaviour 
{

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerControl>() != null)
		{
			//se reinicia el nivel
			print("trigger");
			Generico.PlayerControl.Respawn();
		}
	}
}
