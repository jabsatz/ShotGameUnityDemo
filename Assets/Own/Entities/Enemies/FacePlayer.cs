using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update() {
        transform.localScale = new Vector2(
		    player.position.x < transform.position.x ? -1 : 1,
            transform.localScale.y
        );
    }
}
