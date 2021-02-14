using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int baseDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var data = collision.gameObject.GetComponent<EntityData>();
        print("Trigger detected");
        if (data != null)
        {
            Debug.Log($"Hit an entity at point {collision.transform.position}");
            Debug.Log($"Entity name: {collision.gameObject.name}");

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        var trail = GetComponent<TrailRenderer>();
        if(trail != null)
        {
            trail.startColor = Color.red;
            trail.endColor = Color.yellow;
        }
    }
}
