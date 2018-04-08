using UnityEngine;

public class Swoosh : MonoBehaviour
{

    void Start()
    {
    }
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Swoosh hit " + collider.gameObject.name);
        Destroy(collider.gameObject);
    }
}
