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

    public bool lockCursor = true;

    void Start() {
        // lock and hide cursor
        if (lockCursor) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update() {
        MouseInput();
    }

    void FixedUpdate() {
        Aim();
    }

    void MouseInput() {
        // mouse input
        rotationY = Input.GetAxis("Mouse X") * turnSpeedHorizontal;
        rotationX += Input.GetAxis("Mouse Y") * turnSpeedVertical;
    }

    void Aim() {
        // ensure we cannot rotate too far up or down
        rotationX = Mathf.Clamp(rotationX, minTurnAngle, maxTurnAngle);

        // rotate camera around local X axis
        transform.localEulerAngles = Vector3.right * -rotationX;

        // rotate player (and camera) around Y axis
        playerRB.MoveRotation(playerRB.rotation * Quaternion.Euler(Vector3.up * rotationY));
    }
}
