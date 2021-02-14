using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rbPlayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Camera cam;
    private Vector2 movement;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody attached to the player
        //Temporarily disable due to rigidbody issues
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); //PC input
        movement.y = Input.GetAxisRaw("Vertical");// PC input
        Vector2 direction = Vector2.up * movement.y + Vector2.right * movement.x;
        rbPlayer.MovePosition(rbPlayer.position + direction * moveSpeed * Time.fixedDeltaTime);

        var mousePos = Input.mousePosition;
        var screenPos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - cam.transform.position.z));
     
        rbPlayer.transform.eulerAngles = new Vector3(0,0,Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x))* Mathf.Rad2Deg);;


        
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


        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot!");
        }

    }
}

