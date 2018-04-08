using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {
    public float moveSpeed;

    private Rigidbody2D rb;
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
        rb.velocity = Vector3.left * moveSpeed * Time.deltaTime;
	}
}
