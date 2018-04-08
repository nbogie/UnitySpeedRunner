using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    Animator animator;

    [Header("gameplay tuning")]
    public float accel;
    public float jumpAccel;
    [Tooltip("Maximum horizontal speed under which to allow further acceleration")]
    public float maxXSpeed = 3f;


    [Header("Controls")]
    public KeyCode accelKey;
    public KeyCode jumpKey;
    public bool autoAccelerate = true;

    [Header("Wiring")]
    public GameObject jumpEffect;
    public GameObject slashPrefab;
    public BoxCollider2D groundCollider;

    private int playerNumber;
    private Rigidbody2D rb;
    private Renderer rndr;
    private float jumpingThresholdVelocity = 0.1f;
    private float fallingThresholdVelocity = -0.1f;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rndr = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        ColorPlayer(IsGrounded() ? Color.white : Color.yellow);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            rb.velocity = Vector3.zero;
            StartSlashAnim();
        }
        else //no accel if we're slashing
        {

            if (autoAccelerate)
            {
                if (rb.velocity.x < maxXSpeed)
                {
                    rb.AddForce(Vector3.right * accel * Time.deltaTime);
                }
            }
            else
            {
                rb.AddForce(Input.GetAxis("Horizontal") * Vector3.right * accel * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector3.up * jumpAccel * Time.deltaTime, ForceMode2D.Impulse);
            if (jumpEffect)
            {
                SpawnJumpEffect();
            }
        }
        if (rb.velocity.y < fallingThresholdVelocity)
        {
            animator.SetBool("isFalling", true);
            animator.SetBool("isJumping", false);
        }
        else if (rb.velocity.y > jumpingThresholdVelocity)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isLanded", true);
            animator.SetBool("isJumping", false);
        }
    }

    private void StartSlashAnim()
    {
        animator.SetTrigger("slashTrigger");
    }
    void SpawnSlash()
    {
        GameObject obj = Instantiate(slashPrefab, transform.position + Vector3.up, Quaternion.identity);
        Destroy(obj, 1);
    }
    bool IsGrounded()
    {
        return groundCollider.IsTouchingLayers();
    }

    void ColorPlayer(Color c)
    {
        rndr.material.color = c;
    }

    void SpawnJumpEffect()
    {
        GameObject effect = Instantiate(jumpEffect, transform.position, Quaternion.identity);
        ParticleSystem psys = effect.GetComponent<ParticleSystem>();
        psys.GetComponent<Renderer>().material.color = rndr.material.color;
        Destroy(effect, 2f);
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public void SetPlayerNumber(int n)
    {
        playerNumber = n;
    }
    public void EliminateSelf()
    {
        Destroy(gameObject);
    }
}
