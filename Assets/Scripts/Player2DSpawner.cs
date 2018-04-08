using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DSpawner : MonoBehaviour {
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

        //colors = MakeGoldfishPalette();
        colors = MakePrimaryPalette();
        SpawnAllPlayers();

        CameraScript2D camScript = FindObjectOfType<CameraScript2D>();
        camScript.StartTracking();
    }

    private List<Color> MakeGoldfishPalette()
    {
        List<Color> cs = new List<Color>();
        cs.Add(MakeColor(105, 210, 231));
        cs.Add(MakeColor(224, 228, 204));
        cs.Add(MakeColor(243, 134, 48));
        cs.Add(MakeColor(167, 219, 216));
        return cs;
    }
    private List<Color> MakePrimaryPalette()
    {
        List<Color> cs = new List<Color>();
        cs.Add(Color.red);
        cs.Add(Color.green);
        cs.Add(Color.blue);
        cs.Add(Color.white);
        return cs;
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
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
        GameObject player = Instantiate(prefab, spawnPoint + randomOffset, Quaternion.identity);
        ColorPlayerIfPossible(playerNumber, player);

        PlayerMovement2D movement = player.GetComponent<PlayerMovement2D>();
        movement.SetPlayerNumber(playerNumber);
        movement.accelKey = playerAccelKeys[playerNumber];
        movement.jumpKey = playerJumpKeys[playerNumber];


    }

    private void ColorPlayerIfPossible(int playerNumber, GameObject player)
    {
        SpriteRenderer r = player.GetComponentInChildren<SpriteRenderer>();
        if (r)
        {
            r.color = colors[playerNumber];
        }
    }

	void Update () {
		
	}
}
