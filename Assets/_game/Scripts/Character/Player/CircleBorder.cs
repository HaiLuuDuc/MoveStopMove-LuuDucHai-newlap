using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBorder : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    void Update()
    {
        transform.position = characterTransform.position;
    }
}
