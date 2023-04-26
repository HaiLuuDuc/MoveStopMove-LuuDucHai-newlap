using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CanvasNameOnUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;
    private Vector3 targetPosition;
    public string nameString;

    private void Start()
    {
        // neu la bot thi moi random name
        Bot bot = targetTransform.GetComponent<Bot>();
        if(bot != null)
        {
            nameString = BotNamePool.instance.nameList[(int)Random.Range(0, BotNamePool.instance.nameList.Count)];
            tmp.text = nameString;
        }
    }

    void LateUpdate()
    {
        targetPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);
        rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, speed * Time.deltaTime);
    }

    public void SetTargetTransform(Transform targetTF)
    {
        targetTransform = targetTF;
    }

    public void SetColor(Bot bot)
    {
        tmp.color = bot.skinnedMeshRenderer.material.color;
    }
}
