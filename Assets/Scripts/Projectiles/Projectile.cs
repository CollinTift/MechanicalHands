using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 1f;

    // projectile timing before despawn
    public float projectileTime = 3f; // time in seconds before projectile despawns
    private float projectileLifespan; 

    // force applied to projectile at spawn
    public float projectileSpeed = 10f;

    private void Start() {
        projectileLifespan = 0f;
    }

    private void Update() {
        DoProjectileLifespan();
    }

    private void DoProjectileLifespan() {
        projectileLifespan += Time.deltaTime;
        if (projectileLifespan >= projectileTime) {
            Destroy(gameObject);
        }
    }
}
