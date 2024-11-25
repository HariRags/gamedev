using UnityEngine;

public class enemyAI : MonoBehaviour
{
    private bool isHit = false;
    private float hitCooldown = 0.5f; // Cooldown time in seconds
    private float lastHitTime;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float speedIncreaseInterval = 10f; // Time interval in seconds to increase speed
    [SerializeField] private float speedIncreaseAmount = 0.5f; // Amount to increase speed each interval
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private float elapsedTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("enemyAI script initialized for: " + gameObject.name);

        lastHitTime = -hitCooldown; // Initialize to allow immediate hit
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // Disable gravity
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Lock rotation
        }
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= speedIncreaseInterval)
        {
            patrolSpeed += speedIncreaseAmount;
            elapsedTime = 0f; // Reset the elapsed time
            Debug.Log("Patrol speed increased to: " + patrolSpeed);
        }

        Patrol();
    }

    private void Patrol()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 targetPosition = new Vector3(targetWaypoint.position.x, targetWaypoint.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        // Increase the distance threshold to avoid oscillation
        if (Vector3.Distance(transform.position, targetPosition) < 0.7f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (IsCollisionFromAbove(collision))
            {
                gameObject.GetComponent<health>().TakeDamage(1);
                print("Enemy hit by player jump");
            }
            else
            {
                collision.gameObject.GetComponent<health>().TakeDamage(1);
                print("Player hit");
            }
        }
        if (collision.gameObject.tag == "Fireball" && Time.time - lastHitTime >= hitCooldown)
        {
            isHit = true;
            lastHitTime = Time.time;

            gameObject.GetComponent<health>().TakeDamage(1);
            print("Enemy hit");
        }
        print("Player hit no tag ");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fireball")
        {
            isHit = false;
        }
    }

    private bool IsCollisionFromAbove(Collider2D collision)
    {
        return collision.bounds.min.y > transform.position.y;
    }
}

