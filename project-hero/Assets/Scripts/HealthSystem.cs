using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [SerializeField]
    private int startingHealth;
    private int currentHealth;
    
    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Do something
    }
}
