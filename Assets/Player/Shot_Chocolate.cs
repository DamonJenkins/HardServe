using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Chocolate : MonoBehaviour
{
	//The amount of each of the players stats that this bullet will take
	const float dmgScalar = 0.1f,
				spdScalar = 1.0f,
				rngScalar = 1.0f;

	//damage = HP dealt to enemy on hit
	//speed  = How many units the bullet travels per second
	//range  = How many units the bullet travels before being destroyed
	float damage, speed, range;

	//The number of shots for the player to fire per second
	static float fireRate = 50.0f;

	//TODO: Figure out if this should be multiplicative or additive
	float slowAmount = 0.2f;

	//How long the bullet has left until it is destroyed
	float existenceTimer;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		existenceTimer -= Time.deltaTime;
		if (existenceTimer <= 0.0f) Destroy(gameObject);
	}

	//Takes the player's shot damage, shot speed, and shot range, and the direction in which to move
	public void Initialise(float _plrDmg, float _plrSpd, float _plrRng, Vector2 _direction, Vector2 _plrVelocity)
	{
		damage = _plrDmg * dmgScalar;
		speed  = _plrSpd * spdScalar;
		range  = _plrRng * rngScalar;

		existenceTimer = range / speed;

		//Add significant velocity in the player's movement direction
		Vector2 finalVelocity = (_direction * speed) + (_plrVelocity * 0.3f);

		GetComponent<Rigidbody2D>().velocity = finalVelocity;
	}

	private void OnCollisionEnter2D(Collision2D _collision)
	{
        //TODO: Uncomment
        //Enemy hitEnemy = _collision.gameObject.GetComponent<Enemy>();
        //if (hitEnemy != null) {
        //	hitEnemy.Damage(damage);
        //	hitEnemy.SlowBy(slowAmount);
        //}

        Destroy(gameObject);
    }

	public static float GetFireRate(float _plrFireRate) {
		return _plrFireRate * fireRate;
	}
}
