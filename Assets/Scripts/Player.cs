using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float moveSpeed = 50f;
    [SerializeField] private LayerMask dashLayerMask;
    //private BoxCollider2D boxCollider;
    private Vector3 moveDir;
    //private RaycastHit2D hit;
    private Rigidbody2D rigidbody2D;
    private bool isDashButtonDown;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //boxCollider = GetComponent < BoxCollider2D>();       
    }
    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
    }
    private void FixedUpdate()
    {
        /*
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //Reset moveDelta
        moveDelta = new Vector3(x,y,0);

        //Swap sprite direction.
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);

        //move in this direction by casting box there first, if box == null, free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            //movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Player", "Blocking"));
        if (hit.collider == null)
        {
            //movement
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
        */

        rigidbody2D.velocity = moveDir * moveSpeed;
        
        if (isDashButtonDown)
        {
            float dashAmount = 0.5f;
            Vector3 dashPosition = transform.position + moveDir * dashAmount;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
            if(raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }

            rigidbody2D.MovePosition(dashPosition);
            isDashButtonDown = false; 
        }
        
    }
}
