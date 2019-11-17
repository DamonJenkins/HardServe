using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Chocolate : MonoBehaviour
{

	//The amount of each of the players stats that this bullet will take
	const float dmgScalar = 0.03f,
				spdScalar = 3.0f,
				rngScalar = 3.0f;

	//damage = HP dealt to enemy on hit
	//speed  = How many units the bullet travels per second
	//range  = How many units the bullet travels before being destroyed
	static float damage = 0.1f, speed = 9.0f, range = 9.0f;

	//The number of shots for the player to fire per second
	static float fireRate = 50.0f;

	//TODO: Figure out if this should be multiplicative or additive
	float slowAmount = 1.5f;

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
	public void Initialise(Vector2 _direction, Vector2 _plrVelocity)
	{
		existenceTimer = range / speed;

		//Add significant velocity in the player's movement direction
		Vector2 finalVelocity = (_direction * speed) + (_plrVelocity * 0.3f);

		GetComponent<Rigidbody2D>().velocity = finalVelocity;
	}

	private void OnCollisionEnter2D(Collision2D _collision)
	{
		EnemyAI hitEnemy = _collision.gameObject.GetComponent<EnemyAI>();
		if (hitEnemy != null)
		{
			hitEnemy.receiveDmg(damage);
			hitEnemy.SlowDown(slowAmount);
		}

		Destroy(gameObject);
    }

	public static float GetFireRate(float _plrFireRate) {
		return _plrFireRate * fireRate;
	}

	public static void AddStats(float _damage, float _speed, float _range) {
		damage += _damage * dmgScalar;
		speed += _speed * spdScalar;
		range += _range * rngScalar;
	}
}
