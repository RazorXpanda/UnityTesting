using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class ObjectiveBehaviour : MonoBehaviour
{
    public Text scoreText;
    public float score;
    public Light2D pointLight;
    public float dimAmount;

    public float lightDimInterval;
    private float nextDimTime;

    private void Start()
    {
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
        scoreText.text = $"Score: {Mathf.Ceil(score).ToString()}";
    }

    private void SpiritCollected()
    {
        pointLight.pointLightOuterRadius += dimAmount * 2;
        pointLight.pointLightInnerRadius += dimAmount * 2;

        GetComponent<EntityData>().Healing(5);
    }

    private void OnDestroy()
    {
        GameEvents.current.onSpiritCollected -= SpiritCollected;
        GameEvents.current.ObjectiveLost();
    }
}
