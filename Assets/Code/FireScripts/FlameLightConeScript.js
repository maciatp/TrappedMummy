// this script is used to make the light dynamic. The light is used to create the illusion that the flame emits light

function Update ()
{

	// make the light brighter gradually
		light.intensity = Mathf.Clamp01(light.intensity + 10.0 * Time.deltaTime);
		var tmpTime : int = Time.time * 4.0;
			// change the light intensity over time a bit. This creates a feeling of a moving flame
		if (Time.time - tmpTime / 4.0 < 0.125)
			light.intensity += (Time.time - tmpTime / 4.0) * 10.0;
		else
			light.intensity += (0.5 - (Time.time - tmpTime / 4.0)) * 10.0;
	
}