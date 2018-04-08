using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {
    public GameObject playerPrefab;
    private Vector3 spawnPoint;
    private float laneWidth = 2f;
    public List<Color> colors;

    KeyCode[] playerJumpKeys= { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4
};
    KeyCode[] playerAccelKeys  = { KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightShift, KeyCode.LeftAlt};

	void Start ()
    {
        
        Debug.Log(name + " Start()");

        spawnPoint = Vector3.zero;

        colors = new List<Color>();
        colors.Add(MakeColor(105, 210, 231));
        colors.Add(MakeColor(224, 228, 204));
        colors.Add(MakeColor(243, 134, 48));
        colors.Add(MakeColor(167, 219, 216));
        SpawnAllPlayers();

        CameraScript camScript = FindObjectOfType<CameraScript>();
        camScript.StartTracking();
    }

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnPlayer(playerPrefab, i);
        }
    }

    void EnsureWithin(int v, int mn, int mx){
        if (v < mn || v > mx){
            Debug.LogError("Value out of range: " + v + " should be " + mn + " to " + mx);
        }
    }
    Color MakeColor(int r, int g, int b){
        EnsureWithin(r, 0, 255);
        EnsureWithin(g, 0, 255);
        EnsureWithin(b, 0, 255);
        return new Color(r / 255f, g / 255f, b / 255f);
    }
    void SpawnPlayer(GameObject prefab, int playerNumber)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, playerNumber * laneWidth);
        GameObject player = Instantiate(prefab, spawnPoint + randomOffset, Quaternion.identity);
        ColorPlayerIfPossible(playerNumber, player);

        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.SetPlayerNumber(playerNumber);
        movement.accelKey = playerAccelKeys[playerNumber];
        movement.jumpKey = playerJumpKeys[playerNumber];


    }

    private void ColorPlayerIfPossible(int playerNumber, GameObject player)
    {
        Renderer r = player.GetComponent<Renderer>();
        if (r)
        {
            r.material.color = colors[playerNumber];
        }
    }

	void Update () {
		
	}
}
