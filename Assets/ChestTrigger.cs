using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{

    public GameObject collectableThingPrefab;
    public int amount;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = Instantiate(collectableThingPrefab, transform.position, Quaternion.identity);
        Collectable collectable = go.GetComponent<Collectable>();
        Debug.Log("Got " + collectable.label + " x" + amount);
        Destroy(gameObject);
    }
}
