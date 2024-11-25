using Unity.VisualScripting;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField] public float attackCooldown;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject fireballs;
    private float cooldown = Mathf.Infinity;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && cooldown > attackCooldown)
        {
            Attack();
        }
        cooldown += Time.deltaTime;
    }
    private void Attack()
    {
        animator.SetTrigger("attack");
        cooldown = 0;
        fireballs.transform.position = attackPoint.position;  
        fireballs.GetComponent<fireballs>().SetDirection(transform.localScale.x);
    }
}
