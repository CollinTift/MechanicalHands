using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 1f;

    // time in seconds before projectile despawns
    public float projectileTime = 3f;

    // force applied to projectile at spawn
    public float projectileSpeed = 10f;

    // list of projectile types, maybe use static enumerator to spawn based on type?
    public static Projectile playerProj;
    public static Projectile enemyProj;

    private void Update() {
        
    }
}
