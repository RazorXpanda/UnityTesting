using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner : MonoBehaviour
{
    [SerializeField]
    private GameObject box;
    [SerializeField]
    private int spawnVal;
    [SerializeField]
    private float distanceVal; // more than 20 will seem obvious
    [SerializeField]
    private List<GameObject> clone = new List<GameObject>();
    Vector3 endPos;

    private void Start()
    {
        endPos = new Vector3 (0,0,0);
        for (int i = 0; i < spawnVal; i++)
        {
            clone.Add(box);
        }

        foreach (GameObject clones in clone)
        {
            Instantiate(clones, endPos, Quaternion.identity);
            endPos.x = endPos.x + distanceVal;
        }
    }

}
