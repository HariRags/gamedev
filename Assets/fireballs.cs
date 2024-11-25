using UnityEngine;
using UnityEngine.Rendering;

public class fireballs : MonoBehaviour
{
    private float direction;
    [SerializeField] private float speed;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        
        Deactivate();

    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float xScale = transform.localScale.x;
        if(Mathf.Sign(xScale) != direction)
        {
            xScale *= -1;
        }
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
