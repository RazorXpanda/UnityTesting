using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    #region Debug
    public float bulletSpeed;
    #endregion
    private GameObject target;
    private Rigidbody2D enemyRB;

    public float shotDelay;
    public float detectionRange;
    public float approachMin;           //closest it wants to go to the player
    public float approachMax;           //farthest away it will allow itself to halt
    private float approachRange;
    private float nextShotTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] [Range(0, 5)] float moveSpeed;
    
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
            CheckSurrounding();
    }

    // Scan for available targets in range
    void ScanForTargetInRange()
    {
        var entities = Physics2D.OverlapCircleAll(transform.position, detectionRange);

        foreach (var entity in entities)
        {
            if (entity.CompareTag("Player"))
            {
                if(nextShotTime < Time.time)
                {
                    if (IsAnythingInTheWay() == false)
                    {
                        Shoot();
                        nextShotTime = Time.time + shotDelay;
                    }
                }
            }
        }
    }

    bool IsAnythingInTheWay()
    {
        bool value = false;
        var lineOfSight = Physics2D.RaycastAll(shootingPoint.position, target.transform.position - transform.position);

        foreach(var item in lineOfSight)
        {
            if (item.transform.tag.Contains("Enemy"))
                value = true;
        }

        return value;
    }

    void Shoot()
    {
        var direction = target.transform.position - transform.position;
        direction.z = 0f;
        var _bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        LeanTween.move(_bullet, shootingPoint.position + (direction.normalized * 10f), .25f);
    }

    void FixedUpdate()
    {
        Move();
        RotateToTarget();
    }

    private void Move()
    {
        Move(CheckSurrounding());
        // Makes move a bit smarter by having the enemy circle around the target if it is already in range
        /*
        if (target != null)
        {
            Vector3 playerPos = target.transform.position;

            //Move towards the target
            Vector3 moveDirection = playerPos - transform.position;
            if (Mathf.Abs(moveDirection.magnitude) > approachRange)
            {
                Vector2 moveDir = new Vector2(moveDirection.x, moveDirection.y) * 0.5f;
                enemyRB.MovePosition(enemyRB.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            }
        }
        */
    }

    private void Move(Vector3 targetPos)
    {
        var moveDir = (targetPos - transform.position).normalized;
        enemyRB.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateToTarget()
    {
        if(target != null)
        {
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

    Vector3 CheckSurrounding()
    {
        // 8 directions
        Vector3 N, NE, E, SE, S, SW, W, NW;
        N = transform.position + (transform.up * 2f);
        NE = transform.position + (transform.up + transform.right).normalized * 2f;
        E = transform.position + (transform.right * 2f);
        SE = transform.position + (transform.right - transform.up).normalized * 2f;
        S = transform.position + (-transform.up) * 2f;
        SW = transform.position + (-transform.up - transform.right).normalized * 2f;
        W = transform.position + (-transform.right * 2f);
        NW = transform.position + (-transform.right + transform.up).normalized * 2f;

        Dictionary<Vector3, float> directionalKeys = new Dictionary<Vector3, float>();

        #region Directional Raycast
        // Determine points value for North
        float Npoints = 100f;
        if ((N - target.transform.position).magnitude <= approachRange)
        {
            Npoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, N, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    Npoints -= 5f;
                }
            }
        }
        Npoints /= (N - target.transform.position).magnitude;
        directionalKeys.Add(N, Npoints);

        float NEpoints = 100f;
        if ((NE - target.transform.position).magnitude <= approachRange)
        {
            NEpoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, NE, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    NEpoints -= 5f;
                }
            }
        }
        NEpoints /= (NE - target.transform.position).magnitude;
        directionalKeys.Add(NE, NEpoints);

        float Epoints = 100f;
        if ((E - target.transform.position).magnitude <= approachRange)
        {
            Epoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, E, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    Epoints -= 5f;
                }
            }
        }
        Epoints /= (E - target.transform.position).magnitude;
        directionalKeys.Add(E, NEpoints);

        float SEpoints = 100f;
        if ((SE - target.transform.position).magnitude <= approachRange)
        {
            SEpoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, SE, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    SEpoints -= 5f;
                }
            }
        }
        SEpoints /= (SE - target.transform.position).magnitude;
        directionalKeys.Add(SE, NEpoints);

        float Spoints = 100f;
        if ((S - target.transform.position).magnitude <= approachRange)
        {
            Spoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, S, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    Spoints -= 5f;
                }
            }
        }
        Spoints /= (S - target.transform.position).magnitude;
        directionalKeys.Add(S, Spoints);

        // If want to reduce resource consumption, only check the 5 directions in front and to the side
        float SWpoints = 100f;
        if ((SW - target.transform.position).magnitude <= approachRange)
        {
            SWpoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, SW, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    SWpoints -= 5f;
                }
            }
        }
        SWpoints /= (SW - target.transform.position).magnitude;
        directionalKeys.Add(SW, SWpoints);

        float Wpoints = 100f;
        if ((W - target.transform.position).magnitude <= approachRange)
        {
            Wpoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, W, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    Wpoints -= 5f;
                }
            }
        }
        Wpoints /= (W - target.transform.position).magnitude;
        directionalKeys.Add(W, Wpoints);

        float NWpoints = 100f;
        if ((NW - target.transform.position).magnitude <= approachRange)
        {
            NWpoints = 0;
        }
        else
        {
            foreach (var hit in Physics2D.RaycastAll(transform.position, NW, 1f))
            {
                // Check for anything in the way
                if (hit.collider.tag.Contains("Enemy"))
                {
                    NWpoints -= 5f;
                }
            }
        }
        NWpoints /= (NW - target.transform.position).magnitude;
        directionalKeys.Add(NW, NWpoints);
        #endregion

        // Compare all the possible move position's point to determine where should this entity move next
        var max = from x in directionalKeys where x.Value == directionalKeys.Max(v => v.Value) select x.Key;
        Vector3 targetPos = Vector3.zero;
        foreach(var key in max.ToList())
        {
            targetPos = key;
        }
        return targetPos;
    }

    private void OnDrawGizmos()
    {
        Vector3 N, NE, E, SE, S, SW, W, NW;
        N = transform.position + (transform.up * 2f);
        NE = transform.position + (transform.up + transform.right).normalized * 2f;
        E = transform.position + (transform.right * 2f);
        SE = transform.position + (transform.right - transform.up).normalized * 2f;
        S = transform.position + (-transform.up) * 2f;
        SW = transform.position + (-transform.up - transform.right).normalized * 2f;
        W = transform.position + (-transform.right * 2f);
        NW = transform.position + (-transform.right + transform.up).normalized * 2f;

        Gizmos.DrawLine(transform.position, N);
        Gizmos.DrawLine(transform.position, NE);
        Gizmos.DrawLine(transform.position, E);
        Gizmos.DrawLine(transform.position, SE);
        Gizmos.DrawLine(transform.position, S);
        Gizmos.DrawLine(transform.position, SW);
        Gizmos.DrawLine(transform.position, W);
        Gizmos.DrawLine(transform.position, NW);
    }
}
