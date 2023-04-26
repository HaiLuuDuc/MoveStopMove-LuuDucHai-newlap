using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{

    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private float rotateSpeed;
    public Transform enemyTransform;

    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            transform.Rotate(0, rotateSpeed, 0);
            if(enemyTransform != null)
            {
                transform.position = enemyTransform.position;
            }
        }
    }
    
    public void Active()
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }
        if (playerAttack.enemy != null)
        {
            this.enemyTransform = playerAttack.enemy.transform;
        }
    }

    public void Deactive()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.transform.position -= new Vector3(0, 100, 0); // fix loi targetCircle nhap nhay o enemy cu~
            this.gameObject.SetActive(false);
        }
    }
}
