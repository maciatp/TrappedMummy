using UnityEngine;
using System.Collections;

public class PinchosRoutine : MonoBehaviour 
{
	public float segundosAbajo = 2f;
	public float segundosArriba = 2f;
	private bool loopend = false;
	private bool damage = false;
	
	
	// Use this for initialization
	void Start () 
	{
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
		this.animation.Play("PinchosArriba");
		audio.Play();
		damage=true;
		yield return new WaitForSeconds(segundosAbajo);
		this.animation.Play("PinchosAbajo");
		damage=false;
		yield return new WaitForSeconds(segundosArriba);
		loopend = true;
	}
}
