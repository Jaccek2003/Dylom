using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidBody;

    private bool movementEnabled = true;
    public bool MovementEnabled
    {
        get => movementEnabled;
        set => movementEnabled = value;
    }

    private bool flipped = true;

    public float speed = 1500;
    public float runSpeed = 2500;

    private bool isLeftShift;
    private float moveHorizontal;
    private float moveVertical;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (movementEnabled)
        {
            isLeftShift = Input.GetKey(KeyCode.LeftShift);
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            SetFacingDirection(moveHorizontal);

            bool isMoving = moveHorizontal != 0 || moveVertical != 0;
            
            animator.SetBool("isRunning", isMoving);
            animator.SetBool("isSprinting", isLeftShift);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isSprinting", false);
        }
    }

    private void FixedUpdate()
    {
        float normalizedSpeed = isLeftShift ? runSpeed * Time.deltaTime : speed * Time.deltaTime;
        if (movementEnabled)
            rigidBody.velocity = new Vector3(moveHorizontal * normalizedSpeed, rigidBody.velocity.y, moveVertical * normalizedSpeed);
        else
            rigidBody.velocity = new Vector3(0, 0, 0);
    }



    private void SetFacingDirection(float direction)
    {
        if (direction > 0 && !flipped)
            Flip();

        if (direction < 0 && flipped)
            Flip();
    }

    private void Flip()
    {
        Transform renderer = GetRenderer();
        flipped = !flipped;

        Vector3 scale = renderer.localScale;
        scale.x *= -1;
        renderer.localScale = scale;
    }

    private Transform GetRenderer()
    {
        return transform.Find("Renderer");
    }
}
