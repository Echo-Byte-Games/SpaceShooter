using Unity.VisualScripting;
using UnityEngine;

public class BacgroundObject : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    [SerializeField, Range(0f, 1f)] private float memoryRemovechance = 0.05f;

    void Start()
    {
        
    }
    void Update()
    {
        // Move object to the left
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -6) // -6 is just outside or the left side of the screen
        {
            if (memoryRemovechance >= Random.Range(0f, 1f))
            {
                Destroy(gameObject);
                return;
            }
            gameObject.SetActive(false);
        }

    }
}
