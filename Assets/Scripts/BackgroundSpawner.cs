using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolerBackgroundObjects objectPoolerBackgroundObjects;
    [SerializeField] private float spawnRateRangeDown = 0f;
    [SerializeField] private float spawnRateRangeUp = 0.05f;
    [SerializeField, Range(0f, 1.5f)] private float objectMoveSpeedEasingFactor = 0.4f;
    [SerializeField] private float objectMoveSpeedFactor = 1f;
    [SerializeField] private float objectMoveSpeedRandomFactor = 0.1f;
 

    private float randomIntFrequency = 0.5f;

    void Start()
    {
        
    }


    float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= randomIntFrequency)
        {
            timer = 0f;

            randomIntFrequency = Random.Range(spawnRateRangeDown, spawnRateRangeUp);
            
            GameObject obj =  objectPoolerBackgroundObjects.GetPooledObject();
            float yPosition = Random.Range(-2.5f, 2.5f);
            obj.transform.position = transform.position + new Vector3(0, yPosition, 0);
            BacgroundObject bacgroundObject = obj.GetComponent<BacgroundObject>();

            //float y = Mathf.Abs(yPosition);
            //float eased = y * y * (3f - 2f * y);
            //float eased = y * (3f - 2f * y);

            bacgroundObject.speed = Mathf.Abs(yPosition) * objectMoveSpeedEasingFactor + Random.Range(objectMoveSpeedFactor, objectMoveSpeedFactor + objectMoveSpeedRandomFactor); 
            obj.SetActive(true);
        }
    }
}
