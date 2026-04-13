using UnityEngine;
using System.Collections;

public class LlaveTemporal : MonoBehaviour 
{
	private bool tieneLlave = false;
	private int velocity = 70;
	private SmallDoor doorScript;
	public GameObject door;
	
	void Start()
	{
		doorScript = door.GetComponent<SmallDoor>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == Generico.Player)
		{
			
			if(!tieneLlave)
			{
				GetComponent<AudioSource>().Play();
				doorScript.Open();
				GetComponent<MeshRenderer>().enabled = false;
				tieneLlave = true;
			}
		}
	}
	
	void FixedUpdate()
	{
		transform.Rotate(Vector3.forward*Time.deltaTime*velocity);	   
	}
}
