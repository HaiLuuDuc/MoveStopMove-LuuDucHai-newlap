using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMaterialButton : BaseButton
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Image border;
    [SerializeField] int matOrder;
    protected override void OnClick()
    {
        border.rectTransform.anchoredPosition= rectTransform.anchoredPosition;
        if (matOrder == 1)
        {
            WeaponShopManager.Instance.ChooseMat1();
        }
        else
        {
            WeaponShopManager.Instance.ChooseMat2();
        }
    }
}
