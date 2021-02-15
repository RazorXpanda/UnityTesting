using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityData : MonoBehaviour
{
    /*
     * Holds entity's data that triggers certain events
     * Separate from controller's script to purely just handles data.
     * Also used to filter out which collider needs event triggers
     */
    private static int startingHealth = 100;
    private int health;

    [Header("UI")]
    public Color healthColor;
    public Slider healthSlider;
    public Image fillImage;

    private void OnEnable()
    {
        health = startingHealth;
        healthSlider.maxValue = startingHealth;
        healthSlider.minValue = 0f;
        fillImage.color = healthColor;
        UIManagement.current.SetOverlayHealthUI(healthSlider, health);
    }

    private void Start()
    {
        // Subscribe to onDamageReceived event
        GameEvents.current.onDamageReceived += TakeDamage;
    }

    private void TakeDamage(int _id, int _damage)
    {
        if(this.GetInstanceID() == _id)
        {
            health -= _damage;
            UIManagement.current.SetOverlayHealthUI(healthSlider, health);
            if (health <= 0)
            {
                if (Random.Range(0, 100) > 70)
                {
                    Debug.Log("Instantiate drops");
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from all events here
        GameEvents.current.onDamageReceived -= TakeDamage;
    }
}
