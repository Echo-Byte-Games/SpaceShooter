using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPoolerBackgroundObjects : MonoBehaviour
{
    public List<GameObject> prefabList;
    public List<ChancedPrefab> chancedPrefabList;
    public int poolSize = 30;

    [System.Serializable]
    public class ChancedPrefab
    {
        public GameObject prefab;
        public float chance;
    }

    private List<GameObject> pool;
    void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }
    private List<float> CreateChanceSpreadList()
    {
        List<float> list = new List<float>();
        float sum = 0f;
        list.Add(0f);
        foreach (ChancedPrefab chancedPrefab in chancedPrefabList)
        {
            sum += chancedPrefab.chance;
            list.Add(sum);
        }
        return list;
    }
    private GameObject CreateNewObject()
    {
        int index = Random.Range(0, chancedPrefabList.Count);
        GameObject obj = Instantiate(chancedPrefabList[index].prefab, transform.position, transform.rotation);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }
}
