using UnityEngine;
using System.Collections;

public class PatrolingEnemy : MonoBehaviour 
{
	private int nextPoint;
	private Vector3 initialPos;
	public Transform [] patrols;
	private Vector3 targetPatrol;
	public float rotspeed = 1F;
	private float velocity = 0F;
	public float relativeVelocity;
	
	private Vector3 relativePos;
	private Quaternion rotation;
	private float distanceToPoint;
	private float distanceToPlayer;
	private int state = 0;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () 
	{
		initialPos = transform.position;
		nextPoint = 0;
		controller = GetComponent<CharacterController>();
		ChangePatrolPoint();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		velocity = relativeVelocity + Generico.playerSpeed;
		switch(state)
		{
			case 0:
			
			//Rotate
			relativePos = new Vector3(targetPatrol.x - transform.position.x, transform.position.y+9, targetPatrol.z - transform.position.z);
			rotation = Quaternion.LookRotation(relativePos);
			float angle = Quaternion.Angle(rotation, transform.rotation);
			if(rotation.eulerAngles != transform.eulerAngles && angle > 0.2f)
			{
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.time * rotspeed/100);
			}
			else
				state = 1;
			break;
			
			case 1:
			
			//Move
			distanceToPoint = (targetPatrol - transform.position).magnitude;
			if(distanceToPoint < 0.5f)
			{
				ChangePatrolPoint();
				state = 0;
			}
			else
			{	
				Vector3 forward = transform.TransformDirection(new Vector3(0,-1, 1));
				controller.Move(forward * velocity * Time.deltaTime);
			}
			break;
		}
		distanceToPlayer = (Generico.Player.transform.position - transform.position).magnitude;
		if(distanceToPlayer < 2)
		{
			killPlayer();
		}
		
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
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerControl>() != null)
		{
			//se reinicia el nivel
			killPlayer();
		}
	}
	
	void killPlayer()
	{
		Generico.PlayerControl.Respawn();
	}

}
