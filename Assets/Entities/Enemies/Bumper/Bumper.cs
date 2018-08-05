using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour {
	[SerializeField] private int duration = 20;
	[SerializeField] private int magnitude = 10;
	private Animator animator;
	private bool floatingUp = true;
	private int initialFloatInterval = 30;
	private int floatInterval = 30;

	void Awake () {
		animator = GetComponent<Animator>();
	}

	void FixedUpdate() {
		Vector3 prevPos = gameObject.transform.position;
		float newYPos = prevPos.y + (floatingUp ? floatInterval : -floatInterval) * 0.2f * Time.deltaTime;
		gameObject.transform.position = new Vector3(prevPos.x, newYPos, prevPos.z);
		if(floatInterval == 0) {
			floatInterval = initialFloatInterval;
			floatingUp = !floatingUp;
		} else floatInterval--;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.tag == "Player") {
			Vector2 boostDirection = (col.gameObject.transform.position - transform.position).normalized;
			col.gameObject.GetComponent<CharacterController2D>().BoostTo(boostDirection, duration, magnitude);
            ArmController armController = col.gameObject.GetComponentInChildren<ArmController>();
			if(armController) armController.RefreshAltFire();
			animator.SetTrigger("Bumped");
		}
	}
}
