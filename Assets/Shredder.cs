using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //TODO: check if player or something else
        //TODO: call eliminate on player, if player
        Destroy(collider.gameObject);
	}
}
