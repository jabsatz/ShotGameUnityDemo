using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {
	Animator m_animator;

	void Awake() {
		m_animator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.CompareTag("Player")) {
			m_animator.SetBool("Open", true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.CompareTag("Player")) {
			m_animator.SetBool("Open", false);
		}
	}

	void Lock() {
		m_animator.SetBool("Locked", true);
	}

	void Unlock() {
		m_animator.SetBool("Locked", false);
	}
}
