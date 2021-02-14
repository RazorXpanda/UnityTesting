using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody rbPlayer;

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody attached to the player
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Move forward
        if (Input.GetKey(KeyCode.W))
        {
            rbPlayer.velocity = -transform.forward * moveSpeed;
        }
        //Move backwards
        if (Input.GetKey(KeyCode.S))
        {
            rbPlayer.velocity = transform.forward * moveSpeed;
        }
        //Move left
        if (Input.GetKey(KeyCode.A))
        {
            rbPlayer.velocity = -transform.right * moveSpeed;
        }
        //Move right
        if (Input.GetKey(KeyCode.D))
        {
            rbPlayer.velocity = transform.right * moveSpeed;
        }


        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot!");
        }

        rotatePlayer();
    }

    //Player Rotation
    void rotatePlayer()
    {

    }
}
