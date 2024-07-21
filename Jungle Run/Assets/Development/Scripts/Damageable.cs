using UnityEngine;
using UnityAtoms.BaseAtoms;

public class Damageable : MonoBehaviour
{
    [SerializeField] private IntVariable health;
    [SerializeField] private IntConstant maxHealth;
    [SerializeField] private VoidEvent onDamage;
    [SerializeField] private VoidEvent onDeath;

    private void Awake()
    {
        health.Value = maxHealth.Value;  // Initialize health to max at Awake (before Start)
    }

    public void TakeDamage(int damage)
    {
        health.Value -= damage;

        if (health.Value <= 0)
        {
            health.Value = 0;
            onDeath.Raise();
        }
        else
        {
            onDamage.Raise();
        }
    }
}