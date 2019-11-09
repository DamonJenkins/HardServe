using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	static ItemData[] allItems;

	ItemData thisInfo;

    // Start is called before the first frame update
    void Start()
    {
		if (allItems == null) {
			allItems = Resources.LoadAll<ItemData>("ItemCollection");
		}

		thisInfo = allItems[Random.Range(0, allItems.Length)];
		GetComponent<SpriteRenderer>().sprite = thisInfo.itemSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D _collision){
		//TODO: Make pop-up saying which item they picked up

		_collision.gameObject.GetComponent<Player>().AddStats(
			thisInfo.moveSpeedIncrease,
			thisInfo.maxAmmoIncrease,
			thisInfo.rechargeRateIncrease,
			thisInfo.fireRateIncrease,
			thisInfo.damageIncrease,
			thisInfo.shotSpeedIncrease,
			thisInfo.rangeIncrease
		);

		Destroy(gameObject);
	}
}
