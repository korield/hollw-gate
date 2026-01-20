using UnityEngine;

public class Enemy : MonoBehaviour
{
 
 public int maxHealth = 12;
 int currentHealth;

 void Start()
    {
        currentHealth = maxHealth;
    }   

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Animation für Schaden muss auch peter machen 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Animation fürs sterben jajaja KIllian

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}