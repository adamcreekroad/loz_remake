using UnityEngine;
using System.Collections;

public class Octorok : MovingObject
{
    public string currentDirection;

    public float moveInterval;
    public float moveSpeed;
    public float moveDuration;

    public float attackDamage;
    public float attackInterval;
    public float attackTimer;
    public float bulletSpeed;

    private float moveTimer;
    private float isMovingTimer;

    private bool isMoving;

    public Transform target;
    public GameObject bullet;
    public Transform shootPointUp;
    public Transform shootPointRight;
    public Transform shootPointDown;
    public Transform shootPointLeft;

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
        attackTimer += Time.deltaTime;
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
                currentDirection = "up";
                return new Vector2(0, moveSpeed);
            case 1:
                anim.SetTrigger("lookRight");
                currentDirection = "right";
                return new Vector2(moveSpeed, 0);
            case 2:
                anim.SetTrigger("lookDown");
                currentDirection = "down";
                return new Vector2(0, -moveSpeed);
            case 3:
                anim.SetTrigger("lookLeft");
                currentDirection = "left";
                return new Vector2(-moveSpeed, 0);
            default:
                anim.SetTrigger("lookDown");
                currentDirection = "down";
                return new Vector2(0, -moveSpeed);
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
            var magnitude = 750;
            // calculate force vector
            var force = transform.position - col.transform.position;
            force.Normalize();
            // normalize force vector to get direction only and trim magnitude
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(-force * magnitude);
            StopMovement();
        }
    }

    public void Attack(string attackDir)
    {
        if (attackTimer > attackInterval)
        {
            if (attackDir == "up")
            {
                Vector2 direction = new Vector2(0, 1);
                direction.Normalize();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointUp.transform.position,
                    shootPointUp.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                attackTimer = 0;
            }
            else if (attackDir == "right")
            {
                Vector2 direction = new Vector2(1, 0);
                direction.Normalize();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position,
                    shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                attackTimer = 0;
            }
            else if (attackDir == "down")
            {
                Vector2 direction = new Vector2(0, -1);
                direction.Normalize();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointDown.transform.position,
                    shootPointDown.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                attackTimer = 0;
            }
            else if (attackDir == "left")
            {
                Vector2 direction = new Vector2(-1, 0);
                direction.Normalize();
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position,
                    shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                attackTimer = 0;
            }
        }
    }
}
