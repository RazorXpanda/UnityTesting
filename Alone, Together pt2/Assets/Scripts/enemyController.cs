using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D enemyRB;

    public float approachLimit; //closest it wants to go to the player
    public float approachMin; //farthest away it will allow itself to halt
    private float approachRange;
    [SerializeField] [Range(0, 3)] float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        approachRange = Random.Range(approachLimit, approachMin);
    }


    void FixedUpdate()
    {
        Vector3 playerPos = player.transform.position;

        //Move towards the player
        Vector3 moveDirection = playerPos - transform.position;
        if(moveDirection.magnitude > approachRange)
        {
            Vector2 moveDir = new Vector2(moveDirection.x, moveDirection.y) * 0.5f;
            enemyRB.MovePosition(enemyRB.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
        


        //Look towards the player
        Vector2 lookDir = player.transform.position - transform.position;
        float angle = Vector2.Angle(Vector2.up, lookDir);

        if(lookDir.x >= 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -angle);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
