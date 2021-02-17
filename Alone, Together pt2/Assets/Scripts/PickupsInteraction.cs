using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsInteraction : MonoBehaviour
{
    [SerializeField] private float despawnTime;
    private float lifetime;

    private void Start()
    {
        lifetime = Time.time + despawnTime;
        Debug.Log($"Despawn at {lifetime}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var entityData = collision.gameObject.GetComponent<EntityData>();
        if(entityData.tag == "Player")
        {
            this.GetComponent<Collider2D>().enabled = false;
            lifetime = Time.time + despawnTime;
            switch (this.tag)
            {
                case ("Health"):
                    GameEvents.current.onDamageReceived(entityData.GetInstanceID(), -10);
                    Destroy(this.gameObject);
                    break;
                case ("Strength"):
                    Destroy(this.gameObject);
                    break;
                case ("Spirit"):
                    // Make the spirit pickup do something
                    StartCoroutine(SpiritPickupBehaviour());
                    break;
                default:
                    return;
            }
        }
    }

    private void Update()
    {
        if (lifetime < Time.time)
            Destroy(this.gameObject);
    }

    private IEnumerator SpiritPickupBehaviour()
    {
        var objective = GameObject.FindGameObjectWithTag("Objective");
        LeanTween.move(this.gameObject, objective.transform.position, .75f).setEase(LeanTweenType.easeSpring);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
