using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : BaseButton
{
    [SerializeField] private GameObject cross;

    private void Start()
    {
        ChangeCross(AudioManager.instance.isMute);
    }

    protected override void OnClick()
    {
        AudioManager.instance.isMute = !AudioManager.instance.isMute;
        DataManager.ins.playerData.isMute = AudioManager.instance.isMute;
        ChangeCross(AudioManager.instance.isMute);
    }

    public void ChangeCross(bool isMute)
    {
        if(isMute == false)
        {
            cross.SetActive(false);
        }
        else
        {
            cross.SetActive(true);
        }
    }
}
