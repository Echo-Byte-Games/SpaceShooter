using Unity.VisualScripting;
using UnityEngine;

public class BacgroundObject : MonoBehaviour
{
    public float speed = 5f; // movement speed
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Start()
    {
        
    }
    void Update()
    {
        // Move object to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -6) // -6 is begining or the screen x cordinate
        {
            gameObject.SetActive(false);
        }

    }
}
