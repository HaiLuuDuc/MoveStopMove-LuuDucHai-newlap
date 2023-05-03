using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


[Serializable]
public struct outlinesStruct
{
    public WeaponType weaponType;
    public Outline[] outlines;

}
public class WeaponShopManager : MonoBehaviour
{
    [Header("Display:")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Text weaponName;
    [SerializeField] private Text weaponCost;
    public outlinesStruct[] dictOutline;

    [Header("Weapon:")]
    public Weapon[] weapons;
    public GameObject[] weaponMats;

    [Header("Indexs:")]
    public int currentWeaponIndex;
    public int usingWeaponIndex;



    public static WeaponShopManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        InitWeaponMesh();
        DisplayWeapon(currentWeaponIndex);
        DisplayWeaponMats(currentWeaponIndex);
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

    public void DisplayWeaponMats(int index)
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
        DisplayWeaponMats(currentWeaponIndex);
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
        DisplayWeaponMats(currentWeaponIndex);
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
            DataManager.ins.playerData.usingWeaponIndex = usingWeaponIndex;
            DataManager.ins.playerData.isPurchasedWeapon[currentWeaponIndex] = true;
        }
        else if (weapons[currentWeaponIndex].isPurchased == true)
        {
            weaponCost.text = Constant.USING;
            usingWeaponIndex = currentWeaponIndex;
            DataManager.ins.playerData.usingWeaponIndex = usingWeaponIndex;
            DataManager.ins.playerData.isPurchasedWeapon[currentWeaponIndex] = true;
        }
    }

    public void ChooseMat(int order)
    {
        Weapon wp = weapons[currentWeaponIndex];
        wp.currentMaterialIndex = order;
        DataManager.ins.playerData.currentWeaponMaterialIndexs[currentWeaponIndex] = order;
        wp.ChangeMaterial(wp.currentMaterialIndex);
    }

    public void HideOutlinesWithSameWeaponType(WeaponType weaponType)
    {
        for (int i = 0; i < dictOutline.Length; i++)
        {
            if (dictOutline[i].weaponType == weaponType)
            {
                for (int j = 0; j < dictOutline[i].outlines.Length; j++)
                {
                    dictOutline[i].outlines[j].gameObject.SetActive(false);
                }
            }
        }
    }


    public GameObject GetWeapon()
    {
        return weapons[usingWeaponIndex].gameObject;
    }
}
