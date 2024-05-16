using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour    
{
    //[SerializeField] private float movementSpeed = 3.0f;

    public Rigidbody2D rb;
    private Vector2 movementDirection;
    public float moveSpeed = 5f;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //animator.SetFloat("Speed", Mathf.Abs(movementDirection.magnitude * movementSpeed));

        //bool flipped = movementDirection.x < 0;
        //this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        animator.SetFloat("Speed", movementDirection.sqrMagnitude);

    }

    private void FixedUpdate()
    {
        //rb.velocity = movementDirection * movementSpeed;
        rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
