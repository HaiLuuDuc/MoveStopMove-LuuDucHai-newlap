using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    [SerializeField] protected Button button;

    protected virtual void Awake()
    {
        this.AddOnClickEvent();
    }

    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();

    public virtual void RunOnClick()
    {
        OnClick();
    }
}
