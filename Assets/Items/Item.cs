using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public delegate void ItemPickup();
	public static event ItemPickup ItemPickedUp;

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

		Player plr = _collision.gameObject.GetComponent<Player>();

		if (plr != null)
		{
			plr.AddStats(
				thisInfo.moveSpeedIncrease,
				thisInfo.maxHealthIncrease,
				thisInfo.maxAmmoIncrease,
				thisInfo.rechargeRateIncrease,
				thisInfo.fireRateIncrease,
				thisInfo.damageIncrease,
				thisInfo.shotSpeedIncrease,
				thisInfo.rangeIncrease
			);

			//AudioSource.PlayClipAtPoint(thisInfo.pickupSound, transform.position);

			FindObjectOfType<HUDManager>().PickedUpItem(thisInfo.itemName, thisInfo.itemDescription);

			Destroy(gameObject);
		}
	}
}
