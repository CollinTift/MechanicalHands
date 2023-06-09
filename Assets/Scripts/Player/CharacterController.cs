using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    Rigidbody playerRB;

    private Health health;

    [Header("Movement")]
    public float moveSpeed = 2.0f;

    Vector3 movement;
    float horizontalMovement;
    float verticalMovement;

    [Header("Jumping")]
    bool jump;
    bool jumpCancelled;
    float jumpTime;
    public float maxJumpTime = 0.3f;
    public float jumpForce = 3.0f;
    public float fallRate = 100f;

    bool grounded;
    public Transform groundChecker;
    public float groundCheckRadius = .3f;
    public LayerMask groundLayer;

    public GameObject playerProj;
    public Transform projectileSpawner;
    public Transform projectileParent;

    void Start() {
        playerRB = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    void Update() {
        KeyInput();
        CheckGrounded();
        Jump();
        Shoot();
    }

    void FixedUpdate() {
        Move();
        Fall();
    }

    void KeyInput() {
        // keyboard input - unity uses y as height axis, x,z for ground movement
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        // by multiplying by player.forward and player.right, we get rotation dependent movement
        movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void CheckGrounded () {
        // detect if grounded
        grounded = Physics.CheckSphere(groundChecker.position, groundCheckRadius, groundLayer);
    }

    void Jump() {
        // detects jump input and sets base important values for current dynamic jump
        if (Input.GetButtonDown("Jump") && grounded) {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jump = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        
        // counting total time jump is held on current jump input, and measuring whether it ends early or at max time
        if (jump) {
            jumpTime += Time.deltaTime;
            if (Input.GetButtonUp("Jump")) jumpCancelled = true;
            if (jumpTime > maxJumpTime) jump = false;
        }
    }

    void Move() {
        // move position to input direction, using interpolation if enabled
        playerRB.MovePosition(transform.position + movement * Time.deltaTime * moveSpeed);
    }

    void Fall() {
        // if jump dropped early, apply fall force
        if (jumpCancelled && jump && playerRB.velocity.y > 0) {
            playerRB.AddForce(Vector3.down * fallRate);
        }
    }

    void Shoot() {
        if (Input.GetButtonDown("Fire1")) {
            GameObject proj = Instantiate(playerProj, projectileSpawner.position, projectileSpawner.rotation, projectileParent);
            proj.GetComponent<Rigidbody>().AddForce(projectileSpawner.TransformDirection(0f, 0f, proj.GetComponent<Projectile>().projectileSpeed), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other) {
        // if an enemy projectile hits player
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyProj")) {
            // player takes damage equal to projectile's damage
            Debug.Log(gameObject.name + " has taken Damage: " + other.gameObject.GetComponent<Projectile>().damage);
            health.TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }
    }
}
