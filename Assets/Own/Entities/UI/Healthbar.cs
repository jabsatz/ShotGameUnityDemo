using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
	public GameObject progress;
	public int health = 100;
	public bool isPlayer = false;
	private GameMaster gameMaster;

	void Awake() {
		gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
	}

	public void SetHealth(int health) {
		if(isPlayer && health <= 0 && this.health > 0) gameMaster.EndGameIn(60);
		this.health = health;
	}

	// Update is called once per frame
	void Update () {
        progress.GetComponent<RectTransform>().localScale = new Vector2(
            Mathf.Clamp(health / 100f, 0, 1),
            1
        );
        progress.GetComponent<Image>().color = Color.HSVToRGB(health/350f, 1, .55f);
	}
}
