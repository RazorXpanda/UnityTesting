using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    private Transform meleeTransform;
    public float attackRange = 2f;
    public LayerMask enemyLayers;
    public int meleeDamage = 30;

    void Start()
    {
        meleeTransform = this.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeTransform.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            var data = enemy.gameObject.GetComponent<EntityData>();
            GameEvents.current.DamageReceived(data.GetInstanceID(), meleeDamage);
        }
    }
}