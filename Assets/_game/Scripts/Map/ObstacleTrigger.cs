using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTrigger : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private Material whiteMat;
    [SerializeField] private Material transparentMat;
    private bool isWhite = true;

    private void Start()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = whiteMat;
        }
    }

    void ChangeMat()
    {
        if (isWhite)
        {
            for(int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = transparentMat;
            }
            isWhite = false;
        }
        else
        {
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].material = whiteMat;
            }
            isWhite = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.PLAYER))
        {
            ChangeMat();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.PLAYER))
        {
            ChangeMat();
        }
    }
}
