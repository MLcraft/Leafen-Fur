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
    private bool control = false;

    private Vector3 cameraPos;
    private float camVelocityx = 0;
    private float camVelocityy = 0;
    private Vector3 smooth;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        velocity = new Vector3(0, 0, 0);
        if (this.gameObject.name == "Leader")
        {
            control = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                isGrounded = false;
            }
            camVelocityx = 0.1f * (transform.position.x - mainCamera.transform.position.x);
            camVelocityy = 0.1f * (transform.position.y - mainCamera.transform.position.y+1);
        }
    }
    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            moveDirection = 0;
            if (control == true)
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
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                    if (moveDirection < 0 && facingRight)
                    {
                        facingRight = false;
                        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    }
                }
            }
            // Camera follow
            if (mainCamera)
            {
                /*camVelocityx = 0.01f*(transform.position.x - mainCamera.transform.position.x);

                if (camVelocityx > 0.02)
                {
                    camVelocityx = camVelocityx * 1.05f + 0.03f;
                }

                if (camVelocityx < -0.02)
                {
                    camVelocityx = camVelocityx * 1.05f - 0.03f;
                }*/

                //Debug.Log(camVelocityx);

                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
                mainCamera.transform.Translate(camVelocityx, camVelocityy, 0);
            }
            transform.Translate(velocity);
        }
        velocity.x *= 0.8f;
        velocity.x += moveDirection * Time.deltaTime;
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
