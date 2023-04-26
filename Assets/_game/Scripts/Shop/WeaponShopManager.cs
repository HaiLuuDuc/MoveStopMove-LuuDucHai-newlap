using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class WeaponShopManager : MonoBehaviour
{
    [Header("Display:")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponCost;

    [Header("Weapon:")]
    public Weapon[] weapons;
    public GameObject[] weaponMats;

    [Header("Indexs:")]
    public int currentWeaponIndex = 0;
    public int usingWeaponIndex = 0;


    public static WeaponShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        InitWeaponMesh();
        weapons[currentWeaponIndex].isPurchased = true;
        DisplayWeapon(currentWeaponIndex);
        DisplayWeaponMat(currentWeaponIndex);
    }

    public void InitWeaponMesh()
    {
        for(int i=0;i<weapons.Length;i++) {
            Weapon wp = weapons[i];
            wp.transform.localPosition = Vector3.zero;
            wp.transform.localRotation = Quaternion.Euler(new Vector3(20, 0, 0));
            wp.transform.localScale = new Vector3(1, 1, 1);
            wp.ChangeMaterial(0);
            wp.gameObject.SetActive(false);
        }
    }

    public void UnDisplayWeapon(int index)
    {
        weapons[index].gameObject.SetActive(false);
    }

    public void DisplayWeapon(int index)
    {
        weapons[index].gameObject.SetActive(true);
        DisplayWeaponButtonText(index);
    }

    public void DisplayWeaponButtonText(int index)
    {
        weaponName.text = weapons[currentWeaponIndex].weaponData.weaponName;
        if (weapons[index].isPurchased == false)
        {
            weaponCost.text = weapons[currentWeaponIndex].weaponData.weaponCost.ToString();
        }
        else
        {
            if (index == usingWeaponIndex)
            {
                weaponCost.text = Constant.USING;
            }
            else
            {
                weaponCost.text = Constant.USE;
            }
        }
    }

    public void UnDisplayAllWeaponMats()
    {
        for(int i = 0; i < weaponMats.Length; i++)
        {
            weaponMats[i].gameObject.SetActive(false);
        }
    }

    public void DisplayWeaponMat(int index)
    {
        weaponMats[index].gameObject.SetActive(true);
    }

    public void NextWeapon()
    {
        UnDisplayWeapon(currentWeaponIndex);
        UnDisplayAllWeaponMats();
        currentWeaponIndex++;
        if(currentWeaponIndex>= weapons.Length)
        {
            currentWeaponIndex= 0;
        }
        DisplayWeapon(currentWeaponIndex);
        DisplayWeaponMat(currentWeaponIndex);
    }

    public void PreviousWeapon()
    {
        UnDisplayWeapon(currentWeaponIndex);
        UnDisplayAllWeaponMats();
        currentWeaponIndex--;
        if (currentWeaponIndex == -1)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        DisplayWeapon(currentWeaponIndex);
        DisplayWeaponMat(currentWeaponIndex);
    }

    public void BuyWeapon()
    {
        if (weapons[currentWeaponIndex].isPurchased == false && DataManager.ins.playerData.coin >= weapons[currentWeaponIndex].weaponData.weaponCost)
        {
            weapons[currentWeaponIndex].isPurchased = true;
            DataManager.ins.playerData.coin -= weapons[currentWeaponIndex].weaponData.weaponCost;
            UIManager.instance.UpdateUICoin();
            weaponCost.text = Constant.USING;
            usingWeaponIndex = currentWeaponIndex;
        }
        else if (weapons[currentWeaponIndex].isPurchased == true)
        {
            weaponCost.text = Constant.USING;
            usingWeaponIndex = currentWeaponIndex;
        }
    }

    public void ChooseMat1()
    {
        Weapon wp = weapons[currentWeaponIndex];
        wp.currentMaterialIndex = 0;
        wp.ChangeMaterial(wp.currentMaterialIndex);
    }

    public void ChooseMat2()
    {
        Weapon wp = weapons[currentWeaponIndex];
        wp.currentMaterialIndex = 1;
        wp.ChangeMaterial(wp.currentMaterialIndex); 
    }

    public GameObject GetWeapon()
    {
        return weapons[usingWeaponIndex].gameObject;
    }
}
