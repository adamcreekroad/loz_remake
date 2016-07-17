using UnityEngine;
using System.Collections;

public class OctorokAttackRange : MonoBehaviour
{
    public Octorok octorok;

    public string direction;

	void Awake()
    {
        octorok = gameObject.GetComponentInParent<Octorok>();
	}
	
	void OnTriggerStay2D(Collider2D col)
    {
        if (octorok.currentDirection == direction && col.CompareTag("Player"))
        {
            octorok.Attack(direction);
        }
    }
}
