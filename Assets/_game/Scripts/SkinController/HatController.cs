using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatController : ItemController
{
    public override void OnButtonClick(int index)
    {
        //hien thi buy button
        base.OnButtonClick(index);
        //mac do cho player
        player.WearHat(index);
    }
}
