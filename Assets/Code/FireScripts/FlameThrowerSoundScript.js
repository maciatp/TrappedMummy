// this script is used on an object that emits the sound of the flame thrower

function Update ()
{

		// the sound fades in and out gradually
	if (Input.GetButton("Fire1"))
		audio.volume = Mathf.Clamp01(audio.volume + 5.0 * Time.deltaTime);
	else
		audio.volume = Mathf.Clamp01(audio.volume - 1.0 * Time.deltaTime);
	
}