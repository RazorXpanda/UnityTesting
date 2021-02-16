using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region Debug
    public float bulletSpeed;
    public GameObject debugCube;
    #endregion

    private Rigidbody2D rbPlayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Camera cam;
    private Vector2 movement;
    private float angle;

    [Header("SFX")]
    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] private AudioSource m_audioSource;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");        // PC input
        movement.y = Input.GetAxisRaw("Vertical");          // PC input
        movement.Normalize();
        Vector2 direction = Vector2.up * movement.y + Vector2.right * movement.x;
        rbPlayer.MovePosition(rbPlayer.position + direction * moveSpeed * Time.fixedDeltaTime);

        var mousePos = Input.mousePosition;
        var screenPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - cam.transform.position.z));
     
        rbPlayer.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x))* Mathf.Rad2Deg);;
    }

    void Shoot()
    {
        var direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.z = 0f;
        var _bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        m_audioSource.PlayOneShot(m_audioClip);
        LeanTween.move(_bullet, shootingPoint.position + (direction.normalized * 20f), .25f);
    }

    public void ResetPlayer()
    {

    }
}

