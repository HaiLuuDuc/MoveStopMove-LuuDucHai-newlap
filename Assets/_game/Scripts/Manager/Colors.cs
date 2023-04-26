using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public Material[] characterColors;
    public Material transparent100;

    public static Colors instance;
    private void Awake()
    {
        instance= this;
    }
}
