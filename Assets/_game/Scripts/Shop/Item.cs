using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int cost;
    public bool isPurchased;
    public GameObject lockObj;
    public ItemType itemType;
    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        UnDisplayOutline();
    }

    public void DisplayOutline()
    {
        if(outline != null) { 
            outline.enabled = true;
        }
    }
    
    public void UnDisplayOutline()
    {
        if(outline != null)
        {
            outline.enabled = false;
        }
    }
}
