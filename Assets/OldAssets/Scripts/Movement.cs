using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    SpriteRenderer sr;
    Animator anim;

    public float upForce = 100;
    public float speed = 1500;
    public float runSpeed = 2500;

    public bool isGrounded = false;

    bool isLeftShift;
    float moveHorizontal;
    float moveVertical;

    public Transform inventory;
    public Vector3 offset;

    public Transform backpack;
    public Vector3 offset1;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        isLeftShift = Input.GetKey(KeyCode.LeftShift);
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        // Flip sprite
        if (moveHorizontal > 0)
        {
            sr.flipX = false;
        }
        else if (moveHorizontal < 0)
        {
            sr.flipX = true;
        }

        // Animacje
        bool isMoving = moveHorizontal != 0 || moveVertical != 0;
        anim.SetBool("isRunning", isMoving);

        // Skok
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
            isGrounded = false;
            // anim.SetBool("isGrounded", false);
            anim.SetTrigger("jump");
        }

        // Aktualizacja pozycji ekwipunku i plecaka
        inventory.position = transform.position + offset;
        backpack.position = transform.position + offset1;
    }

    private void FixedUpdate()
    {
        float normalizedSpeed = isLeftShift ? runSpeed * Time.deltaTime : speed * Time.deltaTime;
        rb.velocity = new Vector3(moveHorizontal * normalizedSpeed, rb.velocity.y, moveVertical * normalizedSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // anim.SetBool("isGrounded", true);
        }
    }

    public void ResetMovement()
    {
        transform.rotation = Quaternion.identity;
        if (Camera.main != null)
        {
            Camera.main.transform.rotation = Quaternion.identity;
        }
    }

    public void Save()
    {
        SaveData.instance.playerX = transform.position.x;
        SaveData.instance.playerY = transform.position.y;
        SaveData.instance.playerZ = transform.position.z;
    }

    public void Load()
    {
        transform.position = new Vector3(SaveData.instance.playerX, SaveData.instance.playerY, SaveData.instance.playerZ);
    }
}
