using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMaterialButton : BaseButton
{
    [SerializeField] int matOrder;
    public WeaponType weaponType;
    public Outline outline;

    protected override void OnClick()
    {
        WeaponShopManager.Instance.ChooseMat(matOrder);
        WeaponShopManager.Instance.HideOutlinesWithSameWeaponType(this.weaponType);
        ShowOutline();
    }

    public void ShowOutline()
    {
        outline.gameObject.SetActive(true); 
    }
}
