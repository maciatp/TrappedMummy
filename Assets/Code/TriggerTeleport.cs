using UnityEngine;
using System.Collections;

public class TriggerTeleport : MonoBehaviour 
{
	private Transform target;
	public Transform camaraPos;
	
	void Start()
	{
		target = transform.GetChild(0);
	}
	
	void OnTriggerEnter (Collider other) 
	{
		if(other.gameObject == Generico.Player)
		{
			if(camaraPos!=null)
			{
				Generico.camara.transform.position = camaraPos.position;
			}
			Generico.Player.GetComponent<PlayerControl>().TempFreeze(0.3f);
			Generico.Player.transform.position = target.position;
		}
	} 
}
