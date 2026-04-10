using UnityEngine;
using System.Collections;

public class PatrolFollowEnemy : MonoBehaviour 
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
	public int radio = 20;
	private int state = 0;
	private bool alert = false;
	
	private RaycastHit hit;
	
	private int radioBase;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () 
	{
		initialPos = transform.position;
		nextPoint = 0;
		controller = GetComponent<CharacterController>();
		animation["Walk"].speed = 3;
		radioBase = radio;
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
			relativePos = new Vector3(targetPatrol.x - transform.position.x, transform.position.y+7, targetPatrol.z - transform.position.z);
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
				PredictCollision();
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				
				controller.Move(forward * velocity * Time.deltaTime);
			}
			break;
			
			case 2:
			//Alert
			relativePos = new Vector3(Generico.Player.transform.position.x - transform.position.x, transform.position.y+7, Generico.Player.transform.position.z - transform.position.z);
			rotation = Quaternion.LookRotation(relativePos);
			angle = Quaternion.Angle(rotation, transform.rotation);
			radio = radioBase + 2;
			if(rotation.eulerAngles != transform.eulerAngles && angle > 5f)
			{
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.time * rotspeed/100);
			}
			else
			{
				transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.time * rotspeed/100);
				PredictCollision();
				Vector3 forward = transform.TransformDirection(Vector3.forward);
				controller.Move(forward * velocity * Time.deltaTime);
			}
			
			float ySeparation = Mathf.Abs(Generico.Player.transform.position.y - transform.position.y);
			if(distanceToPlayer > radio || ySeparation > 0.7f)
			{
				alert = false;
				//radio /= 2;
				state = 0;
			}
			break;
		}
		
		distanceToPlayer = (Generico.Player.transform.position - transform.position).magnitude;
		
		if(distanceToPlayer < radio)
		{
			float ySeparation = Mathf.Abs(Generico.Player.transform.position.y - transform.position.y);
			if(!alert && hit.transform.gameObject == Generico.Player.gameObject && ySeparation < 0.7f)
			{
				Alert();	
			}
		}		
		if(distanceToPlayer < 2)
		{
			killPlayer();
		}
		
	}
	
	private void Alert()
	{
		state = 2;
		alert = true;
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
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, radio);	
	}
	
	void PredictCollision()
	{
		Vector3 centro = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
		if (Physics.Raycast (centro, Generico.Player.transform.position - transform.position, out hit, Mathf.Infinity)) 
		{
			Debug.DrawRay(centro, Generico.Player.transform.position - transform.position);
		}
	}
}
