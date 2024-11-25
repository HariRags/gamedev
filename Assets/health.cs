using UnityEngine;

public class health : MonoBehaviour
{
    [SerializeField] public float maxhealthPoints;
    public float currHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currHealth = maxhealthPoints;
    }

    // Update is called once per frame
    public void TakeDamage(float _damage)
    {
        currHealth = Mathf.Clamp(currHealth - _damage, 0, maxhealthPoints);
        print("Health: " + currHealth);
        if (currHealth <= 0)
        {
            print("Dead");
            Die();
        }
    }
    void Die()
    {
        ScoreManager.instance.AddScore(1); // Increase the score by 1

        Destroy(gameObject);
    }
}
