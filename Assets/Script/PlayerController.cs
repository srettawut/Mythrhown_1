using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 200f;
    public float moveSpeed;

    public float normalSpeed = 10f;
    public float boostSpeed = 20f;

    bool jump = false;
    bool grounded = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 35.0f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.1f;

    [SerializeField] private TrailRenderer tr;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = true;
            animator.SetTrigger("Jump");

        }*/
        if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            moveSpeed = boostSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetTrigger("Shoot");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetTrigger("Shoot");
        }
        if (Input.GetButtonDown("Fire1") && grounded)
        {
            animator.SetTrigger("Shoot");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && grounded)
        {
            animator.SetTrigger("Happy");
        }     
        

        float moveX = Input.GetAxisRaw("Horizontal");
       
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        animator.SetBool("IsWalking", moveX != 0);
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }         
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 key pressed!");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        if (jump == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
            Debug.Log("Jump force applied!");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
}

