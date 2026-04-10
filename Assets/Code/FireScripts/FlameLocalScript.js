// this script is used on the secondary flame emitter, which is used to have the beginning of the flame more fluent (without any spaces between the particles)

function Update ()
{

			if (!particleEmitter.emit)
		{
			particleEmitter.emit = true;
				// make the emitter local to stick to the core prefab
			particleEmitter.useWorldSpace = false;
		}
	
}