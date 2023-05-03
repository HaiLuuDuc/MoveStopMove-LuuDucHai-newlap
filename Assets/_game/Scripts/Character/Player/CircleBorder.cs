using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBorder : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        transform.position = characterTransform.position + offset;
    }
}
