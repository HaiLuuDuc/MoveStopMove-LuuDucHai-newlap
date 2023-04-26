using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWearSkinItems : MonoBehaviour
{
    [Header("Character Skin Positions:")]
    public Transform leftHand;
    public Transform leftHand2;
    public Transform dinhdau;
    public Transform lung;
    public Transform tail;
    public SkinnedMeshRenderer pants;
    [Header("Fullset Datas:")]
    public FullsetData[] fullsetDatas;


    public virtual GameObject WearHat(int index)
    {
        if (index < 0)
        {
            return null;
        }
        GameObject newHat = Instantiate(SkinShopManager.instance.hats[index]);
        Quaternion hatOldRotation = newHat.transform.rotation;
        newHat.transform.SetParent(dinhdau.transform);
        newHat.transform.localPosition = Vector3.zero;
        newHat.transform.localRotation = hatOldRotation;
        return newHat;
    }

    public virtual void WearPants(int index)
    {
        pants.material = SkinShopManager.instance.pants[index];
    }

    public virtual GameObject WearShield(int index)
    {
        if (index < 0)
        {
            return null;
        }
        GameObject newShield = Instantiate(SkinShopManager.instance.shields[index]);
        Quaternion shieldOldRotation = newShield.transform.rotation;
        Vector3 shieldOldScale = newShield.transform.localScale;
        newShield.transform.SetParent(leftHand.transform);
        newShield.transform.localPosition = Vector3.zero;
        newShield.transform.localRotation = shieldOldRotation;
        newShield.transform.localScale = shieldOldScale;
        return newShield;
    }

}
