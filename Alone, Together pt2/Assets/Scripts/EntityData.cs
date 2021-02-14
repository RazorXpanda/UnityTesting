using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public int health;

    private void OnEnable()
    {
        health = 100;
    }

    private void Start()
    {
        GameEvents.current.onDamageReceived += TakeDamage;
    }

    private void TakeDamage(int _id, int _damage)
    {
        if(this.GetInstanceID() == _id)
            health -= _damage;

        if (health <= 0)
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDamageReceived -= TakeDamage;
    }
}
