using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour {

	public void Destroy() {
		GameObject.Destroy(gameObject);
	}
}
