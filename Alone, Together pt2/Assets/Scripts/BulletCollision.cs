using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public int baseDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var data = collision.gameObject.GetComponent<EntityData>();
        if (data != null)
        {
            // Call event to send damage to entity
            GameEvents.current.DamageReceived(data.GetInstanceID(), baseDamage);
        }
        Destroy(this.gameObject);
    }

    private void Start()
    {
        // Initiallized bullet trail data
        var trail = GetComponent<TrailRenderer>();
        if(trail != null)
        {
            trail.startColor = Color.red;
            trail.endColor = Color.yellow;
        }
        Destroy(this.gameObject, .5f);
    }
}
