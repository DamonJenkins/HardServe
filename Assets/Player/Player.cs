using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
	//BulletPrefabs
	[SerializeField]
	Shot_Sprinkle sprinkleShot;
	[SerializeField]
	Shot_Chocolate chocolateShot;
	[SerializeField]
	Shot_Cherry cherryShot;

	int currentWeapon = 0;
    float maxSpeed = 7.0f;

	float[] shotTimers = { 0.0f, 0.0f, 0.0f };


    float fireRateScalar = 1.0f;

	//Player Stats
	//damage = HP dealt to enemy on hit
	//speed = How many units the bullet travels per second
	//range = How many units the bullet travels before being destroyed
	float shotDamage = 2.0f, 
		  shotSpeed = 9.0f, 
		  shotRange = 9.0f;

	void Start(){
 
	}

    // Update is called once per frame
    void Update(){
		for (int i = 0; i < 3; i++) {
			if (shotTimers[i] < 0.0f) continue;
			shotTimers[i] -= Time.deltaTime;
		}

        Vector2 tempVelocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        GetComponent<Rigidbody2D>().velocity = tempVelocity.SqrMagnitude() > 1.0f ? tempVelocity.normalized * maxSpeed : tempVelocity * maxSpeed;

        Vector2 plrVel = GetComponent<Rigidbody2D>().velocity;

        Animator myAnimator = GetComponent<Animator>();

        myAnimator.SetBool("GoingLeft", plrVel.x < 0.0f);
        myAnimator.SetBool("GoingRight", plrVel.x > 0.0f);
        myAnimator.SetBool("GoingUp", plrVel.y > 0.0f);
        myAnimator.SetBool("GoingDown", plrVel.y < 0.0f);

        if (CanShoot()) {

            if (Input.GetButton("FireU")){
				switch (currentWeapon) {
					case (0): { ShootSprinkle(new Vector2(0.0f, 1.0f)); break; }
					case (1): { ShootChocolate(new Vector2(0.0f, 1.0f)); break; }
					case (2): { ShootCherry(new Vector2(0.0f, 1.0f)); break; }
				}
            }
            else if(Input.GetButton("FireD")){
				switch (currentWeapon) {
					case (0): { ShootSprinkle(new Vector2(0.0f, -1.0f)); break; }
					case (1): { ShootChocolate(new Vector2(0.0f, -1.0f)); break; }
					case (2): { ShootCherry(new Vector2(0.0f, -1.0f)); break; }
				}
            }
            else if(Input.GetButton("FireL")){
				switch (currentWeapon) {
					case (0): { ShootSprinkle(new Vector2(-1.0f, 0.0f)); break; }
					case (1): { ShootChocolate(new Vector2(-1.0f, 0.0f)); break; }
					case (2): { ShootCherry(new Vector2(-1.0f, 0.0f)); break; }
				}
            }
            else if(Input.GetButton("FireR")){
				switch (currentWeapon) {
					case (0): { ShootSprinkle(new Vector2(1.0f, 0.0f)); break; }
					case (1): { ShootChocolate(new Vector2(1.0f, 0.0f)); break; }
					case (2): { ShootCherry(new Vector2(1.0f, 0.0f)); break; }
				}
            }
        }

        if (Input.GetButtonDown("SwitchLeft")){
            if (currentWeapon == 0) currentWeapon = 2;
            else currentWeapon -= 1;
        }
        else if (Input.GetButtonDown("SwitchRight")){
            if (currentWeapon == 2) currentWeapon = 0;
            else currentWeapon += 1;
        }
    }

    private bool CanShoot() {
        return shotTimers[currentWeapon] <= 0.0f;
    }

	public void ShootSprinkle(Vector2 _direction){
		Vector2 plrVelocity = GetComponent<Rigidbody2D>().velocity;
		
		for (int i = 0; i < 15; i++){
			Vector3 noisyDirection = Quaternion.Euler(0.0f, 0.0f, Random.Range(-22.5f, 22.5f)) * new Vector3(_direction.x, _direction.y, 0.0f);

			GameObject.Instantiate(sprinkleShot, transform.position, Quaternion.identity).Initialise(
				shotDamage,
				shotSpeed * Random.Range(0.85f, 1.0f),
				shotRange * Random.Range(0.85f, 1.0f),
				new Vector2(noisyDirection.x, noisyDirection.y),
				plrVelocity
			);
		}

		shotTimers[currentWeapon] = 1.0f / (Shot_Sprinkle.GetFireRate(fireRateScalar));
	}

	public void ShootChocolate(Vector2 _direction){
		Vector2 plrVelocity = GetComponent<Rigidbody2D>().velocity;

		GameObject.Instantiate(chocolateShot, transform.position, Quaternion.identity).Initialise(
			shotDamage,
			shotSpeed,
			shotRange,
			_direction,
			plrVelocity
		);

		shotTimers[currentWeapon] = 1.0f / (Shot_Chocolate.GetFireRate(fireRateScalar));
	}

	public void ShootCherry(Vector2 _direction){
		Vector2 plrVelocity = GetComponent<Rigidbody2D>().velocity;

		GameObject.Instantiate(cherryShot, transform.position, Quaternion.identity).Initialise(
			shotDamage,
			shotSpeed,
			shotRange,
			_direction,
			plrVelocity
		);

		shotTimers[currentWeapon] = 1.0f / (Shot_Cherry.GetFireRate(fireRateScalar));
	}
}
