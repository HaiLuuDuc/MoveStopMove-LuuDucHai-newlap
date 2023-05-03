using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNextWeapon : BaseButton
{
    protected override void OnClick()
    {
        WeaponShopManager.Instance.NextWeapon();
    }
}
