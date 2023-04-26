using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupTrigger : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material whiteMat;
    [SerializeField] private Material transparentMat;
    private bool isWhite = true;

    private void Start()
    {
        meshRenderer.material = whiteMat;
    }

    void ChangeMat()
    {
        if (isWhite)
        {
            meshRenderer.material = transparentMat;
            isWhite= false;
        }
        else
        {
            meshRenderer.material = whiteMat;
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
