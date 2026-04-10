using UnityEngine;
using System.Collections;

public class Abismo : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == Generico.Player)
        Generico.PlayerControl.Respawn();
	}
}
