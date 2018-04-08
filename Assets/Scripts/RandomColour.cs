using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColour : MonoBehaviour {

	void Start () {
        Renderer r = GetComponent<Renderer>();
            r.material.color = MakeRandomColour();

	}
	
    Color MakeRandomColour(){
        return Random.ColorHSV(0f, 1f, 1, 1f, 1f, 1f);

    }

	void Update () {
		
	}
}
