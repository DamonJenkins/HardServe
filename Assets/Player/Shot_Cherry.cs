using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Cherry : MonoBehaviour
{
    //The stage that will actually do tehe exploding
    [SerializeField]
    Shot_Cherry_Stage2 explosionObject;

	//The amount of each of the players stats that this bullet will take
	const float onHitScalar = 0.4f,
				explosionScalar = 2.0f,
				spdScalar = 1.0f,
				rngScalar = 0.5f;

	//onHitDamage = HP dealt to enemy on hit
	//explosionDamage = HP dealt to enemy during explosion
	//speed  = How many units the bullet travels per second
	//range  = How many units the bullet travels before being destroyed
	float onHitDamage, explosionDamage, speed, range;

	//The number of shots for the player to fire per second
	static float fireRate = 0.33f;

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
        if (existenceTimer <= 0.0f)
        {
            GameObject.Instantiate(explosionObject, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

	//Takes the player's shot damage, shot speed, and shot range, and the direction in which to move
	public void Initialise(float _plrDmg, float _plrSpd, float _plrRng, Vector2 _direction, Vector2 _plrVelocity) {

		onHitDamage = _plrDmg * onHitScalar;
		explosionDamage = _plrDmg * explosionScalar;
		speed  = _plrSpd * spdScalar;
		range  = _plrRng * rngScalar;

		existenceTimer = range / speed;

		//Add significant velocity in the player's movement direction
		Vector2 finalVelocity = (_direction * speed) + (_plrVelocity * 0.6f);

		GetComponent<Rigidbody2D>().velocity = finalVelocity;
	}

	private void OnCollisionEnter2D(Collision2D _collision){
        //TODO: Uncomment
        EnemyAI hitEnemy = _collision.gameObject.GetComponent<EnemyAI>();
        if (hitEnemy != null) {
        	//hitEnemy.Damage(damage);
            GameObject.Instantiate(explosionObject, transform.position, Quaternion.identity, hitEnemy.transform);
        }else{
            GameObject.Instantiate(explosionObject, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

	public static float GetFireRate(float _plrFireRate) {
		return _plrFireRate * fireRate;
	}
}
