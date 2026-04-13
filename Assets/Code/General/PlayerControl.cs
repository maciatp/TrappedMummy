using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	
	public float speed = 10.0f;
	
	public float jumpSpeed = 10.0f;
	public float maxDistanceJump = 2.0f;
	//suele quedar mejor que la gravedad sea igual que jumpSpeed IPD20110930
	public float gravity = 10.0f;
	public int Health = 3;

	public int Damage = 0;
	
	private bool frozen = false;
	
	public bool actionJump = false;
	
	public int llavesNecesarias = 1;
	public int llavesActuales = 0;
	
	public int itemsTotales = 3;
	public int itemsActuales = 0;
	
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 dirInput = Vector3.zero;
	private float distanceToGround;
	private bool onGround = false;
	
	private Transform cameraRelative;
	
	//es la minima distancia entre el suelo y el player IPD20111001
	private const float MIN_DISTANCE_GROUND = 0.15f;
	
	private CharacterController controller;

	private RaycastHit hit;
	
	private Vector3 initialPosition = Vector3.zero;
	
	
	public AudioClip SonidoPasos;
	public AudioClip SonidoSalto;
	
	
	void Start()
	{
		controller = GetComponent<CharacterController>();
		GetComponent<Animation>()["Walk"].speed = 2;
	}
	
	/*void Update() 
	{
		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) 
		{
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;
		if (Input.GetButton("Jump"))
		moveDirection.y = jumpSpeed;
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}*/

	void FixedUpdate() 
	{	
			dirInput = Vector3.zero;
			predictCollision();
			moveDirection.x = 0;
			moveDirection.z = 0;
			
			if(!frozen)
			{
				getKeyboardInput();
				getJoystickInput();
			}
			move();
		
			FaceMovementDirection();
	}
	
	
	
	void predictCollision()
	{
		if (Physics.Raycast (transform.position, Vector3.down, out hit, Mathf.Infinity)) 
		{
			Debug.DrawLine (hit.transform.position, hit.point);
			if(hit.distance < MIN_DISTANCE_GROUND)
			{
				onGround = true;
			}
			else
			{
				onGround = false;
			}
		}
	}

	void move()
	{

			if (onGround) 
			{
				groundMove();

			}//if
			else
			{
				airMove();
			}//else
		
		convertCameraRelative();
		
		controller.Move(moveDirection * Time.deltaTime);
		
	}//move

	void groundMove()
	{
		if(dirInput != Vector3.zero)
		{
			
			GetComponent<Animation>().Play("Walk");
			//WALK SOUND
			GetComponent<AudioSource>().clip = SonidoPasos;
			GetComponent<AudioSource>().loop = true;
			if(!GetComponent<AudioSource>().isPlaying)
			{
				GetComponent<AudioSource>().Play();
			}
			
			moveDirection = dirInput;
		}
		else
		{
			GetComponent<Animation>().Play("Idle");	
			GetComponent<AudioSource>().Stop();
		}

		moveDirection.y = -gravity/10;
		moveDirection *= speed;
		
		if(Input.GetButton ("Jump") || Generico.buttons[0].GetComponent<Jump>().isTouching())
		{
			GetComponent<AudioSource>().clip = SonidoSalto;
			GetComponent<AudioSource>().loop = false;
			GetComponent<AudioSource>().Play();
			actionJump = true;
			moveDirection.y = jumpSpeed;
			transform.position = new Vector3(transform.position.x, transform.position.y + MIN_DISTANCE_GROUND, transform.position.z); 
			initialPosition = transform.position;
		}
	}//groundMove
	
	void airMove()
	{
		GetComponent<Animation>().Play("Jump");
		if(dirInput != Vector3.zero)
		{
			moveDirection.x = dirInput.x;
			moveDirection.z = dirInput.z;
		}
		
		moveDirection.x *= speed;
		moveDirection.z *= speed;

		//Gravedad
		if((actionJump && (transform.position.y - initialPosition.y) > maxDistanceJump && moveDirection.y > -gravity) || moveDirection.y > -gravity)
		{
			actionJump = false;
			moveDirection.y -= (gravity * Time.deltaTime);
			
		}
	}//airMove


	void convertCameraRelative()
	{
		//Hace que la rotación sea relativa a la deseada
		//Se observa la rotación del objeto de dirección y se le multiplica a la rotación deseada para hacerla 
		//relativa a esta
		Quaternion quat = Quaternion.AngleAxis(Generico.direccionMovimiento.transform.rotation.eulerAngles.y,Vector3.up);
		moveDirection = quat * moveDirection;
	}
	
   	void FaceMovementDirection()
    {
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity.y = 0.0f;
        if( horizontalVelocity.magnitude > 0.1f )
        {
        	transform.forward = horizontalVelocity.normalized;
        }
    }
	
	void getKeyboardInput()
	{
		if((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !frozen)
		{
			if(Input.GetAxis("Horizontal") != 0)
			{
				if(Input.GetAxis("Horizontal") < 0)
				{
					dirInput.x = -1;
				}
				else
				{
					dirInput.x = 1;
				}
			}//if
			if(Input.GetAxis("Vertical") != 0 )
			{
				if(Input.GetAxis("Vertical") < 0)
				{
					dirInput.z = -1;
				}//if
				else
				{
					dirInput.z = 1;
				}//else
			}//if
		}//if
	}
	
	void getJoystickInput()
	{		
		//si usa el joystick
		if(Generico.joystick != null)
		{
			if(Generico.joystick.position != Vector2.zero)
			{
				print("I'm reading: " + Generico.joystick.position);
				dirInput.x = Generico.joystick.position.x;
				//se iguala a la z por que es la profundidad en el escenario y el joystick esta dibujado en vertical
				dirInput.z = Generico.joystick.position.y;
				
			}
		}
	}
	
	public void Respawn()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
	
	public void Freeze (bool frz)
	{
		frozen = frz;	
	}
	
	public void TempFreeze(float time)
	{
			StartCoroutine(FreezeTime(time));
	}
	
	public IEnumerator FreezeTime(float time)
	{
		frozen = true;
		yield return new WaitForSeconds(time);
		frozen = false;
	}
	
	
}