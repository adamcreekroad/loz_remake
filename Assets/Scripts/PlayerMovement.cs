using UnityEngine;
using System.Collections;

public class PlayerMovement : MovingObject
{
    public Rigidbody2D rbody;
    public Animator anim;

    public bool movingAllowed;

    public float knockbackDuration;

    // Use this for initialization
    protected override void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movementVector != Vector2.zero)
            {
                anim.SetBool("is_walking", true);
                anim.SetFloat("input_x", movementVector.x);
                anim.SetFloat("input_y", movementVector.y);
            }
            else
            {
                anim.SetBool("is_walking", false);
            }

            rbody.MovePosition(rbody.position + movementVector * Time.deltaTime * 15);
        }
    }

    public void KnockBack()
    {
        StartCoroutine(KnockingBack());
    }

    private IEnumerator KnockingBack()
    {
        canMove = false;
        float knockbackTimer = 0f;
        // gameObject.GetComponent<Animation>().Play("Invulnerable");
        while (knockbackTimer < knockbackDuration)
        {
            knockbackTimer += Time.deltaTime;
            yield return null;
        }
        rbody.velocity = new Vector2(0, 0);
        canMove = true;
    }

        
}
