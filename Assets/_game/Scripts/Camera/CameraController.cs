using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CameraState
{
    MainMenu = 0, Skin = 1, Gaming = 2
}
public class CameraController : MonoBehaviour
{
    [Header("Target:")]
    [SerializeField] private Transform target;

    [Header("Arrays:")]
    public Vector3[] offsetPositions;
    public Vector3[] offsetTargets;
    [SerializeField] public KeyValuePair<Vector3, Vector3>[] pairs; // dont know why not show in inspector

    [Header("Speed:")]
    [SerializeField] private float speedFollow;
    [SerializeField] private float timeSwitch;
    [SerializeField] private float timeMoveHigher;

    private Vector3 currentOffsetPosition;
    private Vector3 currentOffsetTarget;
    private Vector3 startOffsetPos;
    private Vector3 startOffsetTarget;

    private float higherValue = 1.05f;

    //singleton
    public static CameraController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentOffsetPosition = offsetPositions[(int)CameraState.MainMenu];
        currentOffsetTarget = offsetTargets[(int)CameraState.MainMenu];
        StartCoroutine(SwitchTo(CameraState.MainMenu));
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + currentOffsetPosition, speedFollow * Time.deltaTime);
        transform.LookAt(target.position + currentOffsetTarget);
    }

    public IEnumerator SwitchTo(CameraState cameraState)
    {
        startOffsetPos = currentOffsetPosition;
        startOffsetTarget = currentOffsetTarget;
        float duration = timeSwitch;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                currentOffsetPosition = Vector3.Lerp(startOffsetPos, offsetPositions[(int)cameraState], t);
                currentOffsetTarget = Vector3.Lerp(startOffsetTarget, offsetTargets[(int)cameraState], t);
            }
            yield return null;
        }
        yield return null;
    }

    public void StartCotoutineSwitchTo(CameraState cameraState)
    {
        StartCoroutine(SwitchTo(cameraState));
    }

    public IEnumerator MoveHigher()
    {
        float duration = timeMoveHigher;
        float elapsedTime = 0f;
        Vector3 newOffsetPosition = currentOffsetPosition*higherValue;
        Vector3 newOffsetTarget = currentOffsetTarget*higherValue;
        while(elapsedTime < duration)
        {
            elapsedTime+= Time.deltaTime;
            float t = elapsedTime / duration;
            currentOffsetPosition = Vector3.Lerp(currentOffsetPosition, newOffsetPosition, t);
            currentOffsetTarget = Vector3.Lerp(currentOffsetTarget, newOffsetTarget, t);
            yield return null;
        }
        yield return null;
    }

}
