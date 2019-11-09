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


	[Header("Player Stats")]
	[SerializeField]
	public float moveSpeedIncrease;
	[SerializeField]
	public float maxAmmoIncrease;

	[Header("Weapon Stats")]
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
