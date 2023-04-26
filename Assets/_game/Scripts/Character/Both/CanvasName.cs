using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CanvasName : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position + offset, speed * Time.deltaTime);
    }

}
