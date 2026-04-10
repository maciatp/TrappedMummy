using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
	float startTime;
	
	
	void Awake() 
	{
		Time.timeScale = 1;
		startTime = Time.time;
	}
	
	
	void Update () 
	{		
		float guiTime = Time.time - startTime;
		
		float minutes = guiTime / 120;
		float seconds = guiTime % 60;
		float fraction = (guiTime * 10) % 10; 
		
		GetComponent<TextMesh>().text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
		
	}
	
	public void StopTime()
	{
		AudioListener.volume = 0;
		Time.timeScale = 0;
	}
	
	public void ResumeTime()
	{
		if(!Generico.mute)
			AudioListener.volume = 1f;
		Time.timeScale = 1;
	}
}
