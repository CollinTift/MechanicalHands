using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    Rigidbody playerRB;

    public float moveSpeed = 2.0f;

    Vector3 movement;
    float horizontalMovement;
    float verticalMovement;

    void Start() {
        playerRB = GetComponent<Rigidbody>();
    }

    void Update() {
        KeyInput();
    }

    void FixedUpdate() {
        Move();
    }

    void KeyInput() {
        // keyboard input - unity uses y as height axis, x,z for ground movement
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // by multiplying by player.forward and player.right, we get rotation dependent movement
        movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void Move() {
        // move position to input direction, using interpolation if enabled
        playerRB.MovePosition(transform.position + movement * Time.deltaTime * moveSpeed);
    }
}
