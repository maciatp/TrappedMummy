using UnityEngine;
using System.Collections;

public class CustomJoystick : MonoBehaviour 
{

    static private float tapTimeDelta = 0.3f;               // Time allowed between taps

    public Rect touchZone;
    public Vector2 deadZone = Vector2.zero;                     // Control when position is output
    public Vector2 position;                                    // [-1, 1] in x,y
    public int tapCount;                                            // Current tap count
 
	public Vector2 initPos;
	public Vector2 finalPos;

	private bool hasMoved = false;
    private int lastFingerId = -1;                              // Finger last used for this joystick
    private Vector2 fingerDownPos;
    private float fingerDownTime;
    private float firstDeltaTime = 0.5f;
 
    private GUITexture gui;                             // Joystick graphic
    private Rect defaultRect;                               // Default position / extents of the joystick graphic
    private Boundary guiBoundary = new Boundary();          // Boundary for joystick graphic
    private Vector2 guiTouchOffset;                     // Offset to apply to touch input
    private Vector2 guiCenter;                          // Center of joystick
 
    private Vector3 tmpv3;
    private Rect tmprect;
    private Color tmpclr;
 
    public void Start()
    {
        // Cache this component at startup instead of looking up every frame   
        gui = (GUITexture) GetComponent( typeof(GUITexture) );
       
        // Store the default rect for the gui, so we can snap back to it
        defaultRect = gui.pixelInset;   
    }
 
    public void Disable()
    {
        gameObject.active = false;
    }
 
    public void ResetJoystick()
    {
        // Release the finger control and set the joystick back to the default position
        lastFingerId = -1;
        position = Vector2.zero;
        fingerDownPos = Vector2.zero;
		initPos = Vector2.zero;
		finalPos = Vector2.zero;
       	hasMoved = false;
		//MIO
		gui.enabled = false;

    }
 
    public bool IsFingerDown()
    {
        return (lastFingerId != -1);
    }
       
    public int LatchedFinger ()
    {
        return lastFingerId;
    }
 
    public void FixedUpdate()
    {   
		
        int count = Input.touchCount;
       
        if ( count == 0 )
            ResetJoystick();
        else
        {
            for(int i = 0;i < count; i++)
            {
                Touch touch = Input.GetTouch(i);  
				
       
                bool shouldLatchFinger = false;
                if (touch.position.x < Screen.width/2)
                {
                    shouldLatchFinger = true;
                }  
       
                // Latch the finger if this is a new touch
                if ( shouldLatchFinger && ( lastFingerId == -1 || lastFingerId != touch.fingerId ) )
                {   
                    lastFingerId = touch.fingerId;
				
					fingerDownPos = touch.position;
					//IMPORTANTE
					hasMoved = false;
				
					//MIO
					gui.enabled = true;
					
					defaultRect = new Rect(touch.position.x - 50, touch.position.y - 50, 100, 100);
					
					gui.pixelInset = defaultRect;					
                                   
                }              
       
                if ( lastFingerId == touch.fingerId )
                {   
				
					//INITIAL AND FINAL POSITION SETTINGS
                    if((touch.position - fingerDownPos).magnitude > 40)
					{
						initPos = fingerDownPos;
						finalPos = touch.position;
						fingerDownPos = touch.position;
					}
					if((touch.position - finalPos).magnitude > 1 && finalPos != Vector2.zero)
					{
						finalPos = touch.position;	
					}
             
                        // Change the location of the joystick graphic to match where the touch is
                        tmprect = gui.pixelInset;
						tmprect.x = touch.position.x;
						tmprect.y = touch.position.y;
                        gui.pixelInset = tmprect;
                   
                    if ( touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled )
					{
                            ResetJoystick();   
					}
                }          
            }
	}
		//Set Position
		if((finalPos - initPos).magnitude > 5)
		{
			position = finalPos - initPos;
			position = position.normalized;
		}
		else
		{
			position = Vector2.zero;	
		}
    }
 
}