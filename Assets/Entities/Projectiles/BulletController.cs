using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private float speed = 0.4f;

    void Update() {
        float direction = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        transform.position = transform.position + new Vector3(
			Mathf.Cos(direction) * speed,
			Mathf.Sin(direction) * speed,
			0
		);
    }
}
