using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchInteraction : MonoBehaviour
{
    [SerializeField] private Light2D torchLight;
    [SerializeField] private GameObject torchFire;
    [SerializeField] private GameObject torchBase;
    [SerializeField] private GameObject nearbySpawner;
    public bool isLit;
    bool playerNearby;
    float lightingTime;

    private void Start()
    {
        isLit = false;
        playerNearby = false;
        lightingTime = 2f;
    }

    private void Update()
    {
        if (isLit)
        {
            torchLight.intensity = Random.Range(.6f, .8f);
            torchLight.pointLightInnerRadius = Random.Range(10.5f, 11.5f);
            torchLight.pointLightOuterRadius = Random.Range(20.5f, 21.5f);

            if(nearbySpawner != null && nearbySpawner.activeSelf == true)
            {
                nearbySpawner.SetActive(false);
                GameEvents.current.onSpiritCollected(100);
            }
        }
        else if(playerNearby)
        {
            if (Input.GetKey(KeyCode.E))
            {
                lightingTime -= Time.deltaTime;
            }

            if (isLit == false && Input.GetKeyUp(KeyCode.E))
                lightingTime = 2f;

            if (lightingTime <= 0)
            {
                isLit = true;
                torchLight.gameObject.SetActive(true);
                torchFire.SetActive(true);
                torchBase.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag.Contains("Player"))
        {
            playerNearby = false;

            if (isLit == false)
                lightingTime = 2f;
        }
    }
}
