using UnityEngine;
using System.Collections;

public class PlataformaTemporal : MonoBehaviour 
{
	public float segundos = 2f;
	public float aviso = 0.5f;
	
	private TriggerPlataformaMovil trigger;
	private bool activated = false;
	private bool falling = false;
	
	// Use this for initialization
	void Start () 
	{
		trigger = GetComponentInChildren<TriggerPlataformaMovil>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(trigger.GetColliding() && !activated)
		{
			activated = true;
			StartCoroutine(Warning());
			
		}
	}
	
	void FixedUpdate()
	{
		
	}
	
	IEnumerator Warning()
	{
		this.animation.Play("BloqueTemblor");
		yield return new WaitForSeconds(segundos);
		falling = true;
		this.animation.Play("BloqueCaida");
		yield return new WaitForSeconds(1);
	}
}
