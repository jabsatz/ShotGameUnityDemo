using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour {
	[SerializeField] float xDistance = 10;
	[SerializeField] float yDistance = 10;
	[SerializeField] int alertDuration = 30;
	[SerializeField] GameObject bullet;
	[SerializeField] int fireRate = 5;
	int alertTime = 0;
	int fireTime = 0;
	GameObject player;
	Animator animator;

	void Start() {
		player = GameObject.FindWithTag("Player");
		animator = GetComponent<Animator>();
	}

	void Update () {
		if(PlayerIsClose()) {
			animator.SetBool("EnemyOnSight", true);
			alertTime++;
		} else {
			animator.SetBool("EnemyOnSight", false);
			alertTime = 0;
		}

		if(alertTime == alertDuration) {
			animator.SetTrigger("Attack");
		}

		if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
			if(fireTime == fireRate) {
				bool isTurned = transform.localScale.x == -1;
				GameObject.Instantiate(
					bullet,
					transform.position,
					isTurned ? Quaternion.Euler(0,0,180) : transform.rotation
				);
				fireTime = 0;
			} else {
                fireTime++;
            }
		}
	}

	bool PlayerIsClose() {
		Vector2 distance = player.transform.position - transform.position;
		return Mathf.Abs(distance.x) < xDistance && Mathf.Abs(distance.y) < yDistance;
	}
}
