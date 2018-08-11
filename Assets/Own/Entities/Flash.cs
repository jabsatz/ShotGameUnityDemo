using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {

	public int flashCycles = 5;

	public void DamageFlash() {
		StartCoroutine(FlashFor(flashCycles));
	}

	IEnumerator FlashFor(int cycles) {
		gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1f);
		for(int i = 0; i < cycles; i++) {
			yield return null;
		}
        gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0f);
	}
}
