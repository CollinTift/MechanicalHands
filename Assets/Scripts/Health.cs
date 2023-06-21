using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float maxHealth = 100f;

    [SerializeField] float currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0f) Die();
    }

    public void Heal(float healing) {
        currentHealth = Mathf.Clamp(currentHealth + healing, 0f, maxHealth);
    }

    private void Die() {
        // Deactivate entity
        Debug.Log(gameObject.name + "Dead");
    }
}
