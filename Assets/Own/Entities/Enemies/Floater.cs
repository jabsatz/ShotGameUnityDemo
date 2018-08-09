using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour {
    [SerializeField] private bool floatingUp = true;
    [SerializeField] private int initialFloatInterval = 30;
    private int floatInterval;

	void Start() {
		floatInterval = initialFloatInterval;
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
}