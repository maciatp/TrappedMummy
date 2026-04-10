// this script is used on the core prefab (the one that holds and controls other flame prefabs as children)

private var FlamePrefab : GameObject;

function Start ()
{

		// find the emitter object in the scene hierarchy, so that it could be used as a prefab to instantiate new flame emitter objects
	FlamePrefab = GameObject.Find("FlameThrowerFlames/FlameEmitter");
	
}

function Update ()
{

	if (Input.GetButtonDown("Fire1"))
	{
		if (FlamePrefab != null)
		{
				// instantiate a new flame emitter, so that the old one wouldn't be affected
			var clone : GameObject = Instantiate(FlamePrefab, transform.position, transform.rotation);
				// attach the new emitter to the current object, which is the core object of the whole flamethrower flame prefab
			clone.transform.parent = transform;
				// set the new object to be usable (there should be only one flame emitter, which is used as a prefab. Others should have that variable set to false)
			//clone.GetComponent("FlameScript").setIsPrefab(false);
		}
	}
	
}
