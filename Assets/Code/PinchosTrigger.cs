using UnityEngine;
using System.Collections;

public class PinchosTrigger : MonoBehaviour 
{
	public float segundosAviso = 1f;
	public float segundosAbajo = 2f;
	public float segundosArriba = 2f;
	private bool loopend = false;
	private bool damage = false;
	
	
	// Use this for initialization
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerControl>() != null)
		{
			StartCoroutine(Routine());	
		}
	}
	
	IEnumerator Routine()
	{
		yield return new WaitForSeconds(segundosAviso);
		this.GetComponent<Animation>().Play("PinchosArriba");
		GetComponent<AudioSource>().Play();
		damage=true;
		yield return new WaitForSeconds(segundosAbajo);
		this.GetComponent<Animation>().Play("PinchosAbajo");
		damage=false;
		yield return new WaitForSeconds(segundosArriba);
		loopend = true;
	}
}
