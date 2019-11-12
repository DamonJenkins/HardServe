using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Item Data Object")]
public class ItemData : ScriptableObject{

	[Header("Display Information")]
	[SerializeField]
	public Sprite itemSprite;
	[SerializeField]
	public string itemName;
	[SerializeField]
	public string itemDescription;
	[SerializeField]
	public AudioClip pickupSound;

	[Header("Player Stats")]
	[SerializeField]
	public float moveSpeedIncrease;
	[SerializeField]
	public int maxHealthIncrease;

	[Header("Weapon Stats")]
	[SerializeField]
	public float maxAmmoIncrease;
	[SerializeField]
	public float rechargeRateIncrease;
	[SerializeField]
	public float fireRateIncrease;
	[SerializeField]
	public float damageIncrease;
	[SerializeField]
	public float shotSpeedIncrease;
	[SerializeField]
	public float rangeIncrease;

}
