using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 5;
    public Character owner;
    public List<GameObject> pool = new List<GameObject>();
    private Transform weaponClones;


    void Start()
    {
        weaponClones = GameObject.FindGameObjectWithTag(Constant.WEAPON_CLONES).transform;
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Weapon newWeapon = Instantiate(prefab).GetComponent<Weapon>(); // sinh ra 1 weapon moi
            newWeapon.transform.SetParent(weaponClones);
            newWeapon.gameObject.SetActive(false);
            newWeapon.SetOwnerAndWeaponPool(this.owner, this);
            newWeapon.transform.localScale = Vector3.one;
            newWeapon.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
            newWeapon.child.localRotation = Quaternion.Euler(Vector3.zero);
            newWeapon.GetComponent<BoxCollider>().enabled = true;
            owner.pooledWeaponList.Add(newWeapon);
            pool.Add(newWeapon.gameObject);
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Destroy(pool[i]);
        }
        pool.Clear();
        owner.pooledWeaponList.Clear();
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
        GameObject newObj = Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}