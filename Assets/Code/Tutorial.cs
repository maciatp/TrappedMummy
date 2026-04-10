using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour 
{

	public GUISkin skin;
	public Texture2D [] textures;
	public Material material;
	
	private int contador = 0;
	
	void Start()
	{
			material.mainTexture = textures[0];
	}
	
	// Use this for initialization
	void OnGUI()
	{
		GUI.skin = skin;
		if (GUI.Button (new Rect (0, 0,Screen.width,Screen.height), "")) 
		{
			if(contador < textures.Length-1)
			{
				contador++;
				material.mainTexture = textures[contador];
				print("contador: " + contador + " length: " + textures.Length);
			}
			else
			{
				Application.LoadLevel("Nivel1");
			}
		}
		
	}
}
