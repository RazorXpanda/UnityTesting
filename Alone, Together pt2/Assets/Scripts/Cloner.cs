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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            ChangeRandomColor();
    }

    void ChangeRandomColor()
    {
        if (clone.Count <= 0)
            return;

        var newColor = new Color
            (
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );

        clone[0].GetComponent<Renderer>().sharedMaterial.color = newColor;
    }
}
