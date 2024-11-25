using UnityEngine;

public class script : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpSpeed;
    public Rigidbody2D body;
    public Animator animator;
    public bool grounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        body  = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y );
        body.linearVelocity = movement;
        //flip plkayer sprite
        if (horizontalInput > 0.01)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < -0.01)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
            if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
            animator.SetBool("run", horizontalInput != 0);
    }
    public void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
