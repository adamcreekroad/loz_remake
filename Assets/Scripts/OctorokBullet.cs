using UnityEngine;
using System.Collections;

public class OctorokBullet : MonoBehaviour
{
    public float attackDamage;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
                // how much the character should be knocked back
                var magnitude = 750;
                // calculate force vector
                var force = transform.position - col.transform.position;
                force.Normalize();
                // normalize force vector to get direction only and trim magnitude
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            }
            Destroy(gameObject);
        }
    }
}
