using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class ObjectiveBehaviour : MonoBehaviour
{
    public Text HelpfulText;
    public Text scoreText;
    public float score;
    public Light2D pointLight;
    public float dimAmount;

    public float lightDimInterval;
    private float nextDimTime;

    static int amountOfSpiritTilFreeze = 10;
    public int amountOfSpiritCollected;

    private void Start()
    {
        amountOfSpiritCollected = 0;
        score = 0f;
        if (pointLight == null)
            pointLight = GetComponentInChildren<Light2D>();

        nextDimTime = lightDimInterval + Time.time;

        GameEvents.current.onSpiritCollected += SpiritCollected;
    }

    private void Update()
    {
        if(nextDimTime < Time.time)
        {
            pointLight.pointLightOuterRadius -= dimAmount;
            pointLight.pointLightInnerRadius -= dimAmount;
            nextDimTime = lightDimInterval + Time.time;
        }

        if (pointLight.pointLightOuterRadius <= 0)
            Destroy(this.gameObject);

        score += Time.deltaTime;
        if(scoreText != null)
            scoreText.text = $"Time: {Mathf.Ceil(score - 5f > 0 ? score - 5f : 0).ToString()}";
    }

    private void SpiritCollected(int _multiplier)
    {
        pointLight.pointLightOuterRadius += dimAmount * _multiplier;
        pointLight.pointLightInnerRadius += dimAmount * _multiplier;

        amountOfSpiritCollected++;
        if(amountOfSpiritCollected % amountOfSpiritTilFreeze == 0)
        {
            StartCoroutine(FreezeSpawners());
        }

        GetComponent<EntityData>().Healing(5);
    }

    IEnumerator FreezeSpawners()
    {
        SpawnerManager.current.DisableAllSpawners();
        foreach(var ghost in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(ghost);
        }
        HelpfulText.text = $"All the ghosts are sealed temporarily!\nUse this chance to go light more torches!";
        yield return new WaitForSeconds(15f);
        SpawnerManager.current.EnableAllSpawners();
        HelpfulText.text = "";
    }

    private void OnDestroy()
    {
        GameEvents.current.onSpiritCollected -= SpiritCollected;
        GameEvents.current.ObjectiveLost();
    }
}
