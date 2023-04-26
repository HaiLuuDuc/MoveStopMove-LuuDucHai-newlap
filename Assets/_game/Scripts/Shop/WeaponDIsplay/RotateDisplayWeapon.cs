using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDisplayWeapon : MonoBehaviour
{
    [SerializeField] private float speedRotate;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0,speedRotate*Time.deltaTime,0));
    }
}
