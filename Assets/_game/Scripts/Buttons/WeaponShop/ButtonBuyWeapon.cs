using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBuyWeapon : BaseButton
{
    protected override void OnClick()
    {
        WeaponShopManager.Instance.BuyWeapon();
    }
}
