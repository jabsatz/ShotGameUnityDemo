using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

	public void Destroy() {
		gameObject.AddComponent<DestroyOnAnimationEnd>();
		gameObject.GetComponent<Animator>().SetTrigger("Death");
	}
}
