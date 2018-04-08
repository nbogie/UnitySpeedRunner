using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	void Start () {
	}

    public void LoadMenuScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(2);
    }

	void Update () {
		
	}
}
