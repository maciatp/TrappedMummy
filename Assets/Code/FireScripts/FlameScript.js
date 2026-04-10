// this script is used on the main flame emitter. It has various dynamic effects that are applied to the particles

	// the size grow of the particles that are still near to the tube
public var sizeGrowAtFirst : float = 3.5;
	// the strength of the scatter velocity of the smoke
public var scatterVelocity : float = 50.0;
	// the strength of the rising velocity of the smoke
public var flyUpVelocity : float = 8.0;

private var isPrefab : boolean = true;
private var isActive : boolean = true;

function Start ()
{
		// the prefab is active at first. It's deactivated with the first release of the Fire1 button
	isActive = true;
}

function Update ()
{

	if (Input.GetButton("Fire1"))
	{
		if ((!particleEmitter.emit) && (isActive) && (!isPrefab))
			particleEmitter.emit = true;
	}
	else
	{
		isActive = false;
		particleEmitter.emit = false;
	}
	
		// the emitter isn't destroyed while it's active or if it is the object that is used as a prefab to instantiate other identical emitters
	if ((!isActive) && (!isPrefab))
	{
			// if all the particles have disappeared, the emitter should be destroyed
		if (particleEmitter.particles.Length == 0)
		{
			Destroy(gameObject);
		}
	}

}

function LateUpdate ()
{
		// extract the particles
	var particles = particleEmitter.particles;

	for (var i = 0; i < particles.Length; i++)
	{
			// the particles fly up, when they transform to smoke. flyUpEnergy is the time before the particle disappears
		var flyUpEnergy = 0.9;
		if (particles[i].energy <= flyUpEnergy)
		{
			particles[i].position.y += flyUpVelocity * Mathf.Sqrt((flyUpEnergy - particles[i].energy) / flyUpEnergy) * Time.deltaTime;
		}
		
			// the particles scatter when they are more burned out. scatterEnergy is the time before the particle disappears
		var scatterEnergy = 1.5;
		if ((particles[i].energy <= scatterEnergy) && (particles[i].velocity.magnitude <= 10.0))
		{
				// add a random amount of velocity to each direction to make the particles scatter
			particles[i].velocity.x += Time.deltaTime * Random.Range((scatterEnergy - particles[i].energy) * -scatterVelocity, (scatterEnergy - particles[i].energy) * scatterVelocity);
			particles[i].velocity.y += Time.deltaTime * Random.Range((scatterEnergy - particles[i].energy) * -scatterVelocity, (scatterEnergy - particles[i].energy) * scatterVelocity);
			particles[i].velocity.z += Time.deltaTime * Random.Range((scatterEnergy - particles[i].energy) * -scatterVelocity, (scatterEnergy - particles[i].energy) * scatterVelocity);
		}
	}
			
			
	if (isActive)
	{
		if (particleEmitter.emit)
		{
			for (i = 0; i < particles.Length; i++)
			{
					// when particles are still close to the tube (their root), they should move more like in local coordinates rather than world. This is because the flame is thrown with a strong force, which only fades out a bit later
					// followEnergy is the time from the creation of the particle
				var followEnergy = 0.4;
				if (particles[i].energy >= particleEmitter.maxEnergy - followEnergy)
				{
						// the koefficient that is used to determine the locality of the particle (the smaller it is, the more the particle moves as if it was local, not in world space)
					var koefficient = Mathf.Clamp((particleEmitter.maxEnergy - particles[i].energy) / followEnergy, 0.0, 1.0);
					koefficient *= koefficient;
					
						// randomize the position of the particle a bit
					var randomDistance = Random.Range(1.0 - (particleEmitter.maxEnergy - particles[i].energy) / 4.0, 1.0 + (particleEmitter.maxEnergy - particles[i].energy) / 4.0);
						// calculate the position that would be used if the particle was local
					var newPosition : Vector3 = (particleEmitter.transform.position + transform.forward * 2.5 * randomDistance * ((particleEmitter.maxEnergy - particles[i].energy) / followEnergy));
					
						// set the velocity of the particle to be forward, but a bit randomized (it takes effect when the particle leaves the local area)
					particles[i].velocity = transform.forward * 6.0;
					particles[i].velocity.x += Random.Range(-((particleEmitter.maxEnergy - particles[i].energy) / followEnergy), ((particleEmitter.maxEnergy - particles[i].energy) / followEnergy));
					particles[i].velocity.y += Random.Range(-((particleEmitter.maxEnergy - particles[i].energy) / followEnergy), ((particleEmitter.maxEnergy - particles[i].energy) / followEnergy));
					particles[i].velocity.z += Random.Range(-((particleEmitter.maxEnergy - particles[i].energy) / followEnergy), ((particleEmitter.maxEnergy - particles[i].energy) / followEnergy));
					
						// if the particle is small, it's positioned in front of the tube
					if (particles[i].size < 0.2)
					{
							// the koefficient is used to assign the weights of each vector. When time passes, the world space position takes more effect
						particles[i].position = particles[i].position * koefficient + newPosition * (1 - koefficient);
					}
						// if the particle is larger, it should fall down a bit
					else
					{
							// the koefficient is used to assign the weights of each vector. When time passes, the world space position takes more effect
						particles[i].position = particles[i].position * koefficient + (newPosition + Vector3(0, -particles[i].size / 4.0, 0)) * (1 - koefficient);
					}
						// if the particle falls down too much, it should stick to the zero height
					particles[i].position.y = Mathf.Clamp(particles[i].position.y, 0, 10000);
				}
			}
		
				// the particles should fall down a bit, because the liquid of the flame has got weight and is affected by gravity
				// the effect is applied to about a half of the particles (though, some particles might be chosen several times)
			for (var j = 0; j < particles.Length / 2.0; j++)
			{
					// take a random particle
				var whichOne = Mathf.Clamp(Random.Range(1, particles.Length - 1), 0, particles.Length - 1);
				
					// fallDownEnergyMax is the time from the creation of the particle, when the particles begin to fall down and to grow
				var fallDownEnergyMax = 0.04;
					// fallDownEnergyMin is the time from the creation of the particle, when the particles finish to fall down and to grow
				var fallDownEnergyMin = 0.25;
				if ((particles[whichOne].energy >= particleEmitter.maxEnergy - fallDownEnergyMin) && (particles[whichOne].energy <= particleEmitter.maxEnergy - fallDownEnergyMax))
				{
						// the size is increased and the particle is moved down a bit
					particles[whichOne].size += sizeGrowAtFirst * Time.deltaTime;
					particles[whichOne].position.y -= 1.0 * Time.deltaTime;
				}
			}
		}
	}
	
		// apply the changes to the particles
	particleEmitter.particles = particles;
	
}


function setIsPrefab (value : boolean)
{

		// set the object to be not used as a prefab (or vice versa)
	isPrefab = value;
	
}