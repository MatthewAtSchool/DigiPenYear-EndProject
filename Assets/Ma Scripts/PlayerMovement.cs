using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 12;
    public float gravity = -9.81f;
    public float gravity2 = -8f;
    public float jumpHeight = 3;
    Vector3 velocity;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    float levelLoadWaitTime = 1f;

    float scaleShrinkXZ = .5f;
    float scaleShrinkY = .25f;
    bool isSmaller = false;

    // Update is called once per frame
    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = gravity2;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                speed = 12f;;
                jumpHeight = 4f;
                gravity = -25f;
                gravity2 = -8f;
                if(isSmaller == true)
                {
                    transform.localScale += new Vector3(scaleShrinkXZ, scaleShrinkY, scaleShrinkXZ);
                    isSmaller = false;
                }
                break;
            case "Death":
                DeathSequence();
                break;
            case "Finish":
                break;
            case "Jump Boost":
                jumpHeight = 8f;
                break;
            case "Speed Boost":
                speed = 20f;
                break;
            case "Low Gravity":
                gravity2 = -4f;
                gravity = -12f;
                break;
            case "Shrink":
                if (isSmaller == true)
                {
                    return;
                }
                transform.localScale -=  new Vector3(scaleShrinkXZ, scaleShrinkY, scaleShrinkXZ);
                isSmaller = true;
                break;
        }
    }

    void DeathSequence()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("Death", levelLoadWaitTime);
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}   