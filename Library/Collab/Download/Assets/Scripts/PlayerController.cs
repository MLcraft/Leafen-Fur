using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 velocity;
    public Camera mainCamera;
    private float moveDirection = 0;
    private bool isGrounded = false;
    private bool facingRight = true;
    public float jumpHeight = 5.0f;
    public float fallSpeed = 0.00001f;
    public float walkSpeed = 0.005f;
    public float flySpeed = 0.004f;
    public bool leader = true;
    public bool control = false;
    public bool trapped = false;

    private float onGround = 0;
    private Vector3 cameraPos;
    private float camVelocityx = 0;
    private float camVelocityy = 0;
    private Vector3 smooth;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        velocity = new Vector3(0, 0, 0);
        if (this.gameObject.name == "Leader")
        {
            control = true;
        }
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("isJumping", false);
    }

    public void Trapped()
    {
        trapped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            control ^= true;
            leader ^= true;
        }
        if (this.gameObject.name == "Leader" && leader == true)
        {
            control = true;
        }
        if (this.gameObject.name != "Leader" && leader == false)
        {
            control = true;
        }
    }
    void LateUpdate()
    {
        if (control == true)
        {
            if (rb.velocity.y <= 0.001 && rb.velocity.y >= -0.001)
            {
                if (onGround < 0.004)
                {
                    onGround += Time.deltaTime;
                    animator.SetBool("isJumping", false);
                }
            }
            else
            {
                onGround = 0;
            }
            if (Input.GetKeyDown(KeyCode.Space) && onGround > 0.004)
            {
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                isGrounded = false;
                animator.SetBool("isJumping", true);
            }
            camVelocityx = 0.1f * (transform.position.x - mainCamera.transform.position.x);
            camVelocityy = 0.1f * (transform.position.y - mainCamera.transform.position.y + 0.5f);

        }
    }
    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            moveDirection = 0;
            if (control == true && trapped == false)
            {

                // Movement controls
                if ((Input.GetKey(KeyCode.A)))
                {
                    if (isGrounded)
                    {
                        moveDirection = -walkSpeed;
                    }
                    else
                    {
                        moveDirection = -flySpeed;
                    }

                }
                if (Input.GetKey(KeyCode.D))
                {
                    if (isGrounded)
                    {
                        moveDirection = walkSpeed;
                    }
                    else
                    {
                        moveDirection = flySpeed;
                    }

                }

                // Change facing direction
                if (moveDirection != 0)
                {
                    if (moveDirection > 0 && !facingRight)
                    {
                        facingRight = true;
                        //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                        animator.transform.Rotate(new Vector3(0, 180, 0));
                    }
                    if (moveDirection < 0 && facingRight)
                    {
                        facingRight = false;
                        //transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                        animator.transform.Rotate(new Vector3(0, 180, 0));
                    }
                }
            }
            // Camera follow
            transform.Translate(velocity);
            if (mainCamera)
            {

                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -5);
                mainCamera.transform.Translate(camVelocityx, camVelocityy, -5);
            }
        }
        velocity.x *= 0.8f;
        velocity.x += Math.Abs(moveDirection) * Time.deltaTime;
        velocity = new Vector3(velocity.x, velocity.y, velocity.z);
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "GameOver")
        {
            // SceneManager.LoadScene("Bad_Ending");
            SceneManager.LoadScene("Lose_Screen");
        }
    }
}
