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
    private int startingHealth = 100;
    private int health;

    public GameObject dropPrefab;
    public Transform spawnPoint;
    public int dropChance;

    [Header("UI")]
    public Color healthColor;
    public Slider healthSlider;
    public Image fillImage;

    private void OnEnable()
    {
        if(healthSlider != null)
        {
            health = startingHealth;
            healthSlider.maxValue = startingHealth;
            healthSlider.minValue = 0f;
            fillImage.color = healthColor;
            UIManagement.current.SetOverlayHealthUI(healthSlider, health);
        }
    }

    private void Start()
    {
        // Subscribe to onDamageReceived event
        GameEvents.current.onDamageReceived += TakeDamage;
        UIManagement.current.SetOverlayHealthUI(healthSlider, health);
    }

    public void Healing(int _amount)
    {
        health = health + _amount > 100 ? 100 : health + _amount;
        UIManagement.current.SetOverlayHealthUI(healthSlider, health);
    }

    private void TakeDamage(int _id, int _damage)
    {
        if(this.GetInstanceID() == _id)
        {
            health -= _damage;

            if (health <= 0)
            {
                if (Random.Range(0, 100) < dropChance)
                {
                    Instantiate(dropPrefab, transform.position, Quaternion.identity);
                }
                if(this.tag.Contains("Player"))
                {
                    this.transform.position = spawnPoint.position;
                    StartCoroutine(Respawning());
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
            else if (health > 100)
                health = 100;

            if (healthSlider != null)
                UIManagement.current.SetOverlayHealthUI(healthSlider, health);
        }
    }

    IEnumerator Respawning()
    {
        var _playerController = gameObject.GetComponent<playerController>();
        var _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _playerController.enabled = false;
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2f);
        _playerController.enabled = true;
        _spriteRenderer.enabled = true;
        health = startingHealth;
        UIManagement.current.SetOverlayHealthUI(healthSlider, health);
    }

    private void OnDestroy()
    {
        // Unsubscribe from all events here
        GameEvents.current.onDamageReceived -= TakeDamage;
    }
}
