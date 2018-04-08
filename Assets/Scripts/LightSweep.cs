using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSweep : MonoBehaviour {
    public float rotationSpeed;
	
	void Update () {

        transform.Rotate(Vector3.right, rotationSpeed);

	}
}
