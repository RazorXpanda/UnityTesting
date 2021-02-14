using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    #region Debug
    public float bulletSpeed;
    #endregion

    private GameObject target;
    private Rigidbody2D enemyRB;

    public float detectionRange;
    public float approachMin;           //closest it wants to go to the player
    public float approachMax;           //farthest away it will allow itself to halt
    private float approachRange;
    private float nextShotTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] [Range(0, 3)] float moveSpeed;
    
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        approachRange = Random.Range(approachMin, approachMax);
        nextShotTime = Time.time + 5f;
    }

    private void Update()
    {
        ScanForTargetInRange();
    }

    // Scan for available targets in range
    void ScanForTargetInRange()
    {
        var entities = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var entity in entities)
        {
            if (entity.CompareTag("Player"))
            {
                Debug.LogWarning($"Detected a target at {entity.transform.position}");
                if(nextShotTime < Time.time)
                {
                    Shoot();
                    nextShotTime = Time.time + 5f;
                }
            }
        }
    }
    void Shoot()
    {
        var direction = target.transform.position - transform.position;
        direction.z = 0f;
        direction.Normalize();

        var bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        LeanTween.move(bullet, direction * 10f, bulletSpeed);
        Destroy(bullet, bulletSpeed);
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 playerPos = target.transform.position;

            //Move towards the target
            Vector3 moveDirection = playerPos - transform.position;
            if (moveDirection.magnitude > approachRange)
            {
                Vector2 moveDir = new Vector2(moveDirection.x, moveDirection.y) * 0.5f;
                enemyRB.MovePosition(enemyRB.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            }

            //Look towards the target
            Vector2 lookDir = target.transform.position - transform.position;
            float angle = Vector2.Angle(Vector2.up, lookDir);

            if (lookDir.x >= 0)
            {
                transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }
    }
}
