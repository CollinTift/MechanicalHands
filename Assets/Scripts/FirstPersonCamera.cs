using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    public Transform player;
    public Rigidbody playerRB;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotationY;
    private float rotationX;

    public float turnSpeedVertical = 3.0f;
    public float turnSpeedHorizontal = 4.0f;
    public float moveSpeed = 2.0f;

    private Vector3 movement;
    private float horizontalMovement;
    private float verticalMovement;

    public bool lockCursor = true;

    void Start() {
        // lock and hide cursor
        if (lockCursor) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update() {
        PlayerInput();
    }

    void FixedUpdate() {
        Move();
        Aim();
    }

    void PlayerInput() {
        // mouse input
        rotationY = Input.GetAxis("Mouse X") * turnSpeedHorizontal;
        rotationX += Input.GetAxis("Mouse Y") * turnSpeedVertical;
        
        // keyboard input - unity uses y as height axis, x,z for ground movement
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // by multiplying by player.forward and player.right, we get rotation dependent movement
        movement = player.forward * verticalMovement + player.right * horizontalMovement;
    }

    void Aim() {
        // ensure we cannot rotate too far up or down
        rotationX = Mathf.Clamp(rotationX, minTurnAngle, maxTurnAngle);

        // rotate camera around local X axis
        transform.localEulerAngles = Vector3.right * -rotationX;

        // rotate player (and camera) around Y axis
        playerRB.MoveRotation(playerRB.rotation * Quaternion.Euler(Vector3.up * rotationY));
    }

    void Move() {
        // !!!!!!! SWITCH TO ADD FORCE MAYBE? IDK GRAVITY NOT WORKING
        playerRB.MovePosition(player.position + movement * Time.deltaTime * moveSpeed);
    }

    // void Jump() {
    //     //grounded detection
        

    //     //jump input
    //     if (Input.GetButton("Jump")) {
    //         playerRB.
    //     }
    // }
}
