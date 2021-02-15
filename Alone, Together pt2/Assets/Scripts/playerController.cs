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

    //Audio use
    [SerializeField] private AudioClip m_audioClip;
    [SerializeField] private AudioSource m_audioSource;

    void Start()
    {
        //Get the rigidbody attached to the player
        //Temporarily disable due to rigidbody issues
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

        #region OLD
        // if (movement.x != 0 && movement.y != 0)
        // {
        //     angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        //     rbPlayer.rotation = angle;
        // }

        // if (movement != Vector2.zero)
        // {
        //     rbPlayer.MoveRotation(angle);
        // }

        // //I reckon we should not use velocity. Players cannot control the speed of the ball after key release.
        // //Move forward
        // if (Input.GetKey(KeyCode.W))
        // {
        //     //rbPlayer.velocity = -transform.forward * moveSpeed;
        //     Vector3 pos = transform.position;
        //     pos += transform.up * moveSpeed * Time.deltaTime;
        //     transform.position = pos;

        // }
        // //Move backwards
        // if (Input.GetKey(KeyCode.S))
        // {
        //     rbPlayer.velocity = transform.forward * moveSpeed;
        //     Vector3 pos = transform.position;
        //     pos += -transform.up * moveSpeed * Time.deltaTime;
        //     transform.position = pos;

        // }
        // //Move left
        // if (Input.GetKey(KeyCode.A))
        // {
        //     //rbPlayer.velocity = -transform.right * moveSpeed;
        //     Vector3 pos = transform.position;
        //     pos += -transform.right * moveSpeed * Time.deltaTime;
        //     transform.position = pos;

        // }
        // //Move right
        // if (Input.GetKey(KeyCode.D))
        // {
        //     //rbPlayer.velocity = transform.right * moveSpeed;
        //     Vector3 pos = transform.position;
        //     pos += transform.right * moveSpeed * Time.deltaTime;
        //     transform.position = pos;

        // }
        #endregion
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

