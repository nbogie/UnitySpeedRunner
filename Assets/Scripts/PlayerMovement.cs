using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

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

    private int playerNumber;
    private Rigidbody rb;
    private Renderer rndr;


    void Start () {
        rb = GetComponent<Rigidbody>();
        rndr = GetComponent<Renderer>();
	}
	
	void Update () {
        if (autoAccelerate){
            if (rb.velocity.x < maxXSpeed){
                rb.AddForce(Vector3.right * accel * Time.deltaTime);
            } 
        } else {
            rb.AddForce(Input.GetAxis("Horizontal") * Vector3.right * accel * Time.deltaTime);
        }

        if (Input.GetKeyDown(jumpKey))
        {
            rb.AddForce(Vector3.up * jumpAccel * Time.deltaTime, ForceMode.Impulse);
            GameObject effect = Instantiate(jumpEffect, transform.position, Quaternion.identity);
            ParticleSystem psys = effect.GetComponent<ParticleSystem>();
            psys.GetComponent<Renderer>().material.color = rndr.material.color;
            Destroy(effect, 2f);
        }

	}
    public int GetPlayerNumber(){
        return playerNumber;
    }
    public void SetPlayerNumber(int n){
        playerNumber = n;
    }
    public void EliminateSelf()
    {        
        Destroy(gameObject);
    }
}
