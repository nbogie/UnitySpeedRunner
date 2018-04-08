using UnityEngine;

public class Splash : MonoBehaviour {

	void Start () {
        Invoke("Next", 2f);
	}
	
    void Next(){
        FindObjectOfType<LevelManager>().LoadMenuScreen();
    }

	void Update () {
		
	}
}
