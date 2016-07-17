using UnityEngine;
using System.Collections;

public class PlayerHealth : MovingObject
{
    public float playerHealth;

    private void Die()
    {
        Application.LoadLevel(0);
    }

    public void TakeDamage(float damage)
    {
        gameObject.GetComponent<PlayerMovement>().KnockBack();
        playerHealth -= damage;
        if (playerHealth <= 0f)
        {
            Die();
        }
    }
}
