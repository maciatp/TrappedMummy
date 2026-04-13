using UnityEngine;
using System.Collections;

public class PlataformaMovil : MonoBehaviour 
{
	private int nextPoint;
	private Vector3 initialPos;
	public Transform [] patrols;
	private Vector3 targetPatrol;
	private float velocity = 0.5F;
	public float relativeVelocity = 10f;
	private float InitialVelocity;
	
	private Vector3 relativePos;
	private float distanceToPoint;
	private float distanceToPlayer;
	private int state = 0;
	
	private float stopTime;
	
	private Vector3 direction = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () 
	{
		initialPos = transform.position;
		InitialVelocity = relativeVelocity;
		nextPoint = 0;
		ChangePatrolPoint();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Move
		distanceToPoint = (targetPatrol - transform.position).magnitude;
		if(distanceToPoint < 0.5f)
		{
			ChangePatrolPoint();
		}
		else
		{	
			if(Time.time > stopTime)
			{
				GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + direction * Time.deltaTime * (relativeVelocity));	
			}
		}
		distanceToPlayer = (Generico.Player.transform.position - transform.position).magnitude;
	}
	
	private void ChangePatrolPoint ()
	{
		if(nextPoint != patrols.Length)
		{
			targetPatrol = patrols[nextPoint].position;
			nextPoint++;
		}
		else
		{
			targetPatrol = initialPos;
			nextPoint = 0;
		}
		stopTime = Time.time + 0.4f;
		direction = (targetPatrol - transform.position).normalized;
		
	}

}
