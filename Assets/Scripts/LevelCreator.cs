using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
    public GameObject sectionPrefab;
    public float furthestSpawnedLevelPosition = -1f;

    void Start () {
        
	}

    Bounds FindSectionBounds(GameObject section)
    {
        GameObject firstChild = section.transform.GetChild(0).gameObject;
        Bounds b = new Bounds(firstChild.transform.position, new Vector3(0.1f, 0.1f, 0.1f));
        foreach (Transform t in section.transform)
        {
            b.Encapsulate(t.position);
        }
        return b;
    }
    void PaintLevel(GameObject section)
    {
        Color color = Random.ColorHSV(0f, 1f, 0.2f, 0.4f, 1f, 1f);
        foreach (Transform t in section.transform)
        {
            GameObject go = t.gameObject;
            Renderer r = go.GetComponent<Renderer>();

            if(r){
                r.material.color = color;
            }
        }
    }
    void SpawnNewLevelSectionsTo(float xMax){
        Debug.Log("spawn new level sections at " + xMax);
        GameObject section = Instantiate(sectionPrefab);
        PaintLevel(section);
        Bounds sectionBounds = FindSectionBounds(section);
        section.transform.position = new Vector3(xMax + sectionBounds.extents.x, 0, 0);
        furthestSpawnedLevelPosition = section.transform.position.x + sectionBounds.extents.x;

    }

	void Update () {
        Vector3 cpos = Camera.main.transform.position;

        if( cpos.x > furthestSpawnedLevelPosition){
            SpawnNewLevelSectionsTo(cpos.x);

        }
	}
}
