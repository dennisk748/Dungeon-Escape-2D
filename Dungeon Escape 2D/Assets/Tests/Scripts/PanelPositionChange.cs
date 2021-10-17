using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPositionChange : MonoBehaviour
{
    Canvas canvas;
    RectTransform rectTransform;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float x = canvas.transform.position.x - 0.25f;
        float y = canvas.transform.position.y - 4.11f;
        //Debug.Log(x);
        Vector3 changedpos = rectTransform.anchoredPosition;
        changedpos.x += x;
        changedpos.y += y;
        

        rectTransform.anchoredPosition = new Vector3(changedpos.x,changedpos.y,0f);

    }
}
