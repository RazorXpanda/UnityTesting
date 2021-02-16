using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D enemyRB;
    private Animator animator;

    public bool isAggro = false;
    public float bulletSpeed;
    public float chaseTime;
    public float shotDelay;
    public float detectionRange;
    public float approachMin;           //closest it wants to go to the player
    public float approachMax;           //farthest away it will allow itself to halt
    private float approachRange;
    private float nextShotTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] [Range(0, 5)] float moveSpeed;

    [Header("SFX")]
    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] private AudioSource m_audioSource;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        approachRange = Random.Range(approachMin, approachMax);
        nextShotTime = Time.time + 3f;
        chaseTime = 5f;
    }

    private void Update()
    {
        if (chaseTime <= 0 || target == null || !target.tag.Contains("Player"))
        {
            ScanForTargetInRange();
        }
        else
            chaseTime -= Time.deltaTime;

        if (nextShotTime < Time.time)
        {
            if (target != null && IsAnythingInTheWay() == false)
            {
                Attack();
                nextShotTime = Time.time + shotDelay;
            }
        }
    }

    // Scan for available targets in range
    void ScanForTargetInRange()
    {
        var entities = Physics2D.OverlapCircleAll(transform.position, detectionRange  * 1.5f);

        foreach (var entity in entities)
        {
            if (entity.tag.Contains("Player"))
            {
                isAggro = true;
                target = entity.gameObject;
                chaseTime = 5f;
                return;
            }
            else if (entity.tag.Contains("Objective"))
            {
                target = entity.gameObject;
            }
        }

        if(isAggro == true)
            target = GameObject.FindGameObjectWithTag("Objective");
    }

    bool IsAnythingInTheWay()
    {
        bool value = false;
        var lineOfSight = Physics2D.RaycastAll(shootingPoint.position, target.transform.position - transform.position, (target.transform.position - transform.position).magnitude);

        foreach(var item in lineOfSight)
        {
            if (item.transform.tag.Contains("Enemy"))
                value = true;
            if (item.transform.tag.Contains("Obstacle"))
                value = true;
        }

        return value;
    }

    void Attack()
    {
        var distanceToTarget = (transform.position - target.transform.position).magnitude;
        if (distanceToTarget <= detectionRange * 2f)
        {
            if(distanceToTarget <= 2f)
            {
                // Close combat
                Debug.Log("Melee attack");
            }
            else
            {
                // At a distance, shoot
                var direction = target.transform.position - transform.position;
                direction.z = 0f;
                var _bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
                m_audioSource.PlayOneShot(m_audioClip);
                LeanTween.move(_bullet, shootingPoint.position + (direction.normalized * 10f), .25f);
            }
        }
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 targetVector = target.transform.position - transform.position;
            if (Mathf.Abs(targetVector.magnitude) < detectionRange)
                Move(CheckSurrounding());
            else
                Move(transform.position + targetVector.normalized);
            RotateToTarget();
        }
    }

    private void Move(Vector3 targetPos)
    {
        var moveDir = (targetPos - transform.position).normalized;
        enemyRB.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateToTarget()
    {
        // Rotate the shooting point to target
        if (target != null)
        {
            //Look towards the target
            Vector2 lookDir = target.transform.position - transform.position;
            float angle = Vector2.Angle(Vector2.up, lookDir);

            animator.SetFloat("moveX", lookDir.x);
            animator.SetFloat("moveY", lookDir.y);

            if (lookDir.x >= 0)
            {
                pivotPoint.transform.eulerAngles = new Vector3(0, 0, -angle);
            }
            else
            {
                pivotPoint.transform.eulerAngles = new Vector3(0, 0, angle);
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
        if (Mathf.Abs((N - target.transform.position).magnitude) <= approachRange)
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
                    Npoints -= 55f;
                }
                else
                    Npoints -= 45f;
            }
        }
        Npoints /= (N - target.transform.position).magnitude;
        directionalKeys.Add(N, Npoints);

        float NEpoints = 100f;
        if (Mathf.Abs((NE - target.transform.position).magnitude) <= approachRange)
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
                    NEpoints -= 55f;
                }
                else
                    NEpoints -= 45f;
            }
        }
        NEpoints /= Mathf.Abs((NE - target.transform.position).magnitude);
        directionalKeys.Add(NE, NEpoints);

        float Epoints = 100f;
        if (Mathf.Abs((E - target.transform.position).magnitude) <= approachRange)
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
                    Epoints -= 55f;
                }
                else
                    NEpoints -= 45f;
            }
        }
        Epoints /= Mathf.Abs((E - target.transform.position).magnitude);
        directionalKeys.Add(E, NEpoints);

        float SEpoints = 100f;
        if (Mathf.Abs((SE - target.transform.position).magnitude) <= approachRange)
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
                    SEpoints -= 55f;
                }
                else
                    SEpoints -= 45f;
            }
        }
        SEpoints /= Mathf.Abs((SE - target.transform.position).magnitude);
        directionalKeys.Add(SE, NEpoints);

        float Spoints = 100f;
        if (Mathf.Abs((S - target.transform.position).magnitude) <= approachRange)
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
                    Spoints -= 55f;
                }
                else
                    Spoints -= 45f;
            }
        }
        Spoints /= Mathf.Abs((S - target.transform.position).magnitude);
        directionalKeys.Add(S, Spoints);

        // If want to reduce resource consumption, only check the 5 directions in front and to the side
        float SWpoints = 100f;
        if (Mathf.Abs((SW - target.transform.position).magnitude) <= approachRange)
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
                    SWpoints -= 55f;
                }
                else
                    SWpoints -= 45f;
            }
        }
        SWpoints /= Mathf.Abs((SW - target.transform.position).magnitude);
        directionalKeys.Add(SW, SWpoints);

        float Wpoints = 100f;
        if (Mathf.Abs((W - target.transform.position).magnitude) <= approachRange)
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
                    Wpoints -= 55f;
                }
                else
                    Wpoints -= 45f;
            }
        }
        Wpoints /= Mathf.Abs((W - target.transform.position).magnitude);
        directionalKeys.Add(W, Wpoints);

        float NWpoints = 100f;
        if (Mathf.Abs((NW - target.transform.position).magnitude) <= approachRange)
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
                    NWpoints -= 55f;
                }
                else
                    NWpoints -= 45f;
            }
        }
        NWpoints /= Mathf.Abs((NW - target.transform.position).magnitude);
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

        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
