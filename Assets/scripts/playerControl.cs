using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator anim;
    private float mov;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;  
    private bool facingRight = true;
    private bool onGround = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        UpdateAnimationParameters();
        HandleJump();

    }
    private void HandleMovement()
    {
        mov = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(mov * speed, rb2d.velocity.y);

        if (mov > 0 && !facingRight)
        {
            Flip();
        }
        else if (mov < 0 && facingRight)
        {
            Flip();
        }

      
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void UpdateAnimationParameters()
    {
        anim.SetFloat("vspeed", rb2d.velocity.y);
        anim.SetFloat("move", Mathf.Abs(mov));
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
            anim.SetBool("onland", true);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = false;
            anim.SetBool("onland", false);
        }
    }



}
