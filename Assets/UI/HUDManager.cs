using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
	Player player;
	RectTransform ammoRect;
	Animator itemPopupAnimator;

	[SerializeField]
	Texture playerHealthIcon;

	[SerializeField]
	Sprite sprinklesAmmoBG;
	[SerializeField]
	Sprite chocolateAmmoBG;
	[SerializeField]
	Sprite cherryAmmoBG;

	float healthW = 50.0f, healthH = 50.0f;
	float healthPosX = 10.0f, healthPosY = 10.0f;
	float healthSpacingX = 30.0f, healthSpacingY = 35.0f;
	int iconsPerLine = 6;

	void ReblitAmmo() {
		Image img = GameObject.Find("AmmoLevel").GetComponent<Image>();

		switch (Player.currentWeapon){
			case (0): { img.sprite = sprinklesAmmoBG; break; }
			case (1): { img.sprite = chocolateAmmoBG; break; }
			case (2): { img.sprite = cherryAmmoBG; break; }
		}
	}

    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<Player>();
		Player.WeaponChanged += ReblitAmmo;
		ReblitAmmo();

		ammoRect = GameObject.Find("AmmoMask").GetComponent<RectTransform>();

		itemPopupAnimator = GameObject.Find("ItemPopup").GetComponent<Animator>();
	}

	void Update()
	{
		float size = 400.0f * player.AmmoScale;
		Vector3 recPos = ammoRect.position;

		ammoRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

	}

	void OnGUI()
    {
		for (int i = 0; i < player.Health; i++) {
			float xPos = healthPosX + ((float)(i % iconsPerLine) * healthSpacingX);
			float yPos = healthPosY + ((float)(i / iconsPerLine) * healthSpacingY);

			float screenScale = Screen.width / 960.0f;

			GUI.DrawTexture(new Rect(xPos * screenScale, yPos * screenScale, healthW * screenScale, healthH * screenScale), playerHealthIcon);
		}
    }

	public void PickedUpItem(string _name, string _description) {
		GameObject.Find("ItemName").GetComponent<Text>().text = _name;
		GameObject.Find("ItemDesc").GetComponent<Text>().text = _description;

		itemPopupAnimator.Play("ItemPopupAnim", -1, 0.0f);
	}
}
