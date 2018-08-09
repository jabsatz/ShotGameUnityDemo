using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBetween : MonoBehaviour {

	[SerializeField] private Transform Target1;
    [SerializeField] private Transform Target2;
	[Range(0, 1)][SerializeField] private float point = 0.5f;

	// Update is called once per frame
	void LateUpdate () {
		Vector3 betweenPoint = (Target1.position + Target2.position)*point;
		transform.position = new Vector3(
			Target2.position.x,
			Target2.position.y,
			transform.position.z
		);
	}
}
