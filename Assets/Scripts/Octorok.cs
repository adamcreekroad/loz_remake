using UnityEngine;
using System.Collections;

public class Octorok : MovingObject
{
    public float moveInterval;
    public float moveSpeed;
    public float moveDuration;
    public float attackDamage;

    private float moveTimer;
    private float isMovingTimer;

    private bool isMoving;

    private Animator anim;
    private Rigidbody2D rb2D;

    // Use this for initialization
    protected override void Start ()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMoving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > moveInterval)
            {
                StartCoroutine(Move(DetermineMoveDirection()));
                isMoving = true;
            }
        }
	
	}

    public Vector2 DetermineMoveDirection()
    {
        int direction = (int)Random.Range(0, 3);
        switch (direction)
        {
            case 0:
                anim.SetTrigger("lookUp");
                return new Vector2(0, moveSpeed);
            case 1:
                anim.SetTrigger("lookRight");
                return new Vector2(moveSpeed, 0);
            case 2:
                anim.SetTrigger("lookDown");
                return new Vector2(0, -moveSpeed);
            default:
                anim.SetTrigger("lookLeft");
                return new Vector2(-moveSpeed, 0);
        }
    }

    public IEnumerator Move(Vector2 force)
    {
        isMovingTimer = 0;
        while (isMovingTimer < moveDuration)
        {
            rb2D.velocity = force;
            isMovingTimer += Time.deltaTime;
            yield return null;
        }
        StopMovement();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void StopMovement()
    {
        rb2D.velocity = new Vector2(0, 0);
        moveTimer = 0;
        isMoving = false;
        isMovingTimer = moveDuration;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Octorok>().StopMovement();
            StopMovement();
        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            // how much the character should be knocked back
            var magnitude = 500;
            // calculate force vector
            var force = transform.position - col.transform.position;
            force.Normalize();
            // normalize force vector to get direction only and trim magnitude
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            StopMovement();
        }
    }
}
