using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 120f;

    float horizontalMove = 0f;
    bool stand = false;
	bool jump = false;
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(CanMove()) {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            if(horizontalMove != 0) {
                horizontalMove = horizontalMove > 0 ? 1 : -1;
            }
            stand = Input.GetAxisRaw("Ground") > 0.5f || Input.GetButton("Ground");
            if(Input.GetButtonDown("Jump")) {
                jump = true;
            }
        } else {
            horizontalMove = 0f;
            jump = false;
        }
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * runSpeed * Time.fixedDeltaTime, false, jump, stand);
		jump = false;
    }

    bool CanMove() {
        return !animator.GetCurrentAnimatorStateInfo(0).IsTag("Frozen");
    }
}
