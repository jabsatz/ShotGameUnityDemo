using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {
	[SerializeField] private int duration = 20;
	[SerializeField] private int magnitude = 10;
	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator>();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			Vector2 boostDirection = (col.gameObject.transform.position - transform.position).normalized;
			col.gameObject.SendMessage("BoostTo", new object[3] { boostDirection, duration, (float) magnitude });
			col.gameObject.BroadcastMessage("RefreshAltFire");
			animator.SetTrigger("Bumped");
		}
	}
}
