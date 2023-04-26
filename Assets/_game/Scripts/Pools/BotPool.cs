using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float initialY;
    private List<GameObject> pool = new List<GameObject>();
    public int poolSize;

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Vector3 pos = new Vector3(0, initialY, 0);
            GameObject obj = Instantiate(prefab,pos,Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If we get here, all objects are in use
        Vector3 pos = new Vector3(0, initialY, 0);
        GameObject newObj = Instantiate(prefab,pos,Quaternion.identity);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(false);
    }
}