using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Cherry_Stage2 : MonoBehaviour
{

    List<GameObject> enemiesInRange = new List<GameObject>();

    float fuseTime = 1.5f;

    //How long the bomb has left until it explodes
    float existenceTimer;

    // Start is called before the first frame update
    void Start()
    {
        existenceTimer = fuseTime;
    }

    // Update is called once per frame
    void Update()
    {
        existenceTimer -= Time.deltaTime;
        if (existenceTimer <= 0.0f) Destroy(gameObject);
    }

    private void OnDestroy()
	{
		//Explode
		foreach (GameObject enemy in enemiesInRange) {
			//enemy.GetComponent<Enemy>().Damage(explosionDamage);
		}
	}

    private void OnTriggerEnter2D(Collider2D _collision){
		if (!enemiesInRange.Contains(_collision.gameObject)) {
			enemiesInRange.Add(_collision.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D _collision){
		if (enemiesInRange.Contains(_collision.gameObject)) {
			enemiesInRange.Remove(_collision.gameObject);
		}
	}
}
