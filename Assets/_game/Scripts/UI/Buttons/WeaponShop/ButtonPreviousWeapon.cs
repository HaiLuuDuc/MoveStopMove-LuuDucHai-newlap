using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPreviousWeapon : BaseButton
{
    protected override void OnClick()
    {
        WeaponShopManager.Instance.PreviousWeapon();
    }
}
