using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReplay : BaseButton
{
    protected override void OnClick()
    {
        //LevelManager.instance.RemakeLevel();
        UIManager.instance.ShowIndicators();
    }
}
