using UnityEngine;

public class scri : MonoBehaviour
{
    public Rigidbody2D rigidman;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.Space)) == true)
        {
            rigidman.linearVelocity = Vector2.up * 5;

        }
    }
}
