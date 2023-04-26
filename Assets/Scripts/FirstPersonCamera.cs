using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {
    public Transform player;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    private float rotationX;

    public float turnSpeed = 3.0f;
    public float moveSpeed = 2.0f;

    bool lockCursor = true;

    void Start() {
        // lock and hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        Aim();
        Move();
    }

    void Aim() {
        // mouse input
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotationX += Input.GetAxis("Mouse Y") * turnSpeed;

        // ensure we cannot rotate too far up or down
        rotationX = Mathf.Clamp(rotationX, minTurnAngle, maxTurnAngle);

        // rotate camera around local X axis
        transform.localEulerAngles = Vector3.right * -rotationX;

        // rotate player (and camera) around Y axis
        player.Rotate(Vector3.up * y);
    }

    void Move() {
        Vector3 dir = new Vector3(0, 0, 0);

        // unity uses y as height axis, x,z for ground movement
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        // moves rotation dependent
        player.transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
