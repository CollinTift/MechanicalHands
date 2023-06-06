using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Health health;

    private void Start() {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision other) {
        // if a player projectile hits this enemy
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerProj")) {
            // this enemy takes damage equal to projectile's damage
            Debug.Log(gameObject.name + " has taken Damage: " + other.gameObject.GetComponent<Projectile>().damage);
            health.TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }
    }
}
