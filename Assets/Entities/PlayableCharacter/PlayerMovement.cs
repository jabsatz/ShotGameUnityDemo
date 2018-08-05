using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public float runSpeed = 120f;

    float horizontalMove = 0f;
	bool jump = false;

    void Start() {
        transform.position = GameMaster.lastCheckpointPos;
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump")) {
			jump = true;
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, false, jump);
		jump = false;
    }
}
