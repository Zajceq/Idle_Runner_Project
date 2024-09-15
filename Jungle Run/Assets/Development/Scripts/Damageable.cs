using UnityEngine;
using UnityAtoms.BaseAtoms;

public class Damageable : MonoBehaviour
{
    [SerializeField] private IntVariable health; // Current health of the object
    [SerializeField] private IntConstant maxHealth; // Maximum health of the object
    [SerializeField] private VoidEvent onDamage; // Event triggered when damage is taken
    [SerializeField] private VoidEvent onDeath; // Event triggered when health reaches 0

    private void Awake()
    {
        // Set health to maxHealth when the object is initialized
        health.Value = maxHealth.Value;
    }

    public void TakeDamage(int damage)
    {
        // Subtract the damage from the current health
        health.Value -= damage;

        if (health.Value <= 0)
        {
            health.Value = 0; // Ensure health doesn't go below 0
            onDeath.Raise(); // Trigger death event
        }
        else
        {
            onDamage.Raise(); // Trigger damage event
        }
    }
}