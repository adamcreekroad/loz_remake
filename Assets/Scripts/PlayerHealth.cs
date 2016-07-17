using UnityEngine;
using System.Collections;

public class PlayerHealth : MovingObject
{
    public float playerHealth;
    public float invulnerableDuration;

    private bool invulnerable;

    private void Die()
    {
        Application.LoadLevel(0);
    }

    public void TakeDamage(float damage)
    {
        if (!invulnerable)
        {
            gameObject.GetComponent<PlayerMovement>().KnockBack();
            StartCoroutine(Invulnerability());
            playerHealth -= damage;
            if (playerHealth <= 0f)
            {
                Die();
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        float invulnerableTimer = 0f;
        // gameObject.GetComponent<Animation>().Play("Invulnerable");
        while (invulnerableTimer < invulnerableDuration)
        {
            invulnerableTimer += Time.deltaTime;
            yield return null;
        }
        invulnerable = false;
    }
}
