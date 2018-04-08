using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    public List<GameObject> thingsToTrack;
    public float debugMoveSpeed = 1;
    public float maxAcceptablePlayerDistance = 12f;
    public Announcer announcer;
    public float cameraSpeed = 1f;

    #region unity methods
    void Start()
    {
        announcer = FindObjectOfType<Announcer>();
    }

    public void StartTracking(){
        thingsToTrack = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            HandleDebugCameraMovementKeys();
            return;
        }
        if (thingsToTrack.Count < 1)
        {
            return;
        }
        if (thingsToTrack.Count == 1){
            PlayerMovement player = thingsToTrack[0].GetComponent<PlayerMovement>();
            announcer.Announce("Player  " + player.GetPlayerNumber() + " Wins!", 3f);
        }

        Bounds b = FindBoundsOfTargets();
        Vector3 desiredPosition = new Vector3(b.center.x, b.center.y, -10f);
        //TODO: became a bit jerky once lerp was added.  Tried LateUpdate()
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * cameraSpeed);
        DealWithStragglers(b);
    }

    private void DealWithStragglers(Bounds b)
    {
        if (b.extents.x * 2 > maxAcceptablePlayerDistance)
        {
            if (thingsToTrack.Count == 1){
                return;
            }

            GameObject leftmost = FindLeftmostThing();
            if (leftmost)
            {
                PlayerMovement player = leftmost.GetComponent<PlayerMovement>();
                int playerNum = player.GetPlayerNumber();
                announcer.Announce("Player " + playerNum + " eliminated!", 1f);

                //TODO: consider instead teleporting to lead player and exhausting one life
                player.EliminateSelf();
                thingsToTrack.Remove(leftmost);
            }
        }
    }

    #endregion
    #region other methods
    GameObject FindLeftmostThing()
    {
        float minX = float.PositiveInfinity;
        GameObject leftmostObject = null;

        foreach (GameObject go in thingsToTrack)
        {
            if (go.transform.position.x < minX)
            {
                leftmostObject = go;
                minX = leftmostObject.transform.position.x;
            }
        }
        return leftmostObject;

    }
    private void HandleDebugCameraMovementKeys()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * debugMoveSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * debugMoveSpeed);
        }
    }



    private Bounds FindBoundsOfTargets()
    {
        Bounds b = new Bounds(thingsToTrack[0].transform.position, new Vector3(0.1f, 0.1f, 0.1f));
        foreach (GameObject go in thingsToTrack)
        {
            b.Encapsulate(go.transform.position);
        }

        return b;
    }
    #endregion
}
