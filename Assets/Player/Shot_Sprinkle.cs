using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Sprinkle : MonoBehaviour
{
	[SerializeField]
	Sprite[] sprinkleImages;

	//The amount each stat will increase when AddStats is increased
	const float dmgScalar = 0.07f, 
				spdScalar = 3.0f, 
				rngScalar = 2.1f;

	//damage = HP dealt to enemy on hit
	//speed  = How many units the bullet travels per second
	//range  = How many units the bullet travels before being destroyed
	static float damage = 0.2f, speed = 9.0f, range = 7.2f;

	//The number of shots for the player to fire per second
	static float fireRate = 1.5f;

	//How long the bullet has left until it is destroyed
	float existenceTimer;

    // Start is called before the first frame update
    void Start()
    {
		GetComponent<SpriteRenderer>().sprite = sprinkleImages[Random.Range(0, sprinkleImages.Length)];
    }

    // Update is called once per frame
    void Update()
    {
		existenceTimer -= Time.deltaTime;
		if (existenceTimer <= 0.0f) Destroy(gameObject);

        Color oC = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(oC.r, oC.g, oC.b, existenceTimer * 15.0f);

        float scaleBy = Mathf.Min(existenceTimer * 10.0f, 1.0f);

        transform.localScale = new Vector3(scaleBy, scaleBy, scaleBy);
    }

	//Takes the player's shot damage, shot speed, and shot range, and the direction in which to move
	public void Initialise(Vector2 _direction, Vector2 _plrVelocity) {

		float rangeNoise = Random.Range(0.85f, 1.0f);
		float speedNoise = Random.Range(0.85f, 1.0f);

		existenceTimer = (range * rangeNoise) / (speed * speedNoise);

		//Add significant velocity in the player's movement direction
		Vector2 finalVelocity = (_direction * (speed * speedNoise)) + (_plrVelocity * 0.4f);

		GetComponent<Rigidbody2D>().velocity = finalVelocity;
	}

	private void OnCollisionEnter2D(Collision2D _collision){
		EnemyAI hitEnemy = _collision.gameObject.GetComponent<EnemyAI>();
		if (hitEnemy != null)
		{
			hitEnemy.receiveDmg(damage);
		}
		//else {
		//	SeekAI hitSeek = _collision.gameObject.GetComponent<SeekAI>();
		//	if (hitSeek != null)
		//	{
		//		hitSeek.receiveDmg(damage);
		//	}
		//	else
		//	{

		//	}
		//}

		Destroy(gameObject);
    }

	public static float GetFireRate(float _plrFireRate) {
		return _plrFireRate * fireRate;
	}

	public static void AddStats(float _damage, float _speed, float _range){
		damage += _damage * dmgScalar;
		speed += _speed * spdScalar;
		range += _range * rngScalar;
	}
}
