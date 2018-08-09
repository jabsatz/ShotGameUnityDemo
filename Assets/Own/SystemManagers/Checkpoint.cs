using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			GameMaster.lastCheckpointPos = transform.position;
		}
	}
}
