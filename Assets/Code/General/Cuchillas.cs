using UnityEngine;
using System.Collections;

public class Cuchillas : MonoBehaviour 
{
	public float segundosAbajo = 2f;
	public float segundosArriba = 2f;
	private bool loopend = false;
	private bool damage = false;
	private BoxCollider trigger;
	
	
	// Use this for initialization
	void Start () 
	{
		trigger = GetComponentInChildren<BoxCollider>();
		StartCoroutine(Routine());
	}
	
	void FixedUpdate()
	{
		if(loopend)
		{
			StartCoroutine(Routine());
			loopend = false;
		}
	}
	
	IEnumerator Routine()
	{
		this.GetComponent<Animation>().Play("CuchillasBajan");
		GetComponent<AudioSource>().Play();
		//this.audio.Play();
		trigger.enabled = true;
		yield return new WaitForSeconds(segundosAbajo);
		this.GetComponent<Animation>().Play("CuchillasSuben");
		trigger.enabled = false;
		yield return new WaitForSeconds(segundosArriba);
		loopend = true;
	}
}
