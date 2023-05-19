using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public float moveSpeed;
    public float horizontalInput;
    public bool isOnGround = true;
    public float jumpForce;  
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveSpeed * horizontalInput * Time.deltaTime);

        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalInput));

        if (horizontalInput <= 0)
        {
            sr.flipX = true;

        }
        else if (horizontalInput >= 0)
        {
            sr.flipX = false;

        }
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            isOnGround = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        else
        {
            StartCoroutine(JumpWait());
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("Winking");
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isJumping", false);
    }

}
